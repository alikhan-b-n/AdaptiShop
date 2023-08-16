using AdatiShop.BLL.DTOs;

namespace AdatiShop.BLL.Services.Abstract;

public interface ICategoryService
{
    public Task<Guid> Create(CategoryDto categoryDto);
    public Task<bool> Delete(Guid id);
    public Task<CategoryDto> Get(Guid id);
    public Task<List<CategoryDto>> GetAll();
    public Task<bool> Update(CategoryDto categoryDto);
}