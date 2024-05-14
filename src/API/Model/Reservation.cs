using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int UserId { get; init; }
        public int BookId { get; init; }
        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                if (EndDate == default) // 1st-- always true at instantiation time
                { EndDate = value.AddDays(30); }

                if (value < EndDate) //checks valid enddate (when updating res)
                { startDate = value; }
            }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                if (value >= StartDate)
                { endDate = value; } // 2nd-- order of assignment matters
            }
        }

        public Reservation() { }

        public Reservation(int bookId, int userId, DateTime startDate, DateTime endDate = default)
        {
            //ReservationId = library.LastUsedIds["Reservation"];
            //library.LastUsedIds["Reservation"]++;
            UserId = userId;
            BookId = bookId;
            StartDate = startDate;
            if (endDate == default)
            {
                EndDate = startDate.AddDays(30);
            }
            else
            {
                EndDate = endDate;
            }
        }
    }
}
