
using CommandLine;

namespace Mako.IoT.NFVersionInspector.Commands
{
    [Verb("proj", HelpText = "Display all referenced nuget packages and native assemblies used in solution.")]
    public class ProjOptions
    {
        [Option('t', "path",Default = true, Required = true, HelpText = "solution path")]
        public string Path { get; set; }
    }

}
