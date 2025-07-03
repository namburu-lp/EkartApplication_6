using ECommerceAPP.DTOS;

namespace ECommerceAPP.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<ProductDto> AddProductAsync(ProductDto productDto);
        Task<ProductDto> UpdateProductAsync(ProductDto productDto);
        Task<bool> DeleteProductAsync(int productId);

        Task<IEnumerable<ProductDto>> SearchDiscontinuedAsync();
        Task<IEnumerable<ProductDto>> SearchByCategoryAsync(string category);
        Task<IEnumerable<ProductDto>> SearchBySupplierAsync(string supplier);
        Task<IEnumerable<ProductDto>> SearchByUnitStockAsync(int stock);
        Task<IEnumerable<ProductDto>> SearchByUnitsOnOrderGreaterThanZeroAsync();
        Task<IEnumerable<ProductDto>> SearchByOutOfStockAsync();

        bool IdExists(int id);
        bool CompanyNameExists(string companyName);
        bool CategoryNameExists(string categoryName);
        bool UnitInStockExists(int unitStock);
    }
}
