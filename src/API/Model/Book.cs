﻿using API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace API.Model
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public string Publisher { get; set; }
        public int Quantity { get; set; }

        public Book() { }

        public Book(int id, BookDTO bookDto)
        {
            Id = id;
            Title = bookDto.Title;
            AuthorName = bookDto.AuthorName;
            AuthorSurname = bookDto.AuthorSurname;
            Publisher = bookDto.Publisher;
            Quantity = bookDto.Qty;
        }

        //public Book(int id, string title, string authorName, string authorSurname, string publisher, int quantity)
        //{
        //    //BookId = library.LastUsedIds["Book"];
        //    BookId = id;
        //    Title = title;
        //    AuthorName = authorName;
        //    AuthorSurname = authorSurname;
        //    Publisher = publisher;
        //    Quantity = quantity;

        //    //library.Books.Add(this);
        //    //library.LastUsedIds["Book"]++;
        //}

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Book)
            {
                return false;
            }
            return ((Book)obj).Title == Title && ((Book)obj).AuthorName == AuthorName && ((Book)obj).AuthorSurname == AuthorSurname && ((Book)obj).Publisher == Publisher;
        }
    }
}
