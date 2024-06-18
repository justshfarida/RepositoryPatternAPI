using RepositoryPatternApi.Application.Interfaces.IRepositories;
using RepositoryPatternAPI.Data;
using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.UnitofWork;

namespace RepositoryPatternAPI.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
        }
    }
}
