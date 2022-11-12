using FSDRealEstate.Models;
using System.ComponentModel.DataAnnotations;

namespace FSDRealEstate.ViewModel
{
    public class ImageViewModel
    {
        public int Property_id { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
