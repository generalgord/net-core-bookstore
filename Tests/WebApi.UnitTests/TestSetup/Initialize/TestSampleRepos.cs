using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class TestSampleRepos
    {
        public static List<Genre> Genres
        {
            get
            {
                var genres = new List<Genre>
                {
                    new Genre { Name = "Personal Growth" },
                    new Genre { Name = "Science Fiction" },
                    new Genre { Name = "Noval" },
                    new Genre { Name = "Romance" },
                    new Genre { Name = "Detective" },
                    new Genre { Name = "Classics" },
                    new Genre { Name = "Adventure" },
                    new Genre { Name = "Science" },
                    new Genre { Name = "Family" },
                };
                return genres;
            }
        }

        public static List<Book> Books
        {
            get
            {
                var books = new List<Book>
                {
                    new Book
                    {
                        GenreId = 1,
                        Title = "Mastering C# 8.0",
                        PageCount = 305,
                        PublishDate = DateTime.Parse("2015-08-09")
                    },
                    new Book
                    {
                        GenreId = 2,
                        Title = "Entity Framework Tutorial",
                        PageCount = 210,
                        PublishDate = DateTime.Parse("2018-03-04")
                    },
                    new Book
                    {
                        GenreId = 3,
                        Title = "ASP.NET 4.0 Programming",
                        PageCount = 560,
                        PublishDate = DateTime.Parse("2012-01-03")
                    },
                    new Book
                    {
                        GenreId = 4,
                        Title = "Let us C",
                        PageCount = 110,
                        PublishDate = DateTime.Parse("2019-05-05")
                    },
                    new Book
                    {
                        GenreId = 2,
                        Title = "Let us C++",
                        PageCount = 440,
                        PublishDate = DateTime.Parse("2014-06-03")
                    },
                    new Book
                    {
                        GenreId = 3,
                        Title = "Let us C#",
                        PageCount = 980,
                        PublishDate = DateTime.Parse("2021-03-04")
                    }
                };
                return books;
            }
        }

        public static List<Author> Authors
        {
            get
            {
                var authors = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Ahmet",
                        LastName = "Selim",
                        DateOfBirth = DateTime.Parse("1969-06-03")
                    },
                    new Author
                    {
                        FirstName = "Necati",
                        LastName = "Tugel",
                        DateOfBirth = DateTime.Parse("1980-03-01")
                    },
                    new Author
                    {
                        FirstName = "Saliha",
                        LastName = "Selman",
                        DateOfBirth = DateTime.Parse("1960-07-11")
                    },
                };
                return authors;
            }
        }
    }
}
