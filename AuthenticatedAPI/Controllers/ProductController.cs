using Authenticated_Models;
using AuthenticatedAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticatedAPI.Controllers;

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
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return Ok(await _info.products.ToListAsync());
        }

        [HttpGet("{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
        {
            var category = await _info.categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(category.Products);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            _info.products.Add(product);
            await _info.SaveChangesAsync();

            return Ok("Product added successfully");
        }
    }

