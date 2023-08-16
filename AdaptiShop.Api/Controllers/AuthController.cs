using AdaptiShop.ViewModels.Params;
using AdatiShop.BLL.DTOs;
using AdatiShop.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AdaptiShop.Controllers;

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("api/user/signup")]
    public async Task<IActionResult> Register([FromBody] SignUpViewModel viewModel)
    {
        string token;
        try
        {
            token = await _authService.SignUp(
                new UserSignUpDto(viewModel.Email, viewModel.Username, viewModel.Password));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return Ok(token);
    }

    [HttpPost("api/user/signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInViewModel viewModel)
    {
        string token;
        try
        {
            token = await _authService.SignIn(new UserSignInDto(viewModel.Email, viewModel.Password));
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }

        return Ok(token);
    }
    
    [Authorize]
    [HttpGet("api/user/get")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            ///Save for future projects
            return Ok(await _authService.GetUserByHeader(Request.Headers[HeaderNames.Authorization]!));
        }
        catch (ArgumentException e)
        {
            return NotFound("User is not found, wrong token");
        }
    }
}