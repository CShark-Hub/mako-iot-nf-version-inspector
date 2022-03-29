namespace Mako.IoT.NFVersionInspector
{
    public class DependencyFinder
    {
        public static IEnumerable<Package> FindPackages(string rootPath)
        {
            var packagePaths = new HashSet<string>();
            foreach (var nfproj in FileFinder.FindNfprojFiles(rootPath))
            {
                using var r = new StreamReader(File.OpenRead(nfproj));
                foreach (var packagesPath in NfprojParser.GetPackagesPaths(r))
                {
                    packagePaths.Add(packagesPath);
                }
            }

            var packages = FileFinder.FindNuspecFiles(rootPath, packagePaths);
            

            return packages;
        }

        public static IEnumerable<Package> GetDependenciesFromNuget(string id, string version, bool refreshCache = false,
            IList<Package>? packages = null, Dictionary<string, string>? versionMapping = null)
        {
            packages ??= new List<Package>();
            versionMapping ??= new Dictionary<string, string>();

            if (versionMapping.ContainsKey($"{id}-{version}"))
                version = versionMapping[$"{id}-{version}"];


            if (!packages.Any(p => p.Id == id && p.Version == version))
            {
                string? versionFound = null;
                var package = Cache.GetOrAdd(id, version, 
                    (pId, pVersion) =>  NugetClient.GetFromNuspec(pId, pVersion, out versionFound),
                    refreshCache);
                if (versionFound != null)
                {
                    versionMapping.Add($"{id}-{version}", versionFound);
                }
                packages.Add(package);
                foreach (var p in package.Dependencies)
                {
                    if (!MinimumVersions.IsMinimumVersion(p))
                        throw new PackageNotFoundException();


                    if (!packages.Contains(p))
                    {
                        if (p.IsNative)
                            packages.Add(p);
                        else
                            GetDependenciesFromNuget(p.Id, p.Version, refreshCache, packages, versionMapping);
                    }
                }
            }

            return packages;
        }

        public static string? FindCompatibleVersion(string id, IEnumerable<Package> nativePackages,
            bool refreshCache = false, bool silent = false)

        {
            var versions = NugetClient.GetPackageVersions(id);
            var np = nativePackages as Package[] ?? nativePackages.ToArray();

            foreach (var version in versions)
            {
                if (!silent)
                    Console.WriteLine($"Checking {id} {version}...");

                try
                {
                    var deps = GetDependenciesFromNuget(id, version, refreshCache).Where(d => d.IsNative);

                    if (!deps.Except(np).Any())
                        return version;

                }
                catch (PackageNotFoundException)
                {
                }

            }

            return null;
        }
    }
}
