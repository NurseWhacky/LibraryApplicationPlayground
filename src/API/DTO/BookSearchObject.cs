using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class BookSearchObject
    {
        public List<string> Criteria { get; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public bool IsAvailable { get; set; }

        public BookSearchObject(string title, string author, string publisher, bool isAvalable)
        {
            Title = title;
            Author = author;
            Publisher = publisher;
            IsAvailable = isAvalable;

            // Initialize Criteria list
            Criteria = new List<string>();

            // Add non-null search criteria to the list
            if (!string.IsNullOrEmpty(Title))
            {
                Criteria.Add(Title);
            }
            if (!string.IsNullOrEmpty(Author))
            {
                Criteria.Add(Author);
            }
            if (!string.IsNullOrEmpty(Publisher))
            {
                Criteria.Add(Publisher);
            }
        }

    }
}
