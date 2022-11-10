using FSDRealEstate.Models;

namespace FSDRealEstate.Repository
{
    public interface IImage
    { 
        public Image GetObject(int Id);
        public IEnumerable<Image> GetAll();
        public Image Update(Image change);
        public Image Delete(int id);
        public Image Create(Image obj);
        public void DeleteList(int pid);
    }
}
