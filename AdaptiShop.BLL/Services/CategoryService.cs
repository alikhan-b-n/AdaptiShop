using AdaptiShop.DAL.Entities;
using AdaptiShop.DAL.Providers.Abstract;
using AdatiShop.BLL.DTOs;
using AdatiShop.BLL.Services.Abstract;

namespace AdatiShop.BLL.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryProvider _categoryProvider;

    public CategoryService(ICategoryProvider categoryProvider)
    {
        _categoryProvider = categoryProvider;
    }

    public async Task<Guid> Create(CategoryDto categoryDto)
    {
        try
        {
            if (categoryDto.FatherCategoryId == null)
            {
                CategoryEntity categoryEntity = new CategoryEntity
                {
                    Name = categoryDto.Name
                };
                await _categoryProvider.Create(categoryEntity);
                return categoryEntity.Id;            }
            else
            {
                var fatherCatergory = await _categoryProvider.GetById(categoryDto.FatherCategoryId);
                CategoryEntity categoryEntity = new CategoryEntity
                {
                    FatherCategory = fatherCatergory,
                    FatherCategoryId = fatherCatergory.Id,
                    Name = categoryDto.Name
                };
                await _categoryProvider.Create(categoryEntity);
                return categoryEntity.Id;
            }
            
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
            await _categoryProvider.Delete(id);
            return true;
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public async Task<CategoryDto> Get(Guid id)
    {
        try
        {
            var categoryEntity = await _categoryProvider.GetById(id);
            return new CategoryDto(categoryEntity.FatherCategoryId, categoryEntity.Name, categoryEntity.Id);
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        try
        {
            var categoryEntities = await _categoryProvider.GetAll();
            return categoryEntities.Select(x => new CategoryDto(x.FatherCategoryId, x.Name, x.Id)).ToList();
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public async Task<bool> Update(CategoryDto categoryDto)
    {
        try
        {
            await _categoryProvider.Update(new CategoryEntity
            {
                Id = categoryDto.Id,
                FatherCategoryId = categoryDto.FatherCategoryId,
                Name = categoryDto.Name
            });
            return true;
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }
}