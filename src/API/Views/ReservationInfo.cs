using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Views
{
    public record ReservationInfo(string BookTitle, string Username, DateTime EndDate)
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Book title: {BookTitle}, \n");
            sb.Append($"User: {Username}, \n");
            sb.Append($"Expires at: {EndDate.DayOfWeek}.");
            return sb.ToString();
        }
    }
}
