using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAppInteractive
{
    public class LibraryAsset
    {
        private int _libID;
        private AssetStatus _status;
        private loanPeriod _loan;
        private Book _book; // Association to the parent Book 

        public int LibID => _libID;
        public AssetStatus Status { get => _status; set => _status = value; }
        public loanPeriod Loan { get => _loan; set => _loan = value; }
        public bool IsAvailable => _status == AssetStatus.Available;

        public LibraryAsset(int libID, Book book)
        {
            _libID = libID;
            _book = book;
            _status = AssetStatus.Available;
        }
    }
}
