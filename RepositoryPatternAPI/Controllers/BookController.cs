using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternApi.Application.DTOs;
using RepositoryPatternApi.Application.Interfaces.IServices;
using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternAPI.Controllers;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.DTOs;
using RepositoryPatternAPI.Services.Abstractions;

namespace RepositoryPatternAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController:ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService BookService, ILogger<AuthorController> logger, IMapper mapper)
        {
            _bookService = BookService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ResponseModel<IEnumerable<Book>>> GetAllBooks()
        {
            var books = _bookService.GetAllBooks().ToList();

            return new ResponseModel<IEnumerable<Book>>
            {
                Message = "Authors retrieved successfully",
                Data = books
            };
        }

        [HttpPost]
        public ActionResult<ResponseModel<BookDTO>> CreateBook(BookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            _bookService.AddBook(book);

            return new ResponseModel<BookDTO>
            {
                Message = "Book created successfully",
                Data = _mapper.Map<BookDTO>(book)
            };
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Book>> GetBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(new ResponseModel<Book>
                {
                    Message = "Book not found",
                    Data = null
                });
            }

            return new ResponseModel<Book>
            {
                Message = "Book retrieved successfully",
                Data = book
            };
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<BookDTO>> DeleteBook(int id)
        {
            var author = _bookService.GetBookById(id);
            if (author == null)
            {
                return NotFound(new ResponseModel<BookDTO>
                {
                    Message = "Author not found",
                    Data = null
                });
            }

            _bookService.DeleteBook(id);

            return new ResponseModel<BookDTO>
            {
                Message = "Book deleted successfully",
                Data = _mapper.Map<BookDTO>(author)
            };
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<BookDTO>> UpdateBook(BookDTO bookDTO, int id)
        {
            var existingBook = _bookService.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound(new ResponseModel<BookDTO>
                {
                    Message = "Book not found",
                    Data = null
                });
            }

            var updatedAuthor = _mapper.Map(bookDTO, existingBook);
            _bookService.UpdateBook(updatedAuthor);

            return new ResponseModel<BookDTO>
            {
                Message = "Author updated successfully",
                Data = _mapper.Map<BookDTO>(updatedAuthor)
            };
        }
    }
}
