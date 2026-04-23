using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAppInteractive
{
    public class Paperbook : Book
    {
        public const int MAX_BORROW_DAYS = 30;
        public const decimal LATE_PENALTY_PER_DAY = 0.25m;

        public Paperbook(string name, string isbn) : base(name, isbn) { }

        public override LibraryAsset BorrowBook()
        {
            var asset = FindNextAvailableAsset();
            if (asset != null)
            {
                asset.Status = AssetStatus.Loaned;
                asset.Loan = new loanPeriod(DateTime.Now, DateTime.Now.AddDays(MAX_BORROW_DAYS));
            }
            return asset;
        }

        public override bool ReturnBook(int libID, out TimeSpan latePeriod, out decimal penalty)
        {
            // Implementation logic for returning and calculating penalties
            latePeriod = TimeSpan.Zero;
            penalty = 0m;
            return true;
        }
    }
   
}
