﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public class Reservation
    {
        public int ReservationId { get; init; }
        public int UserId { get; init; }
        public int BookId { get; init; }
        public DateTime StartDate { get; init; } = DateTime.Now;
        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                if (value == null)
                { endDate = StartDate.AddDays(30); }
                if (value >= StartDate && value <= StartDate.AddDays(30))
                {
                    endDate = value;
                }
                else
                { endDate = StartDate.AddDays(30); }
            }
        }

        public Reservation()
        {
            StartDate = DateTime.Now;
            EndDate = StartDate.AddDays(30);
        }

        //public Reservation()
    }
}
