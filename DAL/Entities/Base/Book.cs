using DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dal.Entities
{
    public class Book : BaseEntity
    {
        
        [Required]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        [Required]
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        [Required(ErrorMessage = "The  field is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The  field is required.")]
        [Range(0, 10000, ErrorMessage = "Can only be between 0+ .. 10000")]
        public decimal Price { get; set; }
        public string DelId { get; set; }
        [StringLength(1000, ErrorMessage = "Max 1000 digits")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The  field is required.")]
        [Range(10, 1000, ErrorMessage = "Can only be between 10 .. 1000")]
        public int PageCount { get; set; }
        public byte[] Image { get; set; }
        public virtual List<AttributeType> AttributeTypes { get; set; }
        public virtual Author Author { get; set; }
        public virtual Country Country { get; set; }
        
    }
}
   