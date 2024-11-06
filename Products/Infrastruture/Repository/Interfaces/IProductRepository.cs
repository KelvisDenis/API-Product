using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Domain.Models;

namespace Products.Infrastruture.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> AddProductAsync(ProductModel? product);
        Task<ProductModel> GetProductByIdAsync(int? id);
        Task<ProductModel> GetProductByNameAsync(string? name);
        Task<ProductModel> GetProductByCode(string? code);
        Task<IEnumerable<ProductModel>> GetAllProductsAsync(); 
        Task<bool> UpdateProductAsync(ProductModel? product);
        Task<bool> DeleteProductAsync(int? id);
    }
}