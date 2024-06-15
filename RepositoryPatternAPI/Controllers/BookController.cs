using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternApi.Application.DTOs;
using RepositoryPatternApi.Application.Interfaces.IServices;
using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPatternAPI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<ResponseModel<IEnumerable<Book>>> GetAllBooks()
        {
            Log.Information("Entering GetAllBooks method.");
            try
            {
                var books = _bookService.GetAllBooks().ToList();

                Log.Information("Exiting GetAllBooks method with success.");
                return new ResponseModel<IEnumerable<Book>>
                {
                    Message = "Books retrieved successfully",
                    Data = books
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in GetAllBooks method.");
                return new ResponseModel<IEnumerable<Book>>
                {
                    Message = "Error retrieving books",
                    Data = null
                };
            }
        }

        [HttpPost]
        public ActionResult<ResponseModel<BookDTO>> CreateBook(BookDTO bookDTO)
        {
            Log.Information("Entering CreateBook method with parameters: {BookDTO}", bookDTO);
            try
            {
                var book = _mapper.Map<Book>(bookDTO);
                _bookService.AddBook(book);

                var createdBookDTO = _mapper.Map<BookDTO>(book);
                Log.Information("Exiting CreateBook method with success.");
                return new ResponseModel<BookDTO>
                {
                    Message = "Book created successfully",
                    Data = createdBookDTO
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in CreateBook method.");
                return new ResponseModel<BookDTO>
                {
                    Message = "Book not created successfully",
                    Data = null
                };
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Book>> GetBook(int id)
        {
            Log.Information("Entering GetBook method with parameter: {Id}", id);
            try
            {
                var book = _bookService.GetBookById(id);
                if (book == null)
                {
                    Log.Warning("Book with id {Id} not found.", id);
                    return NotFound(new ResponseModel<Book>
                    {
                        Message = "Book not found",
                        Data = null
                    });
                }

                Log.Information("Exiting GetBook method with success.");
                return new ResponseModel<Book>
                {
                    Message = "Book retrieved successfully",
                    Data = book
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in GetBook method.");
                return new ResponseModel<Book>
                {
                    Message = "Error retrieving book",
                    Data = null
                };
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<BookDTO>> DeleteBook(int id)
        {
            Log.Information("Entering DeleteBook method with parameter: {Id}", id);
            try
            {
                var book = _bookService.GetBookById(id);
                if (book == null)
                {
                    Log.Warning("Book with id {Id} not found.", id);
                    return NotFound(new ResponseModel<BookDTO>
                    {
                        Message = "Book not found",
                        Data = null
                    });
                }

                _bookService.DeleteBook(id);
                Log.Information("Exiting DeleteBook method with success.");
                return new ResponseModel<BookDTO>
                {
                    Message = "Book deleted successfully",
                    Data = _mapper.Map<BookDTO>(book)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in DeleteBook method.");
                return new ResponseModel<BookDTO>
                {
                    Message = "Error deleting book",
                    Data = null
                };
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<BookDTO>> UpdateBook(BookDTO bookDTO, int id)
        {
            Log.Information("Entering UpdateBook method with parameters: {BookDTO}, {Id}", bookDTO, id);
            try
            {
                var existingBook = _bookService.GetBookById(id);
                if (existingBook == null)
                {
                    Log.Warning("Book with id {Id} not found.", id);
                    return NotFound(new ResponseModel<BookDTO>
                    {
                        Message = "Book not found",
                        Data = null
                    });
                }

                var updatedBook = _mapper.Map(bookDTO, existingBook);
                _bookService.UpdateBook(updatedBook);

                Log.Information("Exiting UpdateBook method with success.");
                return new ResponseModel<BookDTO>
                {
                    Message = "Book updated successfully",
                    Data = _mapper.Map<BookDTO>(updatedBook)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in UpdateBook method.");
                return new ResponseModel<BookDTO>
                {
                    Message = "Error updating book",
                    Data = null
                };
            }
        }
    }
}
