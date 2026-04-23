namespace LibraryAppInteractive
{
    public class DigitalBook : Book
    {
        // Field variables as specified in the DigitalBook UML 
        private int _maxBorrowDays;
        private float _latePenaltyPerDay;

        
        //Initializes a new instance of DigitalBook and its digital-specific fields
        public DigitalBook(string bookName, string bookISBN) : base(bookName, bookISBN)
        {
            // Initialization logic for digital-specific fields
            DetermineLoanLicense();
        }

        //Specific method to determine digital licensing terms.
      
        public void DetermineLoanLicense()
        {
            _maxBorrowDays = 14; // Example assignment
            _latePenaltyPerDay = 0.0f; // Often zero for automatic digital returns
        }

        // Implementation of the abstract method to borrow a digital copy.
        
        public override LibraryAsset BorrowBook()
        {
            LibraryAsset asset = FindNextAvailableAsset();
            if (asset != null)
            {
                asset.Status = AssetStatus.Loaned;
                asset.Loan = new loanPeriod(DateTime.Now, _maxBorrowDays);
            }
            return asset;
        }

        public override bool ReturnBook(int libID, out TimeSpan latePeriod, out decimal penalty)
        {
            throw new NotImplementedException();
        }
    }


}