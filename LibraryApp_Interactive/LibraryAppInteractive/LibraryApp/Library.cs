namespace LibraryAppInteractive;

/// <summary>
/// Defines the Library class used to manage the library books and assets.
///
/// NOTE: A single object/instance of this class (called a "singleton") is created and shared automatically
/// with the two pages in the application through the process of Dependency Injection handled and configured
/// in MauiProgram class.  
/// </summary>
public class Library
{
    private List<Book> _bookList = new List<Book>();
    private int _libIDGeneratorSeed = 100;
    private const int DEFAULT_LIBID_START = 100;

    public Book RegisterBook(string name, string isbn, string[] authors, BookType type, int nCopies)
    {
        Book newBook = type switch
        {
            BookType.Paper => new PaperBook(name, isbn),
            BookType.Digital => new DigitalBook(name, isbn),
            _ => throw new Exception("Invalid Book Type")
        };

        // Logic to add authors and create initial assets 
        for (int i = 0; i < nCopies; i++)
        {
            newBook.Assets.ToList().Add(new LibraryAsset(_libIDGeneratorSeed++, newBook));
        }

        _bookList.Add(newBook);
        return newBook;
    }

    public Book FindBookByName(string name) => _bookList.FirstOrDefault(b => b.Name.Contains(name));
}