using FSDRealEstate.Models;
using System.ComponentModel.DataAnnotations;

namespace FSDRealEstate.ViewModel
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Category_id { get; set; }
        // Owner_id
        [Required]
        public string Owner_id { get; set; }
        // Address
        [Required]
        [MaxLength(200, ErrorMessage = "Address maximum is 200 characters.")]
        public string Address { get; set; }
        // Price
        [Required]
        [Range(1, 2147483647)]
        public int Price { get; set; }
        // Status
        [Required]
        [Range(0, 2147483647)]
        public EnumStatus Status { get; set; }
        // Description
        [MaxLength(2000, ErrorMessage = "Description maximum is 2000 characters.")]
        public string Description { get; set; }
        // Location
        [Required]
        [MaxLength(200, ErrorMessage = "Location maximum is 200 characters.")]
        public string Location { get; set; }
        public string imgName { get; set; }
    }
}
