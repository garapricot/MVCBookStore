using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class GenreBook
    {
        [Key, ForeignKey(nameof(Genre)), Column(Order = 0)]
        public int GenreId { get; set; }
        [Key, ForeignKey(nameof(Book)), Column(Order = 1)]
        public int BookId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Book Book { get; set; }
    }
}
