using AdaptiShop.ViewModels.Params;
using AdaptiShop.ViewModels.Response;
using AdatiShop.BLL.DTOs;
using AdatiShop.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaptiShop.Controllers;

[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("api/products")]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAll();

        return Ok(products.Select(x => new ProductViewModel(x.CategoryId, x.Title, x.Price, x.Description)));
    }

    [HttpGet("api/product")]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        var product = await _productService.Get(id);
        return Ok
        (
            new ProductViewModel
            (
                product.CategoryId,
                product.Title,
                product.Price,
                product.Description
            )
        );
    }

    [HttpPost("api/products")]
    public async Task<IActionResult> Create([FromBody] ProductParamViewModel paramViewModel)
    {
        var id = await _productService.Create(
                new ProductDto
                (
                    paramViewModel.CategoryId,
                    paramViewModel.Title,
                    paramViewModel.Price,
                    paramViewModel.Desription
                ));
            return Ok(id);
    }

    [HttpPut("api/products/{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductParamViewModel paramViewModel)
    {
        try
        {
            var product = await _productService.Get(id);
            var updatedProductDto = 
                new ProductDto(
                    paramViewModel.CategoryId,
                    paramViewModel.Title,
                    paramViewModel.Price,
                    paramViewModel.Desription,
                    id
                    );
            product = updatedProductDto;
            return Ok(await _productService.Update(product));
        }
        catch (Exception e)
        {
            return BadRequest("Unable to update");
        }
    }

    [HttpDelete("api/products/{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        try
        {
            return Ok(await _productService.Delete(id));
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}