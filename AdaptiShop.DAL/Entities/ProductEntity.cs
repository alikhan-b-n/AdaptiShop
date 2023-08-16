namespace AdaptiShop.DAL.Entities;

public class ProductEntity : BaseEntity
{
    public string Title { get; set; }
    public CategoryEntity Category { get; set; }

    public Guid CategoryId { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }
}