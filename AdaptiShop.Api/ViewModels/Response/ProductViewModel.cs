namespace AdaptiShop.ViewModels.Response;

public record ProductViewModel(Guid CategoryId, string Title,
    decimal Price, string Description);