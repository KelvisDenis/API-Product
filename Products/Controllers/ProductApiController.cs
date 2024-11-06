using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Products.Application.DTOs;
using Products.Application.Services.Interface;
using Products.Domain.Exceptions;

namespace Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductApiController> _logger;
        public ProductApiController(IProductService productService, ILogger<ProductApiController>logger){
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("v1/Product/Get/id={id}")]
        public async Task<IActionResult> GetByID(int? id){
            if(id == null) throw new BadRequestException("Param ID is null");
            try{
                var product = await _productService.GetProductByIdAsync(id);
                if(product == null) throw new BadRequestException($"Product Not Found by Id {id}");
                _logger.LogInformation("Success in get product by ID"); 
                return Ok(product);
            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return BadRequest($"An erro while get by ID = {id}");
            }
        }
        [HttpGet("v1/Product/Get/name={name}")]
        public async Task<IActionResult> GetByName(string? name){
            if(name == null) return BadRequest("Param name is null");
            try{
                var product = await _productService.GetProductByNameAsync(name);
                if(product == null) throw new BadRequestException($"Product Not Found by name {name}");
                _logger.LogInformation("Success in get product by Name");
                return Ok(product);
            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return BadRequest($"An erro while get by Name = {name}");
            }
        }
        [HttpGet("v1/Product/Get/code={code}")]
        public async Task<IActionResult> GetByCode(string? code){
            if(code == null) return BadRequest("Param code is null");
            try{
                var product = await _productService.GetProductByCode(code);
                if(product == null) throw new BadRequestException($"Product Not Found by code {code}");
                _logger.LogInformation("Success in get product by Code");
                return Ok(product);
            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return BadRequest($"An erro while get by code = {code}");
            }
        }
        [HttpGet("v1/Product/GetAll")]
        public async Task<IActionResult> GetAll(){
            try{
                var product = await _productService.GetAllProductsAsync();
                if(product == null) throw new BadRequestException($"Products Not Found");
                _logger.LogInformation("Success in get product by Barcode");
                return Ok(product);

            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return BadRequest($"An erro while get all products");
            }
        }


        [HttpPost("v1/Product/Add")]
        public async Task<IActionResult> Add([FromBody] CreateProductDTO? model){
            if(model == null) return BadRequest("Param model is null");
            try{
                var response = await _productService.AddProductAsync(model);
                if(response == false) throw new BadRequestException($"Not already added Product");
                _logger.LogInformation("Success in add product");
                return Ok(response);

            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return BadRequest($"An erro while add product");
            }
        }
        [HttpPut("v1/Product/update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO? model){
            if(model == null) return BadRequest("Param model is null");
            try{
                var response = await _productService.UpdateProductAsync(model);
                if(response == false) throw new BadRequestException($"Product Not update by Name {model.Name}");
                _logger.LogInformation("Success in update product");
                return Ok(response);

            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return BadRequest($"An erro while update product");
            }
        }
        [HttpDelete("v1/Product/Delete/id={id}")]
        public async Task<IActionResult> Delete(int? id){
            if(id == null) return BadRequest("Param ID is null");
            try{
                var response = await _productService.DeleteProductAsync(id);
                if(response == false) throw new BadRequestException($"Product Not Found by Id {id} for deleted");
                _logger.LogInformation("Success in delete product");
                return Ok(response);

            }catch(Exception ex){
                _logger.LogError(ex.Message);
                return BadRequest($"An erro while delete product");
            }
        }
    }
}