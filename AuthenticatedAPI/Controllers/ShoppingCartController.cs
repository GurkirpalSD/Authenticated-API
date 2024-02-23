using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticatedAPI.Data;
using Authenticated_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
    public async Task<List<ShoppingCart>?> GetProducts()
    {
        var user = User.Identity?.Name ?? string.Empty;
        var userShoppingCart =  await _info.ShoppingCart
        .Where(userShoppingCart => userShoppingCart.User == user)
        .ToListAsync();      

        return userShoppingCart?.Product; 
    }

    


    [HttpPost]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var user = User.Identity?.Name?? string.Empty;
        var userShoppingCart = await _info.ShoppingCarts.Where(userShoppingCart => userShoppingCart.User == user).ToListAsync();
        userShoppingCart?.Product.RemoveAll(products => products.Id == id);
        await _info.SaveChangesAsync();
     
            return Ok();
        }

  

    [HttpPost]
    public async Task<IActionResult> CreateProduct(int id)
    {
        var user = User.Identity?.Name ?? string.Empty;
        var userShoppingCart = await _info.ShoppingCarts.Where(userShoppingCart => userShoppingCart.User == user).ToListAsync();
        if (userShoppingCart is null)
        {
            _info.Add(new ShoppingCart()
            {
                User = user,
                Product = [new Product()
                {
                    Id = id
                }]            });
        }

      else{
        userShoppingCart.Product.Add(new Product() {Id = id});
      }
      await _info.SaveChangesAsync();
      return Ok();
    }

}
