using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBookByIdQuery
    {
        public BookViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        
        
        public GetBookByIdQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public BookViewModel Handle()
        {
            var book = _dbContext.Books.Where(book => book.Id == Model.Id).SingleOrDefault();            
            if(book is null)
            {
                throw new InvalidOperationException("Kitap mevcut değil");
            }            
            Model.Id=book.Id;
            Model.Genre=((GenreEnum)book.GenreId).ToString();
            Model.PageCount=book.PageCount;
            Model.PublishDate=book.PublishDate.Date.ToString("dd/MM/yyyy");
            Model.Title=book.Title;
            return Model;
        }
    }
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}