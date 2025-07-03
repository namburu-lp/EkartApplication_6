using AutoMapper;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPP.Repository
{
  
    public class ShipperRepository : IShipperRepository
    {
        private readonly Zecommerce123Context _context;
       

        public ShipperRepository(Zecommerce123Context context)
        {
            _context = context;
            
        }

        public async Task<Shipper> CreateShipper(Shipper shipper)
        {
            var result = await _context.Shippers.AddAsync(shipper);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Shipper> DeleteShipperById(int id)
        {
            var result = await _context.Shippers.FirstOrDefaultAsync(s => s.ShipperId == id);
            if (result != null)
            {
                _context.Shippers.Remove(result);
                await _context.SaveChangesAsync();
                return result; //  Return the deleted entity
            }
            return null;
        }


        public async Task<List<Shipper>> GetAllShipper()
        {
            return await _context.Shippers.ToListAsync();
        }

        public async Task<Shipper> GetShipperById(int id)
        {
            return await _context.Shippers.FindAsync(id);
        }

        public async Task<Shipper> Updateshipper(Shipper shipper)
        {
            var result = await _context.Shippers.FirstOrDefaultAsync(s => s.ShipperId == shipper.ShipperId);
            if (result != null)
            {
                result.CompanyName = shipper.CompanyName;
                result.Phone = shipper.Phone;
                //   result.Orders = shipper.Orders;
                await _context.SaveChangesAsync();
                return result;

            }
            return null;
        }
    }
}
