using AdaptiShop.ViewModels.Response;
using AdatiShop.BLL.DTOs;
using AdatiShop.BLL.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AdaptiShop.Controllers;

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
        List<ProductDto> products;
        try
        {
            products = await _productService.GetAll();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

        return Ok(products.Select(x=>new ProductViewModel(x.CategoryId, x.Title, x.Id, x.Price, x.Description)));
    }
}