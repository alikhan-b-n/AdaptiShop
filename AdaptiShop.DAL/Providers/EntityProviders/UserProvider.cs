using AdaptiShop.DAL.Entities;
using AdaptiShop.DAL.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AdaptiShop.DAL.Providers.EntityProviders;

public class UserProvider : BaseProvider<UserEntity>, IUserProvider
{
    private readonly ApplicationContext _applicationContext;

    public UserProvider(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<UserEntity?> GetByEmail(string email)
    {
        return await _applicationContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}