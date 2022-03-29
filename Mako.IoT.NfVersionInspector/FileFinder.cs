using System.IO.Compression;

namespace Mako.IoT.NFVersionInspector
{
    public class FileFinder
    {
        public static IEnumerable<string> FindNfprojFiles(string rootPath)
        {
            return Directory.GetFiles(rootPath, "*.nfproj", SearchOption.AllDirectories);
        }

        public static IEnumerable<Package> FindNuspecFiles(string rootPath, IEnumerable<string> packages)
        {
            var files = new List<Package>();
            var packagesFolder = Directory.GetDirectories(rootPath, "packages", SearchOption.AllDirectories).First();
            foreach (var p in packages)
            {
                var path = Path.Combine(packagesFolder, p);
                if (Directory.Exists(path))
                {
                    var nupkg = Directory
                        .GetFiles(path, "*.nupkg", SearchOption.TopDirectoryOnly)
                        .FirstOrDefault();
                    if (nupkg != null)
                    {
                        using var zip = ZipFile.OpenRead(nupkg);
                        var nuspecEntry = zip.Entries.Where(e => e.Name.EndsWith(".nuspec")).FirstOrDefault();
                        if (nuspecEntry != null)
                        {
                            using var reader = new StreamReader(nuspecEntry.Open());

                            files.Add(NuspecParser.Parse(reader));
                        }
                    }
                }
            }

            return files;
        }
    }
}
