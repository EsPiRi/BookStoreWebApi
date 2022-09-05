using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController (BookStoreDbContext context)
        {
            _context=context;
        }        

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            var _book = _context.Books.SingleOrDefault(x => x.Title == book.Title);
            if (_book is not null)
            {
                return BadRequest();
            }
            _context.Books.Add(book);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            var _book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (_book is null)
            {
                return BadRequest();
            }
            _book.GenreId = book.GenreId != default ? book.GenreId : _book.GenreId;
            _book.Title = book.Title != default ? book.Title : _book.Title;
            _book.PageCount = book.PageCount != default ? book.PageCount : _book.PageCount;
            _book.PublishDate = book.PublishDate != default ? book.PublishDate : _book.PublishDate;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var _book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (_book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(_book);
            _context.SaveChanges();
            return Ok();
        }


        /* [HttpGet]
       public Book Get([FromQuery] string id)
       {
           var book = BookList.Where(book => book.Id==Convert.ToInt32(id)).SingleOrDefault();
           return book;
       } */

        /*  [HttpGet("pageCountGreaterThanOrEqualTo/{pageCount}")]
       public List<Book> GetByPageCountGreaterThanOrEqualTo(int pageCount)
       {
           var book = BookList.Where(book=> book.PageCount>=pageCount).ToList();
           return book;
       } */
    }
}