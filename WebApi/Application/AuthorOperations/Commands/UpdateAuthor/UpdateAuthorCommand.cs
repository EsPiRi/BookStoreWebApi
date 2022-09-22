using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorModel Model;
        private readonly IMapper _mapper;
        public int AuthorId { get; set; }

        public UpdateAuthorCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Güncellemek istenilen yazar bulunmamaktadır!");
            }
            var genre = _context.Authors.Include(x => x.Genre).SingleOrDefault(x => x.Genre.Id == Model.GenreId);
            if(genre is null)
            {
                throw new InvalidOperationException("Güncellemek istenilen kitap türü mevcut değildir!");
            }

            _mapper.Map<UpdateAuthorModel, Author>(Model, author);
            _context.SaveChanges();
        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenreId { get; set; }        
    }
}