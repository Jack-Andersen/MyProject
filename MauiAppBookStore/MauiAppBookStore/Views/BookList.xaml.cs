using MauiAppBookStore.Models;
using MauiAppBookStore.Services;
using MauiAppBookStore.Views;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;

namespace MauiAppBookStore;

public partial class BookList : ContentPage
{

    private readonly IBookRepository _bookService;
    private ObservableCollection<BookListInfo> _books;
    public ObservableCollection<BookListInfo> Books { 
        get { return _books; } 
        set { _books = value; } 
    }

    public BookList()
	{
        InitializeComponent();

        // Initialize book service
        _bookService = new Bookservice();

        // Set up list view
        _books = new ObservableCollection<BookListInfo>();
        BindingContext = this;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        // Load books when page appears
        await LoadBooks();
    }

    private async Task LoadBooks()
    {
        // Clear current books
        _books.Clear();

        // Get books from service
        var books = await _bookService.GetBookList();

        // Add books to observable collection
        foreach (var book in books)
        {
            _books.Add(book);
        }
    }

    private async void OnToggleFavorite(object sender, ToggledEventArgs e)
    {

        var bookId = ((sender as Switch)?.BindingContext as BookListInfo)?.BookId;
        if (bookId.HasValue)
        {
            await _bookService.UpdateBook(bookId.Value, CustomerInfo.Current.CustomerId, e.Value);
        }
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddBook());
    }

    private async void OnDeleteBookClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DeleteBook());
    }
}