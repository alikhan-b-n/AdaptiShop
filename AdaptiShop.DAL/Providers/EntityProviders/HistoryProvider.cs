using AdaptiShop.DAL.Entities;
using AdaptiShop.DAL.Providers.Abstract;

namespace AdaptiShop.DAL.Providers.EntityProviders;

public class HistoryProvider : BaseProvider<HistoryOfPurchaseEntity>, IHistoryProvider
{
    private readonly ApplicationContext _applicationContext;

    public HistoryProvider(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }
}