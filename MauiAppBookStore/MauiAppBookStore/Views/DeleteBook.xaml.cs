using MauiAppBookStore.Models;
using MauiAppBookStore.Services;
using System.Collections.ObjectModel;

namespace MauiAppBookStore.Views;

public partial class DeleteBook : ContentPage
{
    public int BookId { get; set; }

    private readonly IBookRepository _bookService;
    private ObservableCollection<BookListInfo> _books;
    public ObservableCollection<BookListInfo> Books
    {
        get { return _books; }
        set { _books = value; }
    }

    public DeleteBook()
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

    private async void OnDeleteBookClicked2(object sender, EventArgs e)
    {
        if (BookId != 0)
        {
            var result = await _bookService.DeleteBook(BookId);
            await LoadBooks();
        }
        else
        {
            await DisplayAlert("Warning", "Book id dosen't exist", "Ok");
        }
    }
}
