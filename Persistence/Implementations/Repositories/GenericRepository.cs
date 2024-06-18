
using Microsoft.EntityFrameworkCore;
using RepositoryPatternAPI.Data;
using RepositoryPatternAPI.UnitofWork;

namespace RepositoryPatternAPI.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class

    {
        protected ApplicationDBContext dBContext;

        public GenericRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
    
        }
         public void Add(TEntity entity)
        {
            dBContext.Set<TEntity>().Add(entity);
        }

        public void Delete(int id)
        {
            TEntity entityToDelete = GetById(id);
            if (entityToDelete != null)
            {
                dBContext.Set<TEntity>().Remove(entityToDelete);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dBContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return dBContext.Set<TEntity>().Find(id);
        }

        public void Update(TEntity entity)
        {
            dBContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
