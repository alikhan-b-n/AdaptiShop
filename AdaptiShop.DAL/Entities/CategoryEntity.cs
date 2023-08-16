namespace AdaptiShop.DAL.Entities;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }
    public CategoryEntity? FatherCategory { get; set; }
    public Guid? FatherCategoryId { get; set; }
}