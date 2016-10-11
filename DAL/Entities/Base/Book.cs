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
        [ForeignKey(nameof(Attribute))]
        public int? AttributeId { get; set; }
        [Required]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        [Required]
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }        
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }       
        public string Description { get; set; }
        public int PageCount { get; set; }
        public byte[] Image { get; set; }
        public virtual Attributes Attribute { get; set; }
        public virtual Author Author { get; set; }
        public virtual Country Country { get; set; }
        
    }
}
   