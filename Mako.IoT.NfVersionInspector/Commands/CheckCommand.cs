
using Mako.IoT.NFVersionInspector.Extensions;

namespace Mako.IoT.NFVersionInspector.Commands
{
    public class CheckCommand
    {
        public static int Execute(CheckOptions options)
        {
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

            var proposedPackages = DependencyFinder.GetDependenciesFromNuget(options.PackageId, options.PackageVersion, options.RefreshCache).Flatten();

            foreach (var package in proposedPackages.Where(p=>p.IsNative))
            {
                Console.ForegroundColor = nativePackages.Contains(package) ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(package);
            }

            Console.ForegroundColor = ConsoleColor.White;

            return 0;
        }
    }
}
