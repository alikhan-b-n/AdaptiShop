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
            ProductEntity productEntity;
            if (productDto.Id == Guid.Empty)
            {
                productEntity = new ProductEntity
                {
                    Title = productDto.Title,
                    Description = productDto.Description,
                    Price = productDto.Price
                };
            }
            var categoryEntity = await _categoryProvider.GetById(productDto.CategoryId);
            if (categoryEntity.Id != productDto.CategoryId)
            {
                throw new ArgumentException("CategoryId is not referencing any existing category");
            }
            productEntity = new ProductEntity
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
            throw new Exception(e.Message);
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
            return productEntity.Id == id
                ? new ProductDto
                (
                    productEntity.CategoryId,
                    productEntity.Title,
                    productEntity.Price,
                    productEntity.Description
                )
                : throw new ArgumentException("Id is not found");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<List<ProductDto>> GetAll()
    {
        try
        {
            var productEntities = await _productProvider.GetAll();
            return productEntities.Select(x => new ProductDto(x.CategoryId, x.Title, x.Price, x.Description)).ToList();
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
            var productEntity = await _productProvider.GetById(productDto.Id);

            productEntity.Price = productDto.Price;
            productEntity.Title = productDto.Title;
            productEntity.CategoryId = productDto.CategoryId;
            await _productProvider.Update(productEntity);

            return true;
        }
        catch (ArgumentException e)
        {
            throw new ArgumentException(e.Message);
        }
    }
}