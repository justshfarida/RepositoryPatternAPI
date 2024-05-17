using RepositoryPatternApi.Application.Interfaces.IRepositories;
using RepositoryPatternApi.Persistence.Implementations.Repositories;
using RepositoryPatternAPI.Data;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.Repositories;
using RepositoryPatternAPI.UnitofWork;

namespace RepositoryPatternAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDBContext _dbContext;

        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAuthorRepository authorRepository => new AuthorRepository(_dbContext);

        public IBookRepository bookRepository => new BookRepository(_dbContext);

        // IGenericRepository<Author> IUnitOfWork.authorRepository => new AuthorRepository(_dbContext, this);

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
