using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAppInteractive
{
    public struct loanPeriod
    {
       
        private DateTime _borrowedOn;
        private DateTime _returnedOn;
        private DateTime _dueDate;

        
        // Gets or sets the date the asset was checked out
        public DateTime BorrowedOn
        {
            get { return _borrowedOn; }
            set  { _borrowedOn = value; }
        }

        
        public DateTime ReturnedOn
        {
            get { return _returnedOn; }
            set
            {
                if (value < _borrowedOn && value != DateTime.MinValue)
                    throw new ArgumentException("Return date cannot be earlier than borrow date.");
                _returnedOn = value;
            }
        }

        
        //Gets or sets the date the asset is due to be returned
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

       

        
        // Calculates total duration of the loan using lambda expression syntax.
        
        public TimeSpan LoanDuration => ReturnedOn - BorrowedOn;

        
        //Calculates the late period using a conditional expression 
        
        public TimeSpan LatePeriod => ReturnedOn > DueDate ? ReturnedOn - DueDate : TimeSpan.Zero;

        
        //Constructor ensures the struct is initialized with valid timing data.
        
        public loanPeriod(DateTime borrowedOn, int maxBorrowDays)
        {
            _borrowedOn = borrowedOn;
            _dueDate = borrowedOn.AddDays(maxBorrowDays);
            _returnedOn = DateTime.MinValue; // Indicates not yet returned
        }

        public loanPeriod(DateTime now, DateTime dateTime) : this()
        {
        }


        //Overrides ToString to provide a string representation of the loan status.

        public override string ToString()
        {
            return $"Borrowed: {BorrowedOn:d}, Due: {DueDate:d}, Late: {LatePeriod.Days} days";
        }

        
    }
}
