namespace DspLib.DataBase;

public class ConnectionString(string dataSource)
{
    public ConnectionString() : this(
        Environment.GetEnvironmentVariable("dataSource") ?? throw new InvalidOperationException()
    )
    {
    }

    public string GetString()
    {
        return $"Data Source={dataSource};";
    }
}