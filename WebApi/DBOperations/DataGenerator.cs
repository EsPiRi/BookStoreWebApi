using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

                context.Books.AddRange(
                    new Book
                    {

                        Title = "Lean Startup", //personal growth
                        GenreId = 1,
                        PageCount = 200,
                        PublishDate = new System.DateTime(2001, 6, 12)
                    },
                    new Book
                    {

                        Title = "Herland", //science fiction
                        GenreId = 2,
                        PageCount = 250,
                        PublishDate = new System.DateTime(2020, 5, 13)
                    },
                    new Book
                    {

                        Title = "Lean Startup",
                        GenreId = 2, // science fiction
                        PageCount = 500,
                        PublishDate = new System.DateTime(2006, 7, 10)
                    }
                );


                context.SaveChanges();
            }
        }
    }
}