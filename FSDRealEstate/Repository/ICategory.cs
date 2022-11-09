using FSDRealEstate.Models;

namespace FSDRealEstate.Repository
{
    public interface ICategory
    { 
        public Category GetObject(int Id);
        public IEnumerable<Category> GetAll();
        public Category Update(Category change);
        public Category Delete(int id);
        public Category Create(Category obj);
    }
}
