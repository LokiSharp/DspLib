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
        Debug.WriteLine($"Click! {DataSource.Text}");
        InitDatabase(DataSource.Text!);
    }

    public async void InsertButtonClicked(object source, RoutedEventArgs args)
    {
        await InsertDatabase(DataSource.Text!,
            int.Parse(StartSeed.Text!), int.Parse(MaxSeed.Text!), int.Parse(StarCount.Text!));
    }

    private void InitDatabase(string dataSource)
    {
        var connectionString = new ConnectionString(dataSource).GetString();
        new DatabaseInitializer(connectionString).CreateTable();
    }

    private async Task InsertDatabase(string dataSource, int startSeed,
        int maxSeed,
        int starCount)
    {
        var connectionString = new ConnectionString(dataSource).GetString();
        await new DatabaseInserter(connectionString).InsertGalaxiesInfoInBatch(startSeed, maxSeed, starCount);
    }
}