using AdaptiShop.DAL.Entities;

namespace AdaptiShop.DAL.Providers.Abstract;

public interface ICategoryProvider : ICrudProvider<CategoryEntity>
{
    public Task<List<ProductEntity>> GetAllByCategory(CategoryEntity category);
}