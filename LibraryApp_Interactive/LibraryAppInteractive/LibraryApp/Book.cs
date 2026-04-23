using System;
using System.Collections.Generic;
using System.Text;
using LibraryAppInteractive;

namespace LibraryAppInteractive
{
    public abstract class Book
    {
        private string _bookName;
        private string _bookISBN;
        private List<string> _bookAuthorList;
        protected List<LibraryAsset> _libAssetList; // Encapsulated asset collection 

        public string Name => _bookName;
        public string ISBN => _bookISBN;
        public IEnumerable<string> Authors => _bookAuthorList;
        public IEnumerable<LibraryAsset> Assets => _libAssetList;

        public Book(string bookName, string bookISBN)
        {
            _bookName = bookName;
            _bookISBN = bookISBN;
            _bookAuthorList = new List<string>();
            _libAssetList = new List<LibraryAsset>();
        }

        // Abstract methods to be implemented by PaperBook and DigitalBook 
        public abstract LibraryAsset BorrowBook();
        public abstract bool ReturnBook(int libID, out TimeSpan latePeriod, out decimal penalty);

        protected LibraryAsset FindNextAvailableAsset() => _libAssetList.FirstOrDefault(a => a.IsAvailable);

       
    }

    
    
}
