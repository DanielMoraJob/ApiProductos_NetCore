using ApiProductos.Controllers;
using ApiProductos.Data;
using ApiProductos.Interfaces;
using ApiProductos.Interfaces.Repositories;
using ApiProductos.Models;
using ApiProductos.Models.Shared;
using Moq;

namespace Test_Productos
{
    public class productTest
    {

        private readonly ProductController _controller;
        private readonly Mock<IProduct> _service;

        public productTest()
        {
            _service = new Mock<IProduct>();
            _controller = new ProductController(_service.Object);
        }

        [Fact]
        public void GetProducts()
        {
            _service.Setup(service => service.GetProducts()).ReturnsAsync(new PetitionResponse
            {
                Result = new List<Product>()
            });

            var result = _controller.GetProducts();
            //Verificar que el resultado no llegue vacío
            //Verificar que el resultado sea tipo petitionResponse
            //Verificar que el resultado del pettitionResponse sea una lista de productos 
            Assert.NotNull(result);
            Assert.IsType<Task<PetitionResponse>>(result);
            Assert.IsType<List<Product>>(result.Result.Result);
        }

        [Fact]
        public void GetProductById()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 3900,
                Stock = 5
            };

            _service.Setup(service => service.GetProductById(1)).ReturnsAsync(new PetitionResponse
            {
                Result = product
            });

            var result = _controller.GetProductById(1);
            //Verificar que el resultado no llegue vacío
            //Verificar que el resultado sea tipo petitionResponse
            //Verificar que el resultado del pettitionResponse sea una producto
            //Verificar el mensaje de respuesta sea el correcto
            Assert.NotNull(result);
            Assert.IsType<Task<PetitionResponse>>(result);
            Assert.Equal(result.Result.Result, product);
        }

        [Fact]
        public void AddProduct()
        {

            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 3900,
                Stock = 5
            };

            _service.Setup(service => service.AddProduct(product)).ReturnsAsync(new PetitionResponse
            {
                Result = product
            });

            var result = _controller.AddProduct(product);
            //Verificar que el resultado no llegue vacío
            //Verificar que el resultado sea tipo petitionResponse
            //verificar que el resultado del pettitionResponse sea una producto
            Assert.NotNull(result);
            Assert.IsType<Task<PetitionResponse>>(result);
            Assert.Equal(product, result.Result.Result);
        }

        [Fact]
        public void SPUpdateProduct()
        {

            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 3900,
                Stock = 5
            };

            _service.Setup(service => service.SPUpdateProduct(product)).ReturnsAsync(new PetitionResponse
            {
                Result = product,
                Message = "Producto actualizado con éxito."
            });

            var result = _controller.SPUpdateProduct(product);
            //Verificar que el resultado no llegue vacío
            //Verificar que el resultado sea tipo petitionResponse
            //verificar que el resultado del pettitionResponse sea una producto
            //Verificar el mensaje de respuesta sea el correcto
            Assert.NotNull(result);
            Assert.IsType<Task<PetitionResponse>>(result);
            Assert.Equal(product, result.Result.Result);
            Assert.Equal("Producto actualizado con éxito.", result.Result.Message);
        }
        [Fact]
        public async Task SPUpdateProduct_WhenUpdateFails()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 3900,
                Stock = 5
            };

            _service.Setup(service => service.SPUpdateProduct(product))
                .ReturnsAsync(new PetitionResponse
                {
                    Success = false,
                    Message = "Error al actualizar el producto."
                });

            var result = await _controller.SPUpdateProduct(product);
            //Verificar que el resultado no llegue vacío
            //Verificar que el resultado sea fallo
            //verificar que el resultado sea nulo
            //Verificar el mensaje de respuesta sea el correcto de fallo
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Result);
            Assert.Equal("Error al actualizar el producto.", result.Message);
        }

        [Fact]
        public async Task SPUDeleteProduct()
        {

            _service.Setup(service => service.SPDeleteProduct(1))
                .ReturnsAsync(new PetitionResponse
                {
                    Success = true
                });

            var result = await _controller.SPDeleteProduct(1);
            //Verificar que el resultado no llegue vacío
            //Verificar que el resultado sea exitoso
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

    }
}