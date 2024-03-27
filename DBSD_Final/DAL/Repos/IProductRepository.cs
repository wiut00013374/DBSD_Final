using DBSD_Final.DAL.Models;

namespace DBSD_Final.DAL.Repos
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAllAsync();
        Product? GetById(int id);
        int Insert(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
