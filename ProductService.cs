using DotNetCrudProject.Models;

namespace DotNetCrudProject.Services;

public class ProductService
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Laptop", Price = 750 },
        new Product { Id = 2, Name = "Telefon", Price = 450 }
    };

    public List<Product> GetAll()
    {
        return Products;
    }

    public Product? GetById(int id)
    {
        return Products.FirstOrDefault(p => p.Id == id);
    }

    public Product Create(Product product)
    {
        var newId = Products.Any() ? Products.Max(p => p.Id) + 1 : 1;

        var newProduct = new Product
        {
            Id = newId,
            Name = product.Name,
            Price = product.Price
        };

        Products.Add(newProduct);
        return newProduct;
    }

    public bool Update(int id, Product updatedProduct)
    {
        var existingProduct = GetById(id);

        if (existingProduct is null)
        {
            return false;
        }

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;

        return true;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);

        if (product is null)
        {
            return false;
        }

        Products.Remove(product);
        return true;
    }
}
