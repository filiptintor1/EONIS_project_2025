using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopProject.Domain.Entities;

namespace WebShop.Domain.Repositories;

public interface IProductsRepository
{
    Task<(IEnumerable<Product>, int)> GetAllProducts(int pageSize, int pageNumber, string? searchPhrase, string? category, string? kind, string? sortBy, SortDirection sortDirection);
    Task<Product?> GetProductById(Guid productId);
    Task<IEnumerable<Product>> GetProductsByKind(string kind);
    Task<IEnumerable<Product>> GetProductsByCategory(string category);
    Task<Guid> CreateProduct(Product p);
    Task DeleteProduct(Product p);
    Task SaveChanges();


}
