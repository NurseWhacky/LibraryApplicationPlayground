using API.Model;
using Bogus;
using System.Data.SqlClient;
using System.Reflection;




namespace DAL.SQL
{
    internal class Utils
    {
        public void PopulateDB(string connectionString)
        {
            // Create a new Faker object for each table
            Faker<Book> bookFaker = new Faker<Book>("it")
                .StrictMode(true)
                .RuleFor(b => b.Id, f => f.IndexFaker)
                .RuleFor(b => b.Title, f => f.Lorem.Sentence(3, 2))
                .RuleFor(b => b.AuthorName, f => f.Name.FirstName())
                .RuleFor(b => b.AuthorSurname, f => f.Name.LastName())
                .RuleFor(b => b.Publisher, f => f.Company.Bs().ToUpperInvariant())
                .RuleFor(b => b.Quantity, f => f.Random.Byte());
            var books = bookFaker.Generate(100);

            Faker<User> userFaker = new Faker<User>("it")
                .StrictMode(true)
                .RuleFor(u => u.Id, f => f.IndexFaker)
                .RuleFor(u => u.Username, f => f.Name.JobType())
                .RuleFor(u => u.Password, f => f.Internet.Password());
            var users = userFaker.Generate(10);

            Faker<Reservation> reservationFaker = new Faker<Reservation>("it", new Bogus.Binder(BindingFlags.Instance | BindingFlags.NonPublic))
                .StrictMode(true)
                .RuleFor(r => r.Id, f => f.IndexFaker)
                .RuleFor(r => r.BookId, f => f.Random.Int(books.First().Id, books.Last().Id))
                .RuleFor(r => r.UserId, f => f.Random.Int(users.First().Id, users.Last().Id))
                .RuleFor(r => r.StartDate, f => f.Date.Past())
                .RuleFor(r => r.EndDate, f => f.Date.Past().AddDays(f.Random.Int(1, 30)));

            var reservations = reservationFaker.Generate(100);

            // Connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert the fake data into the tables
                foreach (var book in books)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Books (Id, Title, AuthorName, AuthorSurname, Publisher, Quantity) VALUES (@Id, @Title, @AuthorName, @AuthorSurname, @Publisher, @Quantity)", connection))
                    {
                        command.Parameters.AddWithValue("@Id", book.Id);
                        command.Parameters.AddWithValue("@Title", book.Title);
                        command.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                        command.Parameters.AddWithValue("@AuthorSurname", book.AuthorSurname);
                        command.Parameters.AddWithValue("@Publisher", book.Publisher);
                        command.Parameters.AddWithValue("@Quantity", book.Quantity);
                        command.ExecuteNonQuery();
                    }
                }

                foreach (var reservation in reservations)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Reservations (Id, BookId, StartDate, EndDate) VALUES (@Id, @BookId, @StartDate, @EndDate)", connection))
                    {
                        command.Parameters.AddWithValue("@Id", reservation.Id);
                        command.Parameters.AddWithValue("@BookId", reservation.BookId);
                        command.Parameters.AddWithValue("@UserId", reservation.UserId);
                        command.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                        command.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                        command.ExecuteNonQuery();
                    }
                }

                foreach (var user in users)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Users (Id, Username, Password) VALUES (@Id, Username, @Password)", connection))
                    {
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@Name", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
