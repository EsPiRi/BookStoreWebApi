using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Users
    {
        public static void AddUsers(this BookStoreDbContext context)
        {
            context.Users.AddRange(
                new User
                {
                    Name = "TestName1",
                    Surname = "TestSurname1",
                    Email = "testuser@test.com",
                    Password = "test12345"
                },
                new User
                {
                    Name = "TestName2",
                    Surname = "TestSurname2",
                    Email = "testuser2@test.com",
                    Password = "test12345"
                },
                new User
                {
                    Name = "TestName3",
                    Surname = "TestSurname3",
                    Email = "testuser3@test.com",
                    Password = "test12345"
                },
                new User
                {
                    Name = "TestName4",
                    Surname = "TestSurname4",
                    Email = "testuser4@test.com",
                    Password = "test12345"
                }
            );
        }
    }
}