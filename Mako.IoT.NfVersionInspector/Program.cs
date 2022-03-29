using CommandLine;
using Mako.IoT.NFVersionInspector.Commands;

Parser.Default.ParseArguments<BoardOptions, ProjOptions, CheckOptions, FindOptions, ClearOptions>(args)
    .MapResult(
        (BoardOptions o) => BoardCommand.Execute(o),
        (ProjOptions o) => ProjCommand.Execute(o),
        (CheckOptions o) => CheckCommand.Execute(o),
        (FindOptions o) => FindCommand.Execute(o),
        (ClearOptions o) => ClearCommand.Execute(o),
        errors => 1);
