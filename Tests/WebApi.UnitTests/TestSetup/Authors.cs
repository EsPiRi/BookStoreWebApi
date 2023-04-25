using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
                                    DateOfBirth = new DateTime(2000, 01, 01),
                                    GenreId = 3
                                }
                            );

        }
    }
}
