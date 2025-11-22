using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Models;
using Product.Api.Services.CatalogProducts;

namespace Product.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public CatalogProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var items = await _productService.GetAllAsync(cancellationToken);
        var response = new ApiResponse<IEnumerable<CatalogProductDto>>
        {
            Data = items,
            Meta = new Dictionary<string, object?> { { "total", items.Count() } }
        };
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var item = await _productService.GetByIdAsync(id, cancellationToken);
        if (item is null) return NotFound(new ApiResponse<object> { Success = false, Message = "Not found" });
        return Ok(new ApiResponse<CatalogProductDto> { Data = item });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCatalogProductDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        var created = await _productService.CreateAsync(dto, cancellationToken);
        var response = new ApiResponse<CatalogProductDto> { Data = created };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCatalogProductDto dto,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        if (id != dto.Id) return BadRequest("Id route does not match body id");
        try
        {
            await _productService.UpdateAsync(dto, cancellationToken);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponse<object> { Success = false, Message = "Not found" });
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}