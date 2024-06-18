using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.Repositories;
using RepositoryPatternAPI.Services.Abstractions;
using RepositoryPatternAPI.UnitofWork;

namespace RepositoryPatternAPI.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddAuthor(Author author)
        {
            _unitOfWork.authorRepository.Add(author);
            _unitOfWork.Commit();
        }

        public void UpdateAuthor(Author author)
        {
            _unitOfWork.authorRepository.Update(author);
            _unitOfWork.Commit();
        }

        public void DeleteAuthor(int id)
        {
            _unitOfWork.authorRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public Author GetAuthorById(int id)

        {
            Author author = _unitOfWork.authorRepository.GetById(id);
            foreach (var authorBook in author.AuthorBooks)
            {
                Console.WriteLine($"Book ISBN: {authorBook.book.ISBN}, Book Title: {authorBook.book.Title}");
            }
            return author ;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            var authors = _unitOfWork.authorRepository
                .GetAll().ToList();
                //.Include(a => a.AuthorBooks)
                //    .ThenInclude(ab => ab.Book)
                //.ToList();

            return authors;
        }
}
}
