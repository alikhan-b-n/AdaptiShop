using AdaptiShop.DAL.Entities;

namespace AdaptiShop.DAL.Providers.Abstract;

public interface ICrudProvider<TEntity> where TEntity : BaseEntity
{

    Task Create(TEntity entity);

    Task<TEntity> GetById(Guid? id);

    Task<List<TEntity>> GetAll();

    Task Update(TEntity entity);

    Task Delete(Guid id);
    
}