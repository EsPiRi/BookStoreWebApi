using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 6, 12),
                        AuthorId = 1
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new DateTime(2020, 5, 13),
                        AuthorId = 2
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 500,
                        PublishDate = new DateTime(2006, 7, 10),
                        AuthorId = 3
                    }
                );
        }
    }
}