namespace MauiAppBookStore;

public partial class App : Application
{
	public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
