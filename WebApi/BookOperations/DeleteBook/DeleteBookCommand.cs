using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var _book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (_book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil!");
            }
            _dbContext.Books.Remove(_book);
            _dbContext.SaveChanges();
        }
    }
}