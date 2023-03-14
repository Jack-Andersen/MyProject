namespace MauiAppBookStore;

using MauiAppBookStore.Models;
using MauiAppBookStore.Views;
using Microsoft.Maui.Controls;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
    }

    private async void OnBookListButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BookList());
    }

    private async void OnFavoritesButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Favoites());
    }

    private async void OnGeoLocation(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GeoLocation());
    }
}


