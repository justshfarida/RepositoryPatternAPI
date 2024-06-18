namespace RepositoryPatternAPI.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        
            public TEntity GetById(int id);
            public IEnumerable<TEntity> GetAll();
            public void Add(TEntity entity);
            public void Delete(int id);
            public void Update(TEntity entity);
        
    }
}
