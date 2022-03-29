using Mako.IoT.NFVersionInspector.Extensions;

namespace Mako.IoT.NFVersionInspector.Commands
{
    public class ProjCommand
    {
        public static int Execute(ProjOptions options)
        {
            var existingPackages = DependencyFinder.FindPackages(options.Path).Flatten();
            OutputPackages(existingPackages);

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
