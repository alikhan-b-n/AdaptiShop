namespace AdaptiShop.ViewModels.Response;

public record ProductViewModel(Guid CategoryId, string Title, Guid Id,
    decimal Price, string Description);