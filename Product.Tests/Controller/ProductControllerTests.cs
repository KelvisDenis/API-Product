using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Products.Application.DTOs;
using Products.Application.Services.Implementation;
using Products.Application.Services.Interface;
using Products.Controllers;
using Products.Domain.Models;
using Xunit;

namespace Product.Tests.Controller
{
    public class ProductControllerTests
    {
        private readonly ProductApiController _controller;
        private readonly Mock<IProductService> _mock;
        public ProductControllerTests(){
            _mock = new Mock<IProductService>();
            var logger = new NullLogger<ProductApiController>();
            _controller = new ProductApiController(_mock.Object, logger);
        }


        [Fact]
        public void AddProduct_ReturnOKResult_WithProduct()
        {
            var product = new CreateProductDTO{
                Name = "Teste1",
                Amount = 10,
                Price = 12,
            };
           _mock.Setup(x => x.AddProductAsync(product)).ReturnsAsync(true);

            var result = _controller.Add(product);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.True((bool)okResult.Value);
        }


        [Fact]
        public void AddProduct_ReturnBadResult_WithProductNotAdded()
        {
            var product = new CreateProductDTO{
                Name = "",
                Amount = 0,
                Price = 0,
            };
           _mock.Setup(x => x.AddProductAsync(product)).ReturnsAsync(false);

            var result = _controller.Add(product);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var returnBadRequestResult = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("An erro while add product", badRequestResult.Value);
        } 

        [Fact]
        public void  GetProductById_ReturnsOkResult_WithProduct()
        {
            var productId = 1;
            var product = new ViewProductDTO{
                Name = "TesteGetByID",
                Amount = 0,
                Price = 0,
                Barcode = "1234567890teste",
                Code = "teste123456789000"

            };
           _mock.Setup(x => x.GetProductByIdAsync(productId)).ReturnsAsync(product);

            var result = _controller.GetByID(productId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<ViewProductDTO>(okResult.Value);
            Assert.Equal(product, okResult.Value);
        } 

        [Fact]
        public void GetProductById_ReturnsBadRequestResult_WithProductNull(){
            var productId = 1;
            ViewProductDTO product = null;
            _mock.Setup(x => x.GetProductByIdAsync(productId)).ReturnsAsync(product);


            var  result = _controller.GetByID(productId);


            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal($"An erro while get by ID = {productId}", badRequestResult.Value);
        }

        [Fact]
        public void GetProductByName_ReturnsOkResult_WithProduct(){
            var productName = "teste";
            var product = new ViewProductDTO{
                Name = "teste",
                Amount = 100,
                Price = 10,
                Barcode = "12345678903",
                Code = "teste123456789000"
            };
            _mock.Setup(x => x.GetProductByNameAsync(productName)).ReturnsAsync(product);


            var  result = _controller.GetByName(productName);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultIsViewProductDTO = Assert.IsType<ViewProductDTO>(okResult.Value);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public void GetProductByCode_ReturnsOkResult_WithProduct(){
            var productCode = "12345678910";
            var product = new ViewProductDTO{
                Name = "teste",
                Amount = 100,
                Price = 10,
                Barcode = "12345678903",
                Code = "12345678910"
            };
            _mock.Setup(x => x.GetProductByCode(productCode)).ReturnsAsync(product);


            var  result = _controller.GetByCode(productCode);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultIsViewProductDTO = Assert.IsType<ViewProductDTO>(okResult.Value);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public void GetProductByCode_ReturnsBadRequest_WithProductNull(){
            var productCode = "12345678910";
            ViewProductDTO product = null;
            _mock.Setup(x => x.GetProductByCode(productCode)).ReturnsAsync(product);


            var  result = _controller.GetByCode(productCode);


            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal($"An erro while get by code = {productCode}", BadRequestResult.Value);
        }
        [Fact]
        public void GetProductByCode_ReturnsBadRequest_WithParamNull(){
            var productCode = "";
            ViewProductDTO product = null;
            _mock.Setup(x => x.GetProductByCode(productCode)).ReturnsAsync(product);


            var  result = _controller.GetByCode(productCode);


            var BadRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var resultIsString = Assert.IsType<string>(BadRequestResult.Value); 
            Assert.Equal($"An erro while get by code = {productCode}", BadRequestResult.Value);
        }

        [Fact]
        public void GetProductAll_ReturnsOkResult_WithProduct(){
            var products = new List<ViewProductDTO>();
            var product = new ViewProductDTO{
                Name = "teste",
                Amount = 100,
                Price = 10,
                Barcode = "12345678903",
                Code = "12345678910"
            };
            products.Add(product);
            _mock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);


            var  result = _controller.GetAll();


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultsIsIenumerableViewProductDTO = Assert.IsType<List<ViewProductDTO>>(okResult.Value);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public void GetProductAll_ReturnsBadResult_WithProductsNull(){
            List<ViewProductDTO> products = null;
            _mock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);


            var  result = _controller.GetAll();


            var okResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var resultsIsIenumerableIsNullViewProductDTO = Assert.IsType<string>(okResult.Value);
            Assert.Equal($"An erro while get all products", resultsIsIenumerableIsNullViewProductDTO);
        }

        [Fact]
        public void UpdateProduct_ReturnsOkResult_WithProduct(){
            var product = new UpdateProductDTO{
                Id = 1,
                Name = "Teste",
                Price = 12,
                Amount = 100,
                Barcode = "123445432",
                Code = "12345678",
            };
            _mock.Setup(x => x.UpdateProductAsync(product)).ReturnsAsync(true);


            var  result = _controller.Update(product);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultIsBool = Assert.IsType<bool>(okResult.Value);
            Assert.Equal(okResult.Value, true);
        }

        [Fact]
        public void UpdateProduct_ReturnsBadResult_WithProductNull(){
            var product = new UpdateProductDTO{
                Id = 0,
                Name = "",
                Price = 0,
                Amount = 0,
                Barcode = "",
                Code = "",
            };
            _mock.Setup(x => x.UpdateProductAsync(product)).ReturnsAsync(false);


            var  result = _controller.Update(product);


            var okResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var resultIsString = Assert.IsType<string>(okResult.Value);
            Assert.Equal(okResult.Value, "An erro while update product");
        }

        [Fact]
        public void DeleteProduct_ReturnsOkResult_WithProduct(){
            var productId = 1;
            var product = new CreateProductDTO{
                Name = "Teste",
                Price = 10,
                Amount = 120
            };
            _mock.Setup(x => x.AddProductAsync(product)).ReturnsAsync(true);
            _mock.Setup(x => x.DeleteProductAsync(productId)).ReturnsAsync(true);


            var  result = _controller.Delete(productId);


            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultIsTrue = Assert.IsType<bool>(okResult.Value);
            Assert.Equal(okResult.Value, true);
        }

         [Fact]
        public void DeleteProduct_ReturnsBadResult_WithProductNull(){
            var productId = 1;
            _mock.Setup(x => x.DeleteProductAsync(productId)).ReturnsAsync(false);


            var  result = _controller.Delete(productId);


            var badResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var resultIsTrue = Assert.IsType<string>(badResult.Value);
            Assert.Equal(badResult.Value, "An erro while delete product");
        }


    }
}