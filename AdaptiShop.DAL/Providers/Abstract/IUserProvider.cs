using AdaptiShop.DAL.Entities;

namespace AdaptiShop.DAL.Providers.Abstract;

public interface IUserProvider : ICrudProvider<UserEntity>
{
    public Task<UserEntity?> GetByEmail(string email);
}