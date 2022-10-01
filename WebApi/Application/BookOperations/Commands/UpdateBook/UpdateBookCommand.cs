using WebApi.DBOperations;
using System;
using System.Linq;
using AutoMapper;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{

    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public UpdateBookViewModel Handle()
        {
            var _book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);

            if (_book is null)
            {
                throw new InvalidOperationException("Güncellemek istediğiniz kitap mevcut değil");
            }

            var genre = _dbContext.Books.Include(x => x.Genre).FirstOrDefault(x => x.GenreId == Model.GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap türü bulunamadığı için kitap eklenemedi");
            }
            var author=_dbContext.Books.Include(x => x.Author).FirstOrDefault(x => x.AuthorId == Model.AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar bulunamadığı için kitap eklenemedi");
            }
            _mapper.Map<UpdateBookViewModel, Book>(Model, _book);

            _dbContext.SaveChanges();
            return Model;
        }

    }
    public class UpdateBookViewModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public int AuthorId { get; set; }
        
    }
}