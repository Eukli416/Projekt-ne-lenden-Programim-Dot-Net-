using DotNetCrudProject.Models;
using DotNetCrudProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCrudProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public ActionResult<List<Product>> GetAll()
    {
        return Ok(_productService.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _productService.GetById(id);

        if (product is null)
        {
            return NotFound("Produkti nuk u gjet.");
        }

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return BadRequest("Emri i produktit eshte i detyrueshem.");
        }

        if (product.Price < 0)
        {
            return BadRequest("Cmimi nuk mund te jete negativ.");
        }

        var createdProduct = _productService.Create(product);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return BadRequest("Emri i produktit eshte i detyrueshem.");
        }

        if (product.Price < 0)
        {
            return BadRequest("Cmimi nuk mund te jete negativ.");
        }

        var updated = _productService.Update(id, product);

        if (!updated)
        {
            return NotFound("Produkti nuk u gjet.");
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _productService.Delete(id);

        if (!deleted)
        {
            return NotFound("Produkti nuk u gjet.");
        }

        return NoContent();
    }
}
