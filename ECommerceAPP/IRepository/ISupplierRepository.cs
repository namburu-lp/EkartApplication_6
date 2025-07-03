using ECommerceAPP.DTOS;
using ECommerceAPP.Models;

namespace ECommerceAPP.IRepository
{
    public interface ISupplierRepository
    {
         Task<Supplier> CreateSupplier(Supplier supplier);
        Task<List<Supplier>> GetAllSupplier();
        Task<Supplier> GetSupplierById(int id);

        Task<List<Supplier>> GetSupplierByCountry(string country);
        Task<bool> UpdateSupplier(Supplier supplier);
        bool CountryExists(string country);
        bool IdExists(int id);
    }
}

