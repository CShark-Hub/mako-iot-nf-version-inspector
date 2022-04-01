using Mako.IoT.NFVersionInspector.Extensions;

namespace Mako.IoT.NFVersionInspector.Commands
{
    public class BoardCommand
    {
        public static int Execute(BoardOptions options)
        {
            if (options.List)
            {
                foreach (var boardName in Storage.ListBoardInfo())
                {
                    Console.WriteLine(boardName);
                }
            }

            if (!String.IsNullOrWhiteSpace(options.Port))
            {
                var info = DeviceExplorer.GetBoardInfo(options.Port);

                Console.WriteLine(info);

                if (!String.IsNullOrWhiteSpace(options.Name))
                {
                    var packages = info.NativePackages();

                    Storage.SaveBoardInfo(options.Name, packages);
                }
            }

            return 0;
        }
    }
}
