using AdaptiShop.DAL.Entities;
using AdaptiShop.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AdaptiShop.DAL.Providers.EntityProviders;

public class ProductProvider : BaseProvider<ProductEntity>, IProductProvider
{
    private readonly ApplicationContext _applicationContext;

    public ProductProvider(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }

    
}