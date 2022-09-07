using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using WebApi.DBOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context);
            BookViewModel Model = new BookViewModel();
            try
            {
                Model.Id = id;
                query.Model = Model;
                var result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(query.Model);

            // var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            // return book;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel bookModel)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = bookModel;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel bookModel)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookViewModel model;
            try
            {
                command.ModelId = id;
                command.Model = bookModel;
                model = command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(model);
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
    }
}