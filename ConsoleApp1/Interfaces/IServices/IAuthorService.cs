using RepositoryPatternAPI.Data.Entities;

namespace RepositoryPatternAPI.Services.Abstractions
{
    public interface IAuthorService
    {
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int id);
        Author GetAuthorById(int id);
        IEnumerable<Author> GetAllAuthors();
    }
}
