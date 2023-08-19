using AdaptiShop.DAL.Entities;

namespace AdatiShop.BLL.DTOs;

public record ProductDto(Guid CategoryId, string Title, decimal Price, string Description, Guid Id)
{
    public ProductDto
        (Guid CategoryId, string Title, decimal Price, string Description)
        : 
        this(CategoryId, Title, Price, Description, Guid.Empty)
    {
        
    }
}