using AdatiShop.BLL.DTOs;

namespace AdatiShop.BLL.Services.Abstract;

public interface IAuthService
{
    public Task<string> SignIn(UserSignInDto userSignInDto);
    public Task<string> SignUp(UserSignUpDto userSignUpDto);
    Task<UserDto> GetUserByHeader(string[] headers);

}