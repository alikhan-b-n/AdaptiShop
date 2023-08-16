using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdaptiShop.DAL.Entities;
using AdaptiShop.DAL.Providers.Abstract;
using AdatiShop.BLL.DTOs;
using AdatiShop.BLL.Services.Abstract;
using Contracts.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AdatiShop.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserProvider _userProvider;
    private readonly SecretOptions _options;
    
    public AuthService(IUserProvider userProvider, IOptions<SecretOptions> options)
    {
        _userProvider = userProvider;
        _options = options.Value;
    }

    public async Task<string> SignIn(UserSignInDto userSignInDto)
    {
        var userEntity = await _userProvider.GetByEmail(userSignInDto.Email);
        if (userEntity == null) throw new ArgumentException("User not found");
        if (BCrypt.Net.BCrypt.Verify(userSignInDto.Password, userEntity?.PasswordHash))
        {
            return GenerateToken(userSignInDto.Email, userEntity.Username);
        }

        throw new ArgumentException("error, passport is not correct");
    }

    public async Task<string> SignUp(UserSignUpDto userSignUpDto)
    {
        try
        {
            if (await _userProvider.GetByEmail(userSignUpDto.Email) != null)
                throw new ArgumentException("Error, email is already been used");

            UserEntity userEntity = new UserEntity
            {
                Username = userSignUpDto.Username,
                Email = userSignUpDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userSignUpDto.Password)
            };

            await _userProvider.Create(userEntity);
            return GenerateToken(userSignUpDto.Email, userSignUpDto.Username);
        }
        catch (ArgumentException e)
        {
            throw new AggregateException(e.Message);
        }
    }

    public async Task<UserDto> GetUserByHeader(string[] headers)
    {
        var token = headers[0].Replace("Bearer ", "");
        var email = DecryptToken(token).Email;
        var userEntity = await _userProvider.GetByEmail(email);
        if (userEntity == null) throw new ArgumentException("is not authorised");
        return new UserDto(userEntity.Username, userEntity.Email, userEntity.Id);
    }

    private string GenerateToken(string email, string username)
    {
        var key = Encoding.ASCII.GetBytes(_options.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, username)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256)
        };
    
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
    
    private (string Email, string Username) DecryptToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        if (tokenS?.Claims is List<Claim> claims)
        {
            return new ValueTuple<string, string>(claims[0].Value, claims[1].Value);
        }

        throw new ArgumentException();
    }
}