using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.DTOs;
using RepositoryPatternAPI.Services.Abstractions;

namespace RepositoryPatternAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger, IMapper mapper)
        {
            _authorService = authorService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ResponseModel<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            var authors = _authorService.GetAllAuthors();
            var authorDTOs = authors.Select(x=>_mapper.Map<AuthorDTO>(x)).ToList();
                //_mapper.Map<IEnumerable<AuthorDTO>>(authors);

            return new ResponseModel<IEnumerable<AuthorDTO>>
            {
                Message = "Authors retrieved successfully",
                Data = authorDTOs
            };
        }

        [HttpPost]
        public ActionResult<ResponseModel<AuthorDTO>> CreateAuthor(AuthorDTO authorDTO)
        {
            var author = _mapper.Map<Author>(authorDTO);
            _authorService.AddAuthor(author);

            return new ResponseModel<AuthorDTO>
            {
                Message = "Author created successfully",
                Data = _mapper.Map<AuthorDTO>(author)
            };
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<AuthorDTO>> GetAuthor(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(new ResponseModel<AuthorDTO>
                {
                    Message = "Author not found",
                    Data = null
                });
            }

            return new ResponseModel<AuthorDTO>
            {
                Message = "Author retrieved successfully",
                Data = _mapper.Map<AuthorDTO>(author)
            };
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseModel<AuthorDTO>> DeleteAuthor(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound(new ResponseModel<AuthorDTO>
                {
                    Message = "Author not found",
                    Data = null
                });
            }

            _authorService.DeleteAuthor(id);

            return new ResponseModel<AuthorDTO>
            {
                Message = "Author deleted successfully",
                Data = _mapper.Map<AuthorDTO>(author)
            };
        }

        [HttpPut("{id}")]
        public ActionResult<ResponseModel<AuthorDTO>> UpdateAuthor(AuthorDTO authorDTO, int id)
        {
            var existingAuthor = _authorService.GetAuthorById(id);
            if (existingAuthor == null)
            {
                return NotFound(new ResponseModel<AuthorDTO>
                {
                    Message = "Author not found",
                    Data = null
                });
            }

            var updatedAuthor = _mapper.Map(authorDTO, existingAuthor);
            _authorService.UpdateAuthor(updatedAuthor);

            return new ResponseModel<AuthorDTO>
            {
                Message = "Author updated successfully",
                Data = _mapper.Map<AuthorDTO>(updatedAuthor)
            };
        }
    }
}
