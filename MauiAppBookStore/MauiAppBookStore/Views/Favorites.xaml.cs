using MauiAppBookStore.Models;
using MauiAppBookStore.Services;
using System.Collections.ObjectModel;

namespace MauiAppBookStore;

public partial class Favoites : ContentPage
{
    private readonly IBookRepository _bookService;
    private ObservableCollection<BookListInfo> _books;
    public ObservableCollection<BookListInfo> Books
    {
        get { return _books; }
        set { _books = value; }
    }

    public Favoites()
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


        // Get all books from service
        var allBooks = await _bookService.GetBookList();

        // Get books from service
        var favoriteBooks = allBooks.Where(book => book.Favorite);

        // Add books to observable collection
        foreach (var book in favoriteBooks)
        {
            _books.Add(book);
        }
    }
}