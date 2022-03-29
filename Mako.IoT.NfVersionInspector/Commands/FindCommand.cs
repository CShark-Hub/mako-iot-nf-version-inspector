
using Mako.IoT.NFVersionInspector.Extensions;

namespace Mako.IoT.NFVersionInspector.Commands
{
    public class FindCommand
    {
        public static int Execute(FindOptions options)
        {
            IEnumerable<Package>? nativePackages = null;

            if (!String.IsNullOrWhiteSpace(options.BoardName))
            {
                nativePackages = Storage.LoadBoardInfo(options.BoardName);
            }
            else
            {
                if (String.IsNullOrWhiteSpace(options.Port))
                    throw new ArgumentNullException(nameof(options.Port));
                nativePackages = DeviceExplorer.GetBoardInfo(options.Port).NativePackages();
            }

            var version = DependencyFinder.FindCompatibleVersion(options.PackageId, nativePackages, options.RefreshCache);
            if (version == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Compatible package {options.PackageId} not found");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Compatible package {options.PackageId} {version}");
            }

            Console.ForegroundColor = ConsoleColor.White;
            return 0;
        }
    }
}
