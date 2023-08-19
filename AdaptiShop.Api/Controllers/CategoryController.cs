using AdaptiShop.ViewModels.Params;
using AdaptiShop.ViewModels.Response;
using AdatiShop.BLL.DTOs;
using AdatiShop.BLL.Services;
using AdatiShop.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AdaptiShop.Controllers;

[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IAuthService _authService;

    public CategoryController(ICategoryService categoryService, IAuthService authService)
    {
        _categoryService = categoryService;
        _authService = authService;
    }

    [HttpGet("api/categories")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var success = await _categoryService.GetAll();
            var response = success
                .Select(
                    x =>
                        new CategoryViewModel
                        (
                            x.FatherCategoryId,
                            x.Name,
                            x.Id
                        )
                );

            return Ok(response);
        }
        catch (ArgumentException e)
        {
            return NotFound($"Categories are not found");
        }
    }

    [HttpGet("api/categories/{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            var success = await _categoryService.Get(id);
            return Ok(new CategoryDto(
                success.FatherCategoryId,
                success.Name,
                success.Id));
        }
        catch (ArgumentException e)
        {
            return NotFound($"Category {id} is not found");
        }
    }

    [HttpPost("api/categories")]
    public async Task<IActionResult> Create([FromBody] CategoryParamViewModel paramViewModel)
    {
        Guid id = Guid.Empty;
        if (paramViewModel.Id == Guid.Empty)
        {
            id = await _categoryService.Create
            (new CategoryDto
                (
                    Guid.Empty,
                    paramViewModel.Name,
                    paramViewModel.Id
                )
            );
        }
        else
        {
            id = await _categoryService.Create
            (new CategoryDto
                (
                    paramViewModel.FatherCategoryId,
                    paramViewModel.Name,
                    paramViewModel.Id
                )
            );
        }

        return Ok(id);
    }

    [HttpDelete("api/categories/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            await _categoryService.Delete(id);
            return Ok("Success");
        }
        catch (ArgumentException e)
        {
            return NotFound($"Category {id} is not found");
        }
    }

    [HttpPut("api/categories/{id:guid}")]
    public async Task<IActionResult> Update(CategoryParamViewModel paramViewModel, [FromRoute] Guid id)
    {
        try
        {
            await _categoryService
                .Update(
                    new CategoryDto
                    (
                        paramViewModel.FatherCategoryId,
                        paramViewModel.Name,
                        paramViewModel.Id
                    )
                );

            return Ok("Success");
        }
        catch (ArgumentException e)
        {
            return NotFound($"Category {id} is not found");
        }
    }
}