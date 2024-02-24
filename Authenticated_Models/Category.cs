using Authenticated_Models;

namespace Authenticated_Models;
public class Category
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public object Products { get; set; }
}
