using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Ali",
                        Surname = "Veli",
                        DateOfBirth = new DateTime(1990, 12, 31),
                        GenreId = 1
                    },
                    new Author
                    {
                        Name = "Ahmet",
                        Surname = "Mehmet",
                        DateOfBirth = new DateTime(1989, 06, 12),
                        GenreId = 2
                    },
                    new Author
                    {
                        Name = "Sami",
                        Surname = "Sezgin",
                        DateOfBirth = new DateTime(1994, 04, 17),
                        GenreId = 3
                    }
                );
                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new System.DateTime(2001, 6, 12),
                        AuthorId=1
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new System.DateTime(2020, 5, 13),
                        AuthorId=2
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 2,
                        PageCount = 500,
                        PublishDate = new System.DateTime(2006, 7, 10),
                        AuthorId=3
                    }
                );


                context.SaveChanges();
            }
        }
    }
}