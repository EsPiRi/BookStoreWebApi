using WebApi.DBOperations;
using System;
using System.Linq;
using WebApi.BookOperations.GetBooks;

namespace WebApi.BookOperations.UpdateBook
{

    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UpdateBookViewModel Handle()
        {
            var _book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            UpdateBookViewModel vm = new UpdateBookViewModel();
            if (_book is null)
            {
                throw new InvalidOperationException("Güncellemek istediğiniz kitap mevcut değil");
            }

            _book.GenreId = Model.GenreId != default ? Model.GenreId : _book.GenreId;
            _book.Title = Model.Title != default ? Model.Title : _book.Title;
            _book.PageCount = Model.PageCount != default ? Model.PageCount : _book.PageCount;
            //_book.PublishDate = Model.PublishDate != default ? Model.PublishDate : _book.PublishDate;
            _dbContext.SaveChanges();
            return Model;
        }

    }
    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        //public DateTime PublishDate { get; set; }
    }
}