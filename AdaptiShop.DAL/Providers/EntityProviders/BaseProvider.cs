using AdaptiShop.DAL.Entities;
using AdaptiShop.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AdaptiShop.DAL.Providers.EntityProviders;

public abstract class BaseProvider<TEntity> : ICrudProvider<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseProvider(ApplicationContext applicationContext)
    {
        _context = applicationContext;
        _dbSet = applicationContext.Set<TEntity>();
    }


    public async Task Create(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> GetById(Guid? id)
    {
        return await _dbSet.FirstAsync(x => x.Id == id);
        
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Update(TEntity entity)
    {
        // Это если мы используем AsNoTracking для оптимизации
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _dbSet.Remove(_dbSet.First(x => x.Id == id));
        await _context.SaveChangesAsync();
    }
}