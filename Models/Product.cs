using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFDbFirstApproachExample.CustomValidations;

namespace EFDbFirstApproachExample.Models
{
    [Table("Products", Schema = "dbo")]
	public class Product
	{
        [Key]
        [Display(Name = "Product ID")]
        public long ProductID { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name cannot be blank or empty")]
        [RegularExpression(@"^[A-Za-z ]*$", ErrorMessage = "Product Name cannot include special characters or numbers.")]
        [MaxLength(40, ErrorMessage = "Product Name cannot exceed character limit of 40 characters.")]
        [MinLength(2, ErrorMessage = "Product Name cannot contain characters than 2.")]
        public string ProductName { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price cannot be blank or empty")]
        [Range(0,100_000, ErrorMessage = "Price must be any value between 0 and 100,000")]
        [DivisibleBy10(ErrorMessage = "Price value must be a multiple of ten")]
        public Nullable<decimal> Price { get; set; }

        [Column("DOP", TypeName="datetime")]
        [Display(Name = "Date of Purchase")]
        public Nullable<System.DateTime> DOP { get; set; }
        [Display(Name = "Availability Status")]
        [Required(ErrorMessage = "Please choose an Availability Status")]
        public string AvailabilityStatus { get; set; }
        [Display(Name = "Category ID")]
        [Required(ErrorMessage = "Category cannot be blank or empty")]
        public Nullable<long> CategoryID { get; set; }
        [Display(Name = "Brand ID")]
        [Required(ErrorMessage = "Brand cannot be blank or empty")]
        public Nullable<long> BrandID { get; set; }
        [Display(Name = "Active")]
        public Nullable<bool> Active { get; set; }
        [Display(Name = "Image")]
        public string Img { get; set; }
        [Display(Name = "Quantity")]
        public Nullable<decimal> Quantity { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
	}
}