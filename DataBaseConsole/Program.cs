using System.CommandLine;
using DspLib.DataBase;

var rootCommand = new RootCommand("DspLib Database CommandLine");
var dataSourceOption = new Option<string?>(
    "--dataSource",
    "DataSource");
dataSourceOption.AddAlias("-h");
var environmentOption = new Option<bool>(
    "--environment",
    "Environment");
environmentOption.AddAlias("-e");
var startSeedOption = new Option<int>(
    "--startSeed",
    "Num of startSeed");
var maxSeedOption = new Option<int>(
    "--maxSeed",
    "Num of maxSeed");
var starCountOption = new Option<int>(
    "--starCount",
    "Num of starCount");
var initDatabaseCommand = new Command("init", "Init DspLib Database");
initDatabaseCommand.AddOption(dataSourceOption);
initDatabaseCommand.AddOption(environmentOption);
initDatabaseCommand.SetHandler(InitDatabase!,
    dataSourceOption, environmentOption);
rootCommand.AddCommand(initDatabaseCommand);

var insertDatabaseCommand = new Command("insert", "insert DspLib Database");
insertDatabaseCommand.AddOption(dataSourceOption);
insertDatabaseCommand.AddOption(environmentOption);
insertDatabaseCommand.AddOption(startSeedOption);
insertDatabaseCommand.AddOption(maxSeedOption);
insertDatabaseCommand.AddOption(starCountOption);
insertDatabaseCommand.SetHandler(
    async (dataSource, startSeed, maxSeed, starCount, environment) =>
        await InsertDatabase(dataSource!, startSeed, maxSeed, starCount, environment),
    dataSourceOption, startSeedOption, maxSeedOption, starCountOption,
    environmentOption);
rootCommand.AddCommand(insertDatabaseCommand);

return await rootCommand.InvokeAsync(args);

void InitDatabase(string dataSource, bool environment)
{
    var connectionString = environment
        ? new ConnectionString().GetString()
        : new ConnectionString(dataSource).GetString();
    new DatabaseInitializer(connectionString).CreateTable();
}

async Task InsertDatabase(string dataSource, int startSeed, int maxSeed,
    int starCount, bool environment)
{
    var connectionString = environment
        ? new ConnectionString().GetString()
        : new ConnectionString(dataSource).GetString();
    await new DatabaseInserter(connectionString).InsertGalaxiesInfoInBatch(startSeed, maxSeed, starCount);
}