using RepositoryPatternApi.Application.Interfaces.IRepositories;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.Repositories;

namespace RepositoryPatternAPI.UnitofWork

{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        //IGenericRepository<Author> authorRepository { get; }
        IAuthorRepository authorRepository { get; }
        IBookRepository bookRepository { get; }
    }
}
