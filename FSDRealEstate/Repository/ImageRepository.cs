using Azure.Storage.Blobs;
using FSDRealEstate.Data;
using FSDRealEstate.Models;
using Microsoft.Extensions.Configuration;

namespace FSDRealEstate.Repository
{
    public class ImageRepository : IImage
    { 
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _Configuration;

        public ImageRepository(ApplicationDbContext context, IConfiguration Configuration)
        {
            this._context = context;
            _Configuration = Configuration;
        }

        public Image Create(Image obj)
        {
            _context.Image.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Image Delete(int id)
        {
            Image obj = _context.Image.Find(id);
            if (obj != null)
            {
                _context.Image.Remove(obj);
                _context.SaveChanges();
            }
            return obj;
        }

        public void DeleteList(int pid)
        {
            List<Image> images = _context.Image.Where(i=> i.Property_id == pid).ToList();

            _context.Image.RemoveRange(images);
            _context.SaveChanges();
        }

        public Image GetObject(int id)
        {
            return _context.Image.Find(id);
        }

        public IEnumerable<Image> GetAll()
        {
            return _context.Image;
        }

        public Image Update(Image change)
        {
            var o = _context.Image.Attach(change);
            o.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return change;
        }

        public void uploadBlob(IFormFile img)
        {
            BlobContainerClient containerClient = 
                new BlobContainerClient(_Configuration.GetConnectionString("AzureStorage"), 
                    _Configuration.GetConnectionString("AzureContainer"));

            var blobClient = containerClient.GetBlobClient(img.FileName);

            if(containerClient.GetBlobClient(img.FileName).Exists())
            {
                containerClient.GetBlobClient(img.FileName).Delete();
            }

            blobClient.Upload(img.OpenReadStream());
        }
    }
}
