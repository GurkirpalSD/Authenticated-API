using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticatedAPI.Data;
using Authenticated.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticatedAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly AppDataContext _info;

    public ShoppingCartController(ILogger<ShoppingCartController> logger,AppDataContext information)
    {
        _info = information;
    }

    [HttpGet]
    public async Task<List<ProductModel>?> GetProducts()
    {
        var user = User.Identity?.Name ?? string.Empty;
        var userShoppingCart =  await _info.ShoppingCarts.Where(userShoppingCart => userShoppingCart.User == user).FirstOrDefaultAsync();
       

        return userShoppingCart?.Product; 
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var user = User.Identity?.Name?? string.Empty;
        var userShoppingCart = await _info.ShoppingCarts.Where(userShoppingCart => userShoppingCart.User == user).FirstOrDefaultAsync();
        userShoppingCart?.Product.RemoveAll(products => products.Id == id);
        await _info.SaveChangesAsync();
     
            return Ok();
        }

  

    [HttpPost]
    public async Task<IActionResult> CreateProduct(int id)
    {
        var user = User.Identity?.Name ?? string.Empty;
        var userShoppingCart = await _info.ShoppingCarts.Where(userShoppingCart => userShoppingCart.User == user).FirstOrDefaultAsync();
        if (userShoppingCart is null)
        {
            _info.Add(new ShoppingCart()
            {
                User = user,
                Product = [new ProductModel()
                {
                    Id = id
                }]            });
        }

      else{
        userShoppingCart.Product.Add(new ProductModel() {Id = id});
      }
      await _info.SaveChangesAsync();
      return Ok();
    }

}
