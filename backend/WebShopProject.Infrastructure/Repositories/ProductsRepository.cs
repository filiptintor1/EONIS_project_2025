using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Domain.Repositories;
using WebShopProject.Domain.Entities;
using WebShopProject.Infrastructure.Database;

namespace WebShop.Infrastructure.Repositories
{
    internal class ProductsRepository(ProjectDbContext dbContext) : IProductsRepository
    {
        
        public async Task<Guid> CreateProduct(Product p)
        {

            dbContext.Products.Add(p);
            await dbContext.SaveChangesAsync();
            return p.ProductId;
        }

        public async Task DeleteProduct(Product p)
        {

            dbContext.Remove(p);
            await dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Product>, int)> GetAllProducts(int pageSize, int pageNumber, string? searchPhrase, string? category,
            string? kind, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseNormalized = searchPhrase?.ToLower();
            var categoryNormal = category?.ToLower();
            var kindNormal = kind?.ToLower();

            var baseQuery = dbContext.Products.Where(
                p => (searchPhrase == null || p.Name.ToLower().Contains(searchPhraseNormalized)) && (category == null || p.Category.ToLower() == categoryNormal) &&
                     (kind == null || p.KindOfHoney.ToLower() == kindNormal)
            ); 
            var totalCount = await baseQuery.CountAsync();
            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Product, object>>>
                {
                    {nameof(Product.Name), x => x.Name},
                    {nameof(Product.Price), x => x.Price}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending ?
                     baseQuery.OrderBy(selectedColumn)
                     : baseQuery.OrderByDescending(selectedColumn);
            }
            var products = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (products, totalCount);
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.ProductId == productId);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var p = await dbContext.Products
            .Where(x => x.Category == category)
            .ToListAsync();
            return p;
        }

        public async Task<IEnumerable<Product>> GetProductsByKind(string kind)
        {
            var products = await dbContext.Products
           .Where(u => u.KindOfHoney == kind)
           .ToListAsync();
            return products;
        }

        public async Task SaveChanges()
        {
             await dbContext.SaveChangesAsync();
        }
    }
}
