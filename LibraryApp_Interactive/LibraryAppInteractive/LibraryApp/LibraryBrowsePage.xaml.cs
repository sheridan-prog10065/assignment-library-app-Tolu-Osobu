namespace LibraryAppInteractive;

public partial class LibraryBrowsePage : ContentPage
{
    private Book _selectedBook;

    private Library _library; // Usually injected via Dependency Injection 
    

      public LibraryBrowsePage(Library library)
     {
        InitializeComponent();
        _library = library;

     }
    private async void OnSearchBook(object sender, EventArgs e)
    {
        try
        {
            string query = _entSearchName.Text;
            if (string.IsNullOrWhiteSpace(query))
                throw new Exception("Please enter a book name to search.");

            _selectedBook = _library.FindBookByName(query);

            if (_selectedBook != null)
            {
                // Update UI with Book details 
                _lblBookTitle.Text = $"Title: {_selectedBook.Name}";
                _lblBookISBN.Text = $"ISBN: {_selectedBook.ISBN}";
                _lblBookAuthors.Text = $"Authors: {string.Join(", ", _selectedBook.Authors)}";

                // Refresh asset list 
                _lstAssets.ItemsSource = _selectedBook.Assets;
            }
            else
            {
                throw new Exception($"Book '{query}' not found in the catalogue.");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Search Error",
                                    ex.Message,
                                    "OK"); 
            }
    }

    
    //Handles borrowing the first available copy of the selected book 
    
    private async void OnBorrowBook(object sender, EventArgs e)
    {
        try
        {
            if (_selectedBook == null)
                throw new Exception("Please search for and select a book first.");

            LibraryAsset asset = _selectedBook.BorrowBook();

            if (asset != null)
            {
                 await DisplayAlertAsync(
                    "Loan Confirmed",
                    $"Book borrowed successfully. Due date: {asset.Loan.DueDate:d}. Please use ID {asset.LibID} for return.",
                    "OK");
                _lstAssets.ItemsSource = null; // Force UI refresh
                _lstAssets.ItemsSource = _selectedBook.Assets;
            }
            else
            {
                throw new Exception("No copies are currently available for loan.");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Loan Error", ex.Message, "OK");
        }
    }

    
    //Handles returning a book using its unique Library ID 
    
    private async void OnReturnBook(object sender, EventArgs e)
    {
        try
        {
            if (_selectedBook == null)
                throw new Exception("Please select the book category first.");

            if (int.TryParse(_entReturnID.Text, out int libID))
            {
                bool success = _selectedBook.ReturnBook(libID, out TimeSpan late, out decimal penalty);

                if (success)
                {
                    string message = $"Book returned successfully. Late: {late.Days} days. Penalty: {penalty:C}";
                    await DisplayAlertAsync("Return Processed", message, "OK");
                    _lstAssets.ItemsSource = null; // Refresh list
                    _lstAssets.ItemsSource = _selectedBook.Assets;
                }
                else
                {
                    throw new Exception("Return failed. Ensure the ID is correct for this book.");
                }
            }
            else
            {
                throw new Exception("Please enter a valid numerical Library ID.");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Return Error", ex.Message, "OK");
        }
    }
}