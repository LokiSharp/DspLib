namespace DspLib.DataBase;

public class ConnectionString(string host, string database, string username, string password)
{
    public ConnectionString() : this(
        Environment.GetEnvironmentVariable("Host") ?? throw new InvalidOperationException(),
        Environment.GetEnvironmentVariable("Database") ?? throw new InvalidOperationException(),
        Environment.GetEnvironmentVariable("Username") ?? throw new InvalidOperationException(),
        Environment.GetEnvironmentVariable("Password") ?? throw new InvalidOperationException()
    )
    {
    }

    public string GetString()
    {
        return $"Host={host};" +
               $"Database={database};" +
               $"Username={username};" +
               $"Password={password};";
    }
}