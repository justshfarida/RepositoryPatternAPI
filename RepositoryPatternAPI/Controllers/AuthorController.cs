using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.DTOs;
using RepositoryPatternAPI.Services.Abstractions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPatternAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ResponseModel<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            Log.Information("Entering GetAllAuthors method.");
            try
            {
                var authors = _authorService.GetAllAuthors();
                var authorDTOs = authors.Select(x => _mapper.Map<AuthorDTO>(x)).ToList();

                Log.Information("Exiting GetAllAuthors method with success.");
                return new ResponseModel<IEnumerable<AuthorDTO>>
                {
                    Message = "Authors retrieved successfully",
                    Data = authorDTOs
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in GetAllAuthors method.");
                return new ResponseModel<IEnumerable<AuthorDTO>>
                {
                    Message = "Error retrieving authors",
                    Data = null
                };
            }
        }

        [HttpPost]
        public ActionResult<ResponseModel<AuthorDTO>> CreateAuthor(AuthorDTO authorDTO)
        {
            Log.Information("Entering CreateAuthor method with parameters: {AuthorDTO}", authorDTO);
            try
            {
                var author = _mapper.Map<Author>(authorDTO);
                _authorService.AddAuthor(author);

                var createdAuthorDTO = _mapper.Map<AuthorDTO>(author);
                Log.Information("Exiting CreateAuthor method with success.");
                return new ResponseModel<AuthorDTO>
                {
                    Message = "Author created successfully",
                    Data = createdAuthorDTO
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in CreateAuthor method.");
                return new ResponseModel<AuthorDTO>
                {
                    Message = "Author not created successfully",
                    Data = null
                };
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<AuthorDTO>> GetAuthor(int id)
        {
            Log.Information("Entering GetAuthor method with parameter: {Id}", id);
            try
            {
                var author = _authorService.GetAuthorById(id);
                if (author == null)
                {
                    Log.Warning("Author with id {Id} not found.", id);
                    return NotFound(new ResponseModel<AuthorDTO>
                    {
                        Message = "Author not found",
                        Data = null
                    });
                }

                var authorDTO = _mapper.Map<AuthorDTO>(author);
                Log.Information("Exiting GetAuthor method with success.");
                return new ResponseModel<AuthorDTO>
                {
                    Message = "Author retrieved successfully",
                    Data = authorDTO
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in GetAuthor method.");
                return new ResponseModel<AuthorDTO>
                {
                    Message = "Error retrieving author",
                    Data = null
                };
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<AuthorDTO>> DeleteAuthor(int id)
        {
            Log.Information("Entering DeleteAuthor method with parameter: {Id}", id);
            try
            {
                var author = _authorService.GetAuthorById(id);
                if (author == null)
                {
                    Log.Warning("Author with id {Id} not found.", id);
                    return NotFound(new ResponseModel<AuthorDTO>
                    {
                        Message = "Author not found",
                        Data = null
                    });
                }

                _authorService.DeleteAuthor(id);
                Log.Information("Exiting DeleteAuthor method with success.");
                return new ResponseModel<AuthorDTO>
                {
                    Message = "Author deleted successfully",
                    Data = _mapper.Map<AuthorDTO>(author)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in DeleteAuthor method.");
                return new ResponseModel<AuthorDTO>
                {
                    Message = "Error deleting author",
                    Data = null
                };
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<AuthorDTO>> UpdateAuthor(AuthorDTO authorDTO, int id)
        {
            Log.Information("Entering UpdateAuthor method with parameters: {AuthorDTO}, {Id}", authorDTO, id);
            try
            {
                var existingAuthor = _authorService.GetAuthorById(id);
                if (existingAuthor == null)
                {
                    Log.Warning("Author with id {Id} not found.", id);
                    return NotFound(new ResponseModel<AuthorDTO>
                    {
                        Message = "Author not found",
                        Data = null
                    });
                }

                var updatedAuthor = _mapper.Map(authorDTO, existingAuthor);
                _authorService.UpdateAuthor(updatedAuthor);
                
                Log.Information("Exiting UpdateAuthor method with success.");
                return new ResponseModel<AuthorDTO>
                {
                    Message = "Author updated successfully",
                    Data = _mapper.Map<AuthorDTO>(updatedAuthor)
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error occurred in UpdateAuthor method.");
                return new ResponseModel<AuthorDTO>
                {
                    Message = "Error updating author",
                    Data = null
                };
            }
        }
    }
}
