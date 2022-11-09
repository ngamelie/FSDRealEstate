using System.ComponentModel.DataAnnotations;

namespace FSDRealEstate.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Category maximum is 100 characters.")]
        public string CategoryName { get; set; }
    }
}
