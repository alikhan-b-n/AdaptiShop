using AdaptiShop.DAL.Entities;
using AdaptiShop.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AdaptiShop.DAL.Providers.EntityProviders;

public class CategoryProvider : BaseProvider<CategoryEntity>, ICategoryProvider
{
    private readonly ApplicationContext _applicationContext;

    public CategoryProvider(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<List<ProductEntity>> GetAllByCategory(CategoryEntity category)
    {
        return await _applicationContext.Products.AsNoTracking().Where(x => x.Category == category).ToListAsync();
    }
}