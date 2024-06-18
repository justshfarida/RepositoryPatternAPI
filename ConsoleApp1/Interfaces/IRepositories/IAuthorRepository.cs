using RepositoryPatternAPI.Data.Entities;
using RepositoryPatternAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApi.Application.Interfaces.IRepositories
{
    public interface IAuthorRepository:IGenericRepository<Author>
    {
    }
}
