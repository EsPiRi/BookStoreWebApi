using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Silinmek istenen kitap türü mevcut değil!");
            }
            var book = _context.Books.Include(x => x.Genre).FirstOrDefault(x => x.GenreId == GenreId);
            if (book is not null)
            {
                throw new InvalidOperationException("Silinmek istenen kitap türüne ait en az bir kitap bulunmaktadır. Tür silinemez!");
            }
            var author = _context.Authors.FirstOrDefault(x => x.GenreId == GenreId);
            if (author is not null)
            {
                throw new InvalidOperationException("Silinmek istenen kitap türüne ait en az bir yazar bulunmaktadır. Tür silinemez!");
            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}