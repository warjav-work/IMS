using IMS.CoreBusiness;

namespace IMS.UseCases.Products.Interfaces
{
    public interface IViewProductsByNameUseCase
    {
        Task<List<Product>> ExecuteAsync(string name = "");
    }
}