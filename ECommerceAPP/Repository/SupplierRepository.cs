using AutoMapper;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPP.Repository
{
   

    public class SuppliersRepository : ISupplierRepository
    {
        private readonly Zecommerce123Context _context;
        private readonly IConfiguration _configuration;
       

        public SuppliersRepository(Zecommerce123Context context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            
        }

        public async Task<Supplier> CreateSupplier(Supplier supplier)

        {

            var result = await _context.Suppliers.AddAsync(supplier);

            await _context.SaveChangesAsync();


            return result.Entity;

        }



        public async Task<List<Supplier>> GetAllSupplier()

        {

            return await _context.Suppliers.ToListAsync();

        }

        public async Task<List<Supplier>> GetSupplierByCountry(string country)

        {

            var ans = await _context.Suppliers.Where(x => x.Country == country).ToListAsync();

            return ans;

        }
        public async Task<Supplier> GetSupplierById(int id)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == id);
        }

        public async Task<bool> UpdateSupplier(Supplier supplier)
        {
            var existing = await _context.Suppliers
                .FirstOrDefaultAsync(s => s.SupplierId == supplier.SupplierId);

            if (existing != null)
            {

               // _mapper.Map(supplier, existing);
                existing.Address = supplier.Address;
                existing.City = supplier.City;
                existing.PostalCode = supplier.PostalCode;
                existing.Country = supplier.Country;

                await _context.SaveChangesAsync(); // Save changes instead of uploading

                return true;

            }

            return false;
        }


        public bool CountryExists(string country)

        {

            bool res = _context.Suppliers.Any(s => s.Country == country);

            return res;

        }

        public bool IdExists(int id)

        {

            bool res = _context.Suppliers.Any(s => s.SupplierId == id);

            return res;

        }

    }

}

