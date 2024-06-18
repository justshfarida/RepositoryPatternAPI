using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApi.Application.Interfaces.IRepositories
{
    public interface IBookRepository:IGenericRepository<Book>
    {

    }
}
