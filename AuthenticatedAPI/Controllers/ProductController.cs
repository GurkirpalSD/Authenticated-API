using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDataContext _info;

    public ProductController(AppDataContext information)
    {
        _info = information;
    }

    [HttpGet]
    public async Task <ActionResult<IEnumerable<Product>> Get()
    {
        return Ok(_info.Products.ToList());
    }

    [HttpGet("{categoryId}")]
    public async Task ActionResult<IEnumerable<Product>> GetProductsByCategory(int categoryId)
    {
        var category = await _info.Products.Where(category => category.Products).FirstOrDefault(category => category.Id == categoryId);
        if (category == null)
        {
            return NotFound("Category not found.");
        }

        return category.Products;
    }

    [HttpPost]
    public async ActionResult AddProduct(Product product)
    {
        _info.Products.Add(product);
        _info.SaveChanges();

        return Product added successfully;
    }
}