using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DspLib.DataBase;

namespace GUI.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    public void InitButtonClicked(object source, RoutedEventArgs args)
    {
        Debug.WriteLine($"Click! {Host.Text} {DataBase.Text} {Username.Text} {Password.Text}");
        InitDatabase(Host.Text!, DataBase.Text!, Username.Text!, Password.Text!);
    }

    public async void InsertButtonClicked(object source, RoutedEventArgs args)
    {
        await InsertDatabase(Host.Text!, DataBase.Text!, Username.Text!, Password.Text!,
            int.Parse(StartSeed.Text!), int.Parse(MaxSeed.Text!), int.Parse(StarCount.Text!));
    }

    private void InitDatabase(string host, string database, string username, string password)
    {
        var connectionString = new ConnectionString(host, database, username, password).GetString();
        new DatabaseInitializer(connectionString).CreateTable();
    }

    private async Task InsertDatabase(string host, string database, string username, string password, int startSeed,
        int maxSeed,
        int starCount)
    {
        var connectionString = new ConnectionString(host, database, username, password).GetString();
        await new DatabaseInserter(connectionString).InsertGalaxiesInfoInBatch(startSeed, maxSeed, starCount);
    }
}