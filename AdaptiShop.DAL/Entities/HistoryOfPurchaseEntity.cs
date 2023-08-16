namespace AdaptiShop.DAL.Entities;

public class HistoryOfPurchaseEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public UserEntity UserEntity { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity ProductEntity { get; set; }
    
    //date already is in base entity 
}