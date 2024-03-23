using System.CommandLine;
using DspLib.DataBase;

var rootCommand = new RootCommand("DspLib Database CommandLine");
var hostOption = new Option<string?>(
    "--host",
    "Host");
hostOption.AddAlias("-h");
var databaseOption = new Option<string?>(
    "--database",
    "Database");
databaseOption.AddAlias("-d");
var usernameOption = new Option<string?>(
    "--username",
    "Username");
usernameOption.AddAlias("-u");
var passwordOption = new Option<string?>(
    "--password",
    "Password");
passwordOption.AddAlias("-p");
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
initDatabaseCommand.AddOption(hostOption);
initDatabaseCommand.AddOption(databaseOption);
initDatabaseCommand.AddOption(usernameOption);
initDatabaseCommand.AddOption(passwordOption);
initDatabaseCommand.AddOption(environmentOption);
initDatabaseCommand.SetHandler(InitDatabase!,
    hostOption, databaseOption, usernameOption, passwordOption, environmentOption);
rootCommand.AddCommand(initDatabaseCommand);

var insertDatabaseCommand = new Command("insert", "insert DspLib Database");
insertDatabaseCommand.AddOption(hostOption);
insertDatabaseCommand.AddOption(databaseOption);
insertDatabaseCommand.AddOption(usernameOption);
insertDatabaseCommand.AddOption(passwordOption);
insertDatabaseCommand.AddOption(environmentOption);
insertDatabaseCommand.AddOption(startSeedOption);
insertDatabaseCommand.AddOption(maxSeedOption);
insertDatabaseCommand.AddOption(starCountOption);
insertDatabaseCommand.SetHandler(
    (host, database, username, password, startSeed, maxSeed, starCount, environment) =>
        InsertDatabase(host!, database!, username!, password!, startSeed, maxSeed, starCount, environment),
    hostOption, databaseOption, usernameOption, passwordOption, startSeedOption, maxSeedOption, starCountOption,
    environmentOption);
rootCommand.AddCommand(insertDatabaseCommand);

return await rootCommand.InvokeAsync(args);

void InitDatabase(string host, string database, string username, string password, bool environment)
{
    var connectionString = environment
        ? new ConnectionString().GetString()
        : new ConnectionString(host, database, username, password).GetString();
    new DatabaseInitializer(connectionString).CreateTable();
}

void InsertDatabase(string host, string database, string username, string password, int startSeed, int maxSeed,
    int starCount, bool environment)
{
    var connectionString = environment
        ? new ConnectionString().GetString()
        : new ConnectionString(host, database, username, password).GetString();
    new DatabaseInserter(connectionString).InsertGalaxiesInfoInBatch(startSeed, maxSeed, starCount);
}