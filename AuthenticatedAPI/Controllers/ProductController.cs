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
    private readonly ApplicationDbContext _info;

    public ProductController(ApplicationDbContext information)
    {
        _info = information;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        return Ok(_info.Products.ToList());
    }

    [HttpGet("{categoryId}")]
    public ActionResult<IEnumerable<Product>> GetProductsByCategory(int categoryId)
    {
        var category = _info.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == categoryId);
        if (category == null)
        {
            return NotFound("Category not found.");
        }

        return category.Products;
    }

    [HttpPost]
    public ActionResult AddProduct(Product product)
    {
        _info.Products.Add(product);
        _info.SaveChanges();

        return Product added successfully;
    }
}