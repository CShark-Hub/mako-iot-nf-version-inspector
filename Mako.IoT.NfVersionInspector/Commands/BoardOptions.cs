
using CommandLine;

namespace Mako.IoT.NFVersionInspector.Commands
{
    [Verb("board", HelpText = "Get native assemblies installed on board. If board name is specified, stores result in a file.")]
    public class BoardOptions
    {
        [Option('p', "port", Required = true, HelpText = "COM port")]
        public string Port { get; set; }

        [Option('n', "name", Required = false, HelpText = "board name")]
        public string Name { get; set; }
    }

}
