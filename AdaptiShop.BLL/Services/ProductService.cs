using AdaptiShop.DAL.Entities;
using AdaptiShop.DAL.Providers.Abstract;
using AdatiShop.BLL.DTOs;
using AdatiShop.BLL.Services.Abstract;

namespace AdatiShop.BLL.Services;

public class ProductService : IProductService
{
    private readonly IProductProvider _productProvider;
    private readonly ICategoryProvider _categoryProvider;

    public ProductService(IProductProvider productProvider, ICategoryProvider categoryProvider)
    {
        _productProvider = productProvider;
        _categoryProvider = categoryProvider;
    }

    public async Task<Guid> Create(ProductDto productDto)
    {
        try
        {
            var categoryEntity = await _categoryProvider.GetById(productDto.CategoryId);
            var productEntity = new ProductEntity
            {
                CategoryId = categoryEntity.Id,
                Category = categoryEntity,
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price
            };
            await _productProvider.Create(productEntity);
            return productEntity.Id;
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            await _productProvider.Delete(id);
            return true;
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public async Task<ProductDto> Get(Guid id)
    {
        try
        {
            var productEntity = await _productProvider.GetById(id);
            ProductDto productDto =
                new ProductDto(productEntity.CategoryId, productEntity.Title, productEntity.Id, productEntity.Price, productEntity.Description);
            return productDto;
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public async Task<List<ProductDto>> GetAll()
    {
        try
        {
            var productEntities = await _productProvider.GetAll();
            return productEntities.Select(x => new ProductDto(x.CategoryId, x.Title, x.Id, x.Price, x.Description)).ToList();
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public async Task<bool> Update(ProductDto productDto)
    {
        try
        {
            await _productProvider.Update(new ProductEntity
            {
                CategoryId = productDto.CategoryId,
                Title = productDto.Title,
                Id = productDto.Id,
                Description = productDto.Description,
                Price = productDto.Price
            });
            
            return true;
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }
}