using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Model
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Publisher { get; set; }
        public int Quantity { get; set; }

        //public Book() { }
    }
}
