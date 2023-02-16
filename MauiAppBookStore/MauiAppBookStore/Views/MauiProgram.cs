using MauiAppBookStore.ViewModels;

namespace MauiAppBookStore;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<BookList>();
        builder.Services.AddSingleton<Favoites>();

        return builder.Build();
	}
}
