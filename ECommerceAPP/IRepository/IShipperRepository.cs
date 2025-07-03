using ECommerceAPP.DTOS;
using ECommerceAPP.Models;

namespace ECommerceAPP.IRepository
{
    public interface IShipperRepository
    {
   
        Task<Shipper> CreateShipper(Shipper shipper);
        Task<List<Shipper>> GetAllShipper();
        Task<Shipper> GetShipperById(int id);
        Task<Shipper> Updateshipper(Shipper shipper);
        Task<Shipper> DeleteShipperById(int id);
    }
}
