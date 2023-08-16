using AdatiShop.BLL.DTOs;

namespace AdatiShop.BLL.Services.Abstract;

public interface IProductService
{
    public Task<Guid> Create(ProductDto productDto);
    public Task<bool> Delete(Guid id);
    public Task<ProductDto> Get(Guid id);
    public Task<List<ProductDto>> GetAll();
    public Task<bool> Update(ProductDto productDto);
}