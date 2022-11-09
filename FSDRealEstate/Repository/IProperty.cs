using FSDRealEstate.Models;

namespace FSDRealEstate.Repository
{
    public interface IProperty
    {
        public Property GetObject(int Id);
        public IEnumerable<Property> GetAll();
        public Property Update(Property changes);
        public Property Delete(int id);
        public Property Create(Property property);
    }
}
