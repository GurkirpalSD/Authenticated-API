using System.Collections.Generic;
using Authenticated_Models;

namespace Authenticated_Models;
public class ShoppingCart
{
    public int Id { get; set; }
    public string? User { get; set; }
    public List<Product>? Products { get; set;  }
    
 
}