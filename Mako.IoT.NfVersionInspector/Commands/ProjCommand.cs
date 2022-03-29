using Mako.IoT.NFVersionInspector.Extensions;

namespace Mako.IoT.NFVersionInspector.Commands
{
    public class ProjCommand
    {
        public static int Execute(ProjOptions options)
        {
            var existingPackages = DependencyFinder.FindPackages(options.Path).Flatten().ToArray();
            Console.WriteLine();
            Console.WriteLine("Assemblies referenced in the solution:");
            OutputPackages(existingPackages);

            if (!String.IsNullOrWhiteSpace(options.Port) || !String.IsNullOrWhiteSpace(options.BoardName))
            {
                Console.WriteLine();
                Console.WriteLine("Checking board compatibility...");

                Package[]? nativePackages = null;

                if (!String.IsNullOrWhiteSpace(options.BoardName))
                {
                    nativePackages = Storage.LoadBoardInfo(options.BoardName).ToArray();
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(options.Port))
                        throw new ArgumentNullException(nameof(options.Port));
                    nativePackages = DeviceExplorer.GetBoardInfo(options.Port).NativePackages().ToArray();
                }

                foreach (var projectPackage in existingPackages.Where(p=>!p.IsNative))
                {
                    Console.Write($"Checking {projectPackage.Id} {projectPackage.Version} - ");
                    if (!DependencyFinder.GetDependenciesFromNuget(projectPackage.Id, projectPackage.Version).Flatten()
                        .Where(p=>p.IsNative)
                        .Except(nativePackages)
                        .Any())
                        
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("OK ");

                        if (options.UpgradeCheck)
                        {
                            var compatibleVersion =
                                DependencyFinder.FindCompatibleVersion(projectPackage.Id, nativePackages, false, true);

                            if (compatibleVersion != null && compatibleVersion != projectPackage.Version)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($"(possible upgrade to {compatibleVersion})");
                            }
                        }

                        Console.WriteLine();

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"Not compatible ");
                        Console.ForegroundColor = ConsoleColor.White;

                        var compatibleVersion =
                            DependencyFinder.FindCompatibleVersion(projectPackage.Id, nativePackages, false, true);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(compatibleVersion == null
                            ? "(no compatible version found)"
                            : $"(upgrade to {compatibleVersion})");
                    }

                    Console.ForegroundColor = ConsoleColor.White;

                }
            }

            return 0;
        }

        static void OutputPackages(IEnumerable<Package> packages)
        {
            foreach (var package in packages)
            {
                Console.WriteLine($"{package.Id} {package.Version} {package.Checksum}");
            }
        }
    }
}
