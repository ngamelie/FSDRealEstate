using System.ComponentModel.DataAnnotations;

namespace FSDRealEstate.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int Property_id { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Category maximum is 200 characters.")]
        public string ImageUrl { get; set; }
    }
}
