using WebApi.DBOperations;
using System;
using System.Linq;
using WebApi.BookOperations.GetBooks;
using AutoMapper;
using System.Threading.Tasks;

namespace WebApi.BookOperations.UpdateBook
{

    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public UpdateBookViewModel Handle()
        {
            var _book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            //UpdateBookViewModel vm = new UpdateBookViewModel();
            if (_book is null)
            {
                throw new InvalidOperationException("Güncellemek istediğiniz kitap mevcut değil");
            } 
            _mapper.Map<UpdateBookViewModel,Book>(Model,_book);     
            // _book.GenreId = Model.GenreId != default ? Model.GenreId : _book.GenreId;
            // _book.Title = Model.Title != default ? Model.Title : _book.Title;
            // _book.PageCount = Model.PageCount != default ? Model.PageCount : _book.PageCount;
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