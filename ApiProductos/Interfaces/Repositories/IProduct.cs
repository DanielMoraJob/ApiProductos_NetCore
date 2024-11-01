using ApiProductos.Models;
using ApiProductos.Models.Shared;

namespace ApiProductos.Interfaces.Repositories
{
    public interface IProduct
    {
        Task<PetitionResponse> GetProducts();
        Task<PetitionResponse> GetProductById(int Id);
        Task<PetitionResponse> AddProduct(Product product);
        Task<bool> ExistproductById(int productId);
        Task<bool> ExistproductByName(string productName);
        Task<bool> saveChanges();

        Task<PetitionResponse> SPAddProduct(Product product);
        Task<PetitionResponse> SPUpdateProduct(Product product);
        Task<PetitionResponse> SPGetProducts();
        Task<PetitionResponse> SPDeleteProduct(int id);

    }
}
