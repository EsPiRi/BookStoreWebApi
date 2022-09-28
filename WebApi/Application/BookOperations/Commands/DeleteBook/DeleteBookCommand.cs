using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public readonly IBookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public DeleteBookCommand(IBookStoreDbContext dbContext)
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