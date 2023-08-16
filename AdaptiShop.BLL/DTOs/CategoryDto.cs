using AdaptiShop.DAL.Entities;

namespace AdatiShop.BLL.DTOs;

public record CategoryDto(Guid? FatherCategoryId, string Name, Guid Id);