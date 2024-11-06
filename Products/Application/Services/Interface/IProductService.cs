using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Application.DTOs;
using Products.Domain.Models;

namespace Products.Application.Services.Interface
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(CreateProductDTO? product);
        Task<ViewProductDTO> GetProductByIdAsync(int? id);
        Task<ViewProductDTO> GetProductByNameAsync(string? name);
        Task<ViewProductDTO> GetProductByCode(string? code);
        Task<IEnumerable<ViewProductDTO>> GetAllProductsAsync(); 
        Task<bool> UpdateProductAsync(UpdateProductDTO? product);
        Task<bool> DeleteProductAsync(int? id);
    }
}