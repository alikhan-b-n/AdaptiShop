namespace AdaptiShop.ViewModels.Response;

public record CategoryViewModel(Guid? FatherCategoryId, string Name, Guid Id);