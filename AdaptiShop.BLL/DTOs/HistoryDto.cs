namespace AdatiShop.BLL.DTOs;

public record HistoryDto(Guid UserId, UserDto UserDto, Guid ProductId, ProductDto ProductDto, Guid Id);