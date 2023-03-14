using MauiAppBookStore.Models;
using MauiAppBookStore.Services;

namespace MauiAppBookStore.Views;

public partial class AddBook : ContentPage
{
	public AddBook()
	{
		InitializeComponent();
	}

    private async void OnAddBookClicked2(object sender, EventArgs e)
    {
        //BookListInfo book = new BookListInfo()
        //{
        //    Title = TitleEntry.Text,
        //    Rating = Convert.ToInt32(RatingEntry.Text),
        //    Favorite = FavoriteSwitch.IsToggled,
        //    GenreId = Convert.ToInt32(GenreEntry.Text),
        //    AuthorId = Convert.ToInt32(AuthorEntry.Text)
        //};

        //bool success = await bookService.AddBook(book);

        //if (success)
        //{
        //    await DisplayAlert("Success", "Book added successfully", "OK");
        //}
        //else
        //{
        //    await DisplayAlert("Error", "Failed to add book", "OK");
        //}
    }
}