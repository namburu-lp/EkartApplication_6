using AutoMapper;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPP.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Zecommerce123Context _context;
        private readonly IMapper _mapper;

        public ProductRepository(Zecommerce123Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>> SearchDiscontinuedAsync()
        {
            var products = await _context.Products.Where(p => p.Discontinued == true).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<IEnumerable<ProductDto>> SearchByCategoryAsync(string category)
        {
            var cat = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == category);
            if (cat == null) return Enumerable.Empty<ProductDto>();

            var products = await _context.Products.Where(p => p.CategoryId == cat.CategoryId).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchBySupplierAsync(string supplier)
        {
            var sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.CompanyName == supplier);
            if (sup == null) return Enumerable.Empty<ProductDto>();

            var products = await _context.Products.Where(p => p.SupplierId == sup.SupplierId).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchByUnitStockAsync(int stock)
        {
            var products = await _context.Products.Where(p => p.UnitsInStock == stock).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchByUnitsOnOrderGreaterThanZeroAsync()
        {
            var products = await _context.Products
                .Where(p => p.UnitsOnOrder > 0)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchByOutOfStockAsync()
        {
            var products = await _context.Products.Where(p => p.UnitsInStock == 0).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public bool IdExists(int id) => _context.Products.Any(p => p.ProductId == id);
        public bool CompanyNameExists(string companyName) => _context.Suppliers.Any(s => s.CompanyName == companyName);
        public bool CategoryNameExists(string categoryName) => _context.Categories.Any(c => c.CategoryName == categoryName);
        public bool UnitInStockExists(int unitStock) => _context.Products.Any(p => p.UnitsInStock == unitStock);

    }
}


