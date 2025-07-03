using ECommerceAPP.DTOS;

namespace ECommerceAPP.IRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> GetAllCategories();
        void AddCategory(CategoryDto categoryDto);
        void UpdateCategory(int id, string description);
    }
}
