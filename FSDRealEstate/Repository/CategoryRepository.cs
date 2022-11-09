using FSDRealEstate.Data;
using FSDRealEstate.Models;

namespace FSDRealEstate.Repository
{
    public class CategoryRepository : ICategory
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Category Create(Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(int id)
        {
            Category category = _context.Category.Find(id);
            if (category != null)
            {
                _context.Category.Remove(category);
                _context.SaveChanges();
            }
            return category;
        }

        public Category GetObject(int id)
        {
            return _context.Category.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Category;
        }

        public Category Update(Category change)
        {
            var o = _context.Category.Attach(change);
            o.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return change;
        }

    }
}
