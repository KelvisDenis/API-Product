using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products.Application.DTOs;
using Products.Application.Services.Interface;
using Products.Domain.Exceptions;
using Products.Domain.Models;
using Products.Infrastruture.Repository.Interfaces;
using ZXing;

namespace Products.Application.Services.Implementation
{
    public class ProductService:IProductService
    {
        private readonly ILogger<IProductService> _logger;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, ILogger<IProductService> logger){
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<bool> AddProductAsync(CreateProductDTO? product){
            try{
                var productMapp = new ProductModel{
                    Id = 0,
                    Name = product.Name,
                    Amount = product.Amount,
                    Price = product.Price,
                };
                string code = "1234567810";
                var barcode = new BarcodeWriter<BinaryBitmap>
                {
                    Format = BarcodeFormat.CODE_128, 
                    Options = new ZXing.Common.EncodingOptions{
                        Width = 300,
                        Height = 100,
                        Margin = 10
                    }
                };
                productMapp.Barcode = barcode.ToString();
                productMapp.Code = code;
                var response = await _productRepository.AddProductAsync(productMapp);
                _logger.LogInformation("Succes in add product in services class");
                return response;


            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<ViewProductDTO> GetProductByIdAsync(int? id){
            try{
                var product = await _productRepository.GetProductByIdAsync(id);
                if(product == null) throw new NotFoundException("Not Found Product");
                var productDTO = new ViewProductDTO
                {
                    Name = product.Name,
                    Amount = product.Amount,
                    Barcode = product.Barcode,
                    Code = product.Code,
                    Price = product.Price
                };
                _logger.LogInformation($"Success in get product by id={id} in service");
                return productDTO;



            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<ViewProductDTO> GetProductByNameAsync(string? name){
            try{
                var product = await _productRepository.GetProductByNameAsync(name);
                var productDTO = new ViewProductDTO
                {
                    Name = product.Name,
                    Amount = product.Amount,
                    Barcode = product.Barcode,
                    Code = product.Code,
                    Price = product.Price
                };
                _logger.LogInformation($"Success in get product by name={name} in service");
                return productDTO;



            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return null;
            }
        }
       
        public async Task<ViewProductDTO> GetProductByCode(string? code){
            try{
                var product = await _productRepository.GetProductByCode(code);
                var productDTO = new ViewProductDTO
                {
                    Name = product.Name,
                    Amount = product.Amount,
                    Barcode = product.Barcode,
                    Code = product.Code,
                    Price = product.Price
                };
                _logger.LogInformation($"Success in get product by code={code} in service");
                return productDTO;



            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<IEnumerable<ViewProductDTO>> GetAllProductsAsync(){
            try{
                var product = await _productRepository.GetAllProductsAsync();
                var productsDTO = new List<ViewProductDTO>();
                foreach(var item in product){
                    var p = new ViewProductDTO
                    {
                        Name = item.Name,
                        Amount = item.Amount,
                        Barcode = item.Barcode,
                        Code = item.Code,
                        Price = item.Price
                    };
                    productsDTO.Add(p);
                }
                _logger.LogInformation($"Success in get all product in service");
                return productsDTO;



            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<bool> UpdateProductAsync(UpdateProductDTO? product){
             try{
                var productMapp = new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Amount = product.Amount,
                    Price = product.Price, 
                    Code = product.Code,
                    Barcode = product.Barcode,
                };
                var response = await _productRepository.UpdateProductAsync(productMapp);
                _logger.LogInformation("Succes in update product in services class");
                return response;


            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return false;
            }
        }
        public async Task<bool> DeleteProductAsync(int? id){
            try{
                
                var response = await _productRepository.DeleteProductAsync(id);
                _logger.LogInformation($"Succes in remove product by id {id} in services class");
                return response;


            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}