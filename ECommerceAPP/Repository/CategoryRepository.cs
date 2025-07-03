using AutoMapper;
using ECommerceAPP.DTOS;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Zecommerce123Context _context;
        private readonly IMapper _mapper;

        public CategoryRepository(Zecommerce123Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public void AddCategory(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(int id, string description)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                category.Description = description;
                _context.SaveChanges();
            }
        }
    }
}
