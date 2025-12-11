
using System.CommandLine;
using System.Diagnostics;
using AdventOfCode2025;

RootCommand rootCommand = new("AoC Puzzle Runner");

Option<int?> dayOption = new("--day", "-d")
{
    Required = false,
    Description = "Day you want to run"
};

Option<string?> inputOption = new("--input", "-i")
{
    Required = false,
    Description = "Input file (must select day)"
};

rootCommand.Options.Add(dayOption);
rootCommand.Options.Add(inputOption);

rootCommand.SetAction(parseResult =>
{
    var day = parseResult.GetValue<int?>(dayOption);
    var input = parseResult.GetValue<string?>(inputOption);
    Runner.Run(day, input);
});

if (Debugger.IsAttached)
{
    Runner.Run(6, "test.txt");
    return 0;
}
else
{
    return rootCommand.Parse(args).Invoke();
}
