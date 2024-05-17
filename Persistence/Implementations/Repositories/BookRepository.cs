using RepositoryPatternApi.Application.Interfaces.IRepositories;
using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternAPI.Data;
using RepositoryPatternAPI.Repositories;
using RepositoryPatternAPI.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApi.Persistence.Implementations.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDBContext dBContext) : base(dBContext)
        {
        }
    }
}
