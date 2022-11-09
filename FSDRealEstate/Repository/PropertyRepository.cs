using FSDRealEstate.Models;
using FSDRealEstate.Data;

namespace FSDRealEstate.Repository
{
    public class PropertyRepository : IProperty
    {
        private readonly ApplicationDbContext context;

        public PropertyRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Property Create(Property o)
        {
            context.Property.Add(o);   
            context.SaveChanges();
            return o;
        }

        public Property Delete(int id)
        {
            Property o = context.Property.Find(id);
            if (o != null)
            {
                context.Property.Remove(o);
                context.SaveChanges();
            }
            return o;
        }

        public Property GetObject(int id)
        {
            return context.Property.Find(id);
        }

        public IEnumerable<Property> GetAll()
        {
            return context.Property;
        }

        public Property Update(Property change)
        {
            var o = context.Property.Attach(change);
            o.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return change;
        }

    }
}
