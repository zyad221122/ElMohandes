using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using The_Engneering.Contracts.Product;
using The_Engneering.Services;

namespace The_Engneering.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ProductsController(IProductService _productService) : ControllerBase
{
    private readonly IProductService productService = _productService;

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken)
    {
        return Ok(await productService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id, CancellationToken cancellationToken)
    {
        var product = await productService.GetAsync(id, cancellationToken);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
    [HttpPost("")]
    public async Task<IActionResult> AddProduct([FromForm] ProductRequest productDto, CancellationToken cancellationToken)
    {
        
        var productName = await productService.UploadImageAsync(productDto.Cover, cancellationToken);
        productDto.ImageUrl = productName;
        var pro = await productService.AddAsync(productDto.Adapt<Product>());
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id,
                                            [FromBody] Product product,
                                            CancellationToken cancellationToken)
    {
        var isUpdated = await productService.UpdateAsync(id, product, cancellationToken);

        if (!isUpdated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var isDeleted = await productService.DeleteAsync(id, cancellationToken);

        if (!isDeleted)
            return NotFound();
        return NoContent();
    }
}
