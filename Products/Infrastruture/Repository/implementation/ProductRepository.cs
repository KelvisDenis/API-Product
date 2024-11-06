using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Exceptions;
using Products.Domain.Models;
using Products.Infrastruture.Data;
using Products.Infrastruture.Repository.Interfaces;

namespace Products.Infrastruture.Repository.implementation
{
    public class ProductRepository:IProductRepository
    {
        private readonly ILogger<IProductRepository> _logger;
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext, ILogger<IProductRepository> logger){
            _appDbContext = appDbContext;
            _logger = logger;
        }


        public async Task<bool> AddProductAsync(ProductModel? product){
            try{
                var exist = await _appDbContext.ProductSet.AnyAsync(x => x.Name == product.Name);
                if(exist) return false;
                await _appDbContext.ProductSet.AddAsync(product);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Success in add Product with name {product.Name}");
                return true;

            }catch(Exception Exception){
                _logger.LogError($"An error while add product with name {product.Name} ==> " + Exception);
                return false;
            }
        }
        public async Task<ProductModel> GetProductByIdAsync(int? id){ 
            try{
                var product = await _appDbContext.ProductSet.FirstOrDefaultAsync(x => x.Id == id);
                if(product == null) throw new NotFoundException("Product Not Found by Id = " + id);
                _logger.LogInformation($"Success in get Product with id {product.Name}");
                return product;

            }catch(Exception exception){
                _logger.LogError($"An error while get product with id {id} ==> " + exception);
                return null; 
            }
        }
        public async Task<ProductModel> GetProductByNameAsync(string? name){
            try{
                var product = await _appDbContext.ProductSet.FirstOrDefaultAsync(x => x.Name == name);
                if(product == null) throw new NotFoundException("Product Not Found by name = " + name);
                _logger.LogInformation($"Success in get Product with name {product.Name}");
                return product;

            }catch(Exception exception){
                _logger.LogError($"An error while get product with name {name} ==> " + exception);
                return null;
            }
        }
     
        public async Task<ProductModel> GetProductByCode(string? code){
             try{
                var product = await _appDbContext.ProductSet.FirstOrDefaultAsync(x => x.Code == code);
                if(product == null) throw new NotFoundException("Product Not Found by code = " + code);
                _logger.LogInformation($"Success in get Product with code {product.Code}");
                return product;

            }catch(Exception exception){
                _logger.LogError($"An error while get product with code {code} ==> " + exception);
                return null;
            }
        }
        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync(){
             try{
                var products = await _appDbContext.ProductSet.ToListAsync();
                if(products.Count == 0) throw new NotFoundException("Products Not Found");
                _logger.LogInformation($"Success in get all Products");
                return products;

            }catch(Exception exception){
                _logger.LogError($"An error while get all products  ==> " + exception);
                return null;
            }
        }
        public async Task<bool> UpdateProductAsync(ProductModel? product){
            try{
                var exist = await _appDbContext.ProductSet.AnyAsync(x => x.Id == product.Id);
                if(exist == false) throw new NotFoundException("Product Not Found by name = " + product.Name);
                _appDbContext.ProductSet.Update(product);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Success in update Product with Name {product.Name}");
                return true;

            }catch(Exception exception){
                _logger.LogError($"An error while update product with Name {product.Name} ==> " + exception);
                return false;
            }
        }
        public async Task<bool> DeleteProductAsync(int? id){
            try{
                var product = await _appDbContext.ProductSet.FindAsync(id);
                if(product == null) throw new NotFoundException("Product Not Found by id = " + id);
                _appDbContext.ProductSet.Remove(product);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Success in delete Product with id {id}");
                return true;

            }catch(Exception exception){
                _logger.LogError($"An error while delete product with id {id} ==> " + exception);
                return false;
            }
        }
    }
}