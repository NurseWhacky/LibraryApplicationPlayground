using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public bool IsAvailable { get; set; }
    }
}
