using ApiProductos.Interfaces.Repositories;
using ApiProductos.Models;
using ApiProductos.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _iProduct;

        public ProductController(IProduct iProduct)
        {
            _iProduct = iProduct ?? throw new ArgumentNullException(nameof(iProduct));
        }

        [HttpGet, Route("GetProducts")]
        public async Task<PetitionResponse> GetProducts() => 
            await _iProduct.GetProducts();

        [HttpGet, Route("GetProductById")]
        public async Task<PetitionResponse> GetProductById(int id) =>
            await _iProduct.GetProductById(id);

        [HttpPost, Route("AddProduct")]
        public async Task<PetitionResponse> AddProduct(Product product) =>
            await _iProduct.AddProduct(product);

        [HttpGet, Route("SPGetProducts")]
        public async Task<PetitionResponse> SPGetProducts() =>
            await _iProduct.SPGetProducts();

        [HttpPost, Route("SPAddProduct")]
        public async Task<PetitionResponse> SPAddProduct(Product product) =>
            await _iProduct.SPAddProduct(product);

        [HttpPut, Route("SPUpdateProduct")]
        public async Task<PetitionResponse> SPUpdateProduct(Product product) =>
            await _iProduct.SPUpdateProduct(product);

        [HttpDelete, Route("SPDeleteProduct")]
        public async Task<PetitionResponse> SPDeleteProduct(int id) =>
            await _iProduct.SPDeleteProduct(id);


    }
}
