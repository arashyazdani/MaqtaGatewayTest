using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dtos
{
    [DisplayName("Add Product")]
    public class AddProductDto
    {
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product name is required")]
        [RegularExpression(@"^[\u0061-\u007a\u0041-\u005a\u0030-\u0039\u0600-\u06ff\u0750-\u077f\ufb50-\ufdff\u08a0\u2014\u08ff\ufe70\u002d\ufefc\u200b\u200c\s]{3,20}$", ErrorMessage = "Invalid product name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [RegularExpression(@"^[\u0061-\u007a\u0041-\u005a\u0030-\u0039\u0600-\u06ff\u0750-\u077f\ufb50-\ufdff\u08a0\u2014\u08ff\ufe70\u002d\ufefc\u200b\u200c\s]{0,100}$", ErrorMessage = "Invalid description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "PictureUrl is required")]
        public string PictureUrl { get; set; } = "I add it because of short time";

        [Required(ErrorMessage = "ProductTypeId is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid ProductTypeId")]
        public int ProductTypeId { get; set; }

        [Required(ErrorMessage = "ProductBrandId is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid ProductBrandId")]
        public int ProductBrandId { get; set; }
    }
}
