using AdaptiShop.DAL.Entities;

namespace AdatiShop.BLL.DTOs;

public record ProductDto(Guid CategoryId, string Title, Guid Id, decimal Price, string Description);