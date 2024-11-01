using ApiProductos.Data;
using ApiProductos.Interfaces.Repositories;
using ApiProductos.Models;
using ApiProductos.Models.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiProductos.Interfaces
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<PetitionResponse> AddProduct(Product product)
        {
            try
            {
                if (await ExistproductById(product.Id))
                    return new PetitionResponse
                    {
                        Success = false,
                        Result = null,
                        Message = $"El producto con Id {product.Id} ya existe en el sistema.",
                        Url = "product/AddProduct"
                    };

                if (await ExistproductByName(product.Name))
                    return new PetitionResponse
                    {
                        Success = false,
                        Result = null,
                        Message = $"El producto {product.Name} ya existe en el sistema.",
                        Url = "product/AddProduct"
                    };

                _db.Products.Add(product);

                if (!await saveChanges())
                    return new PetitionResponse
                    {
                        Success = false,
                        Result = null,
                        Message = $"No fue posible registrar el producto.",
                        Url = "product/AddProduct"
                    };

                return new PetitionResponse
                {
                    Success = true,
                    Result = product,
                    Message = $"El producto {product.Name} fue registrado correctamente.",
                    Url = "product/AddProduct"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    Success = false,
                    Result = null,
                    Message = $"Un error ocurrio. ${ex.Message}",
                    Url = "product/AddProduct"
                };
            }      
        }

        public async Task<bool> ExistproductById(int productId)
        {
           return await _db.Products.AnyAsync(x => x.Id == productId);
        }

        public async Task<bool> ExistproductByName(string productName)
        {
            return await _db.Products.AnyAsync(x => x.Name.ToLower().Trim() == productName.ToLower().Trim());
        }

        public async Task<PetitionResponse> GetProductById(int id)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);

                return new PetitionResponse
                {
                    Success = true,
                    Result = product,
                    Message = product != null ? "Consulta realizada con éxito." : "El producto no se encuentra regisatrado en el sistema.",
                    Url = "product/GetProductById"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    Success = false,
                    Result = null,
                    Message = $"Un error ocurrio. {ex.Message}",
                    Url = "product/GetProductById"
                };
            }
           
        }

        public async Task<PetitionResponse> GetProducts()
        {
            try
            {
                var products = await _db.Products.ToListAsync();

                return new PetitionResponse
                {
                    Success = true,
                    Result = products,
                    Message = $"Consulta realizada con éxito.",
                    Url = "product/GetProducts"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    Success = false,
                    Result = null,
                    Message = $"Un error ocurrio. {ex.Message}",
                    Url = "product/GetProducts"
                };
            }
        }

        public async Task<bool> saveChanges()
        {
           return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<PetitionResponse> SPAddProduct(Product product)
        {
            try
            {
                var resultSP = await _db.Database.ExecuteSqlInterpolatedAsync
                    ($"EXEC [dbo].[SPAddProduct] {product.Name}, {product.Description}, {product.Price}, {product.Stock}");

                return new PetitionResponse
                {
                    Success = true,
                    Result = product,
                    Message = resultSP.ToString() == "-1" ? "El producto no existe en el sistema" : $"Producto agregado con éxito.",
                    Url = "product/SPAddProduct"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    Success = false,
                    Result = null,
                    Message = $"Un error ocurrio. {ex.Message}",
                    Url = "product/SPAddProduct"
                };
            }
        }

        public async Task<PetitionResponse> SPDeleteProduct(int id)
        {
            try
            {
                var resultSP = await _db.Database.ExecuteSqlInterpolatedAsync
                    ($"EXEC [dbo].[SPDeleteProduct] {id}");

                return new PetitionResponse
                {
                    Success = true,
                    Result = resultSP.ToString(),
                    Message = resultSP.ToString() == "-1" ? "El producto no existe en el sistema" : $"Producto eliminado con éxito.",
                    Url = "product/SPDeleteProduct"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    Success = false,
                    Result = null,
                    Message = $"Un error ocurrio. {ex.Message}",
                    Url = "product/SPDeleteProduct"
                };
            }
        }

        public async Task<PetitionResponse> SPGetProducts()
        {
            try
            {
                var resultSP = await _db.Products.FromSqlInterpolated($"EXEC [dbo].[SPGetProducts]")
                                          .ToListAsync();

                return new PetitionResponse
                {
                    Success = true,
                    Result = resultSP,
                    Message = "Consulta realizada con éxito.",
                    Url = "product/STUGetProducts"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    Success = false,
                    Result = null,
                    Message = $"Un error ocurrio. {ex.Message}",
                    Url = "product/SPUGetProducts"
                };
            }
        }

        public async Task<PetitionResponse> SPUpdateProduct(Product product)
        {
            try
            {
                var resultSP = await _db.Database.ExecuteSqlInterpolatedAsync
                    ($"EXEC [dbo].[SPUpdateProduct] {product.Id},{product.Name}, {product.Description}, {product.Price}, {product.Stock}");

                return new PetitionResponse
                {
                    Success = true,
                    Result = product,
                    Message = resultSP.ToString() == "-1" ? "El producto no existe en el sistema" : $"Producto actualizado con éxito.",
                    Url = "product/SPUpdateProduct"
                };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    Success = false,
                    Result = null,
                    Message = $"Un error ocurrio. {ex.Message}",
                    Url = "product/GetProducts"
                };
            }
                    
        }
    }
}
