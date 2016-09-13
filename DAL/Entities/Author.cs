using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
   public class Author
    {
        [Key]
        public int Id { get; set; }       
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }        
        [DisplayName("Birth Day")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDay { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string Bio { get; set; }
        public string FullName
        {
            get
            {
                return LastName + "  " + FirstName;
            }
        }
    }  

}
