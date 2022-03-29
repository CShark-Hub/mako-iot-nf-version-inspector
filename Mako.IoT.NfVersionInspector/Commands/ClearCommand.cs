namespace Mako.IoT.NFVersionInspector.Commands
{
    public class ClearCommand
    {
        public static int Execute(ClearOptions options)
        {
            if (options.ClearBoards)
                Storage.ClearBoardsInfo();

            if (options.ClearPackages)
                Storage.ClearPackages();

            return 0;
        }
    }
}
