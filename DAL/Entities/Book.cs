using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Author { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public byte[] Image { get; set; }      
        public virtual Country Country { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
