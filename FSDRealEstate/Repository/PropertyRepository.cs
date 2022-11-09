using FSDRealEstate.Models;
using FSDRealEstate.Data;

namespace FSDRealEstate.Repository
{
    public class PropertyRepository : IProperty
    {
        private readonly ApplicationDbContext _context;

        public PropertyRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public Property Create(Property o)
        {
            _context.Property.Add(o);
            _context.SaveChanges();
            return o;
        }

        public Property Delete(int id)
        {
            Property o = _context.Property.Find(id);
            if (o != null)
            {
                _context.Property.Remove(o);
                _context.SaveChanges();
            }
            return o;
        }

        public Property GetObject(int id)
        {
            return _context.Property.Find(id);
        }

        public IEnumerable<Property> GetAll()
        {
            return _context.Property;
        }

        public Property Update(Property change)
        {
            var o = _context.Property.Attach(change);
            o.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return change;
        }

    }
}
