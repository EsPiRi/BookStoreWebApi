using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using WebApi.DBOperations;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.Application.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            BookViewModel result;
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();

            query.BookId = id;
            validator.ValidateAndThrow(query);
            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel bookModel)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            command.Model = bookModel;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel bookModel)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            UpdateBookViewModel model;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();

            command.BookId = id;
            command.Model = bookModel;
            validator.ValidateAndThrow(command);
            model = command.Handle();

            return Ok(model);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator(_context);
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}