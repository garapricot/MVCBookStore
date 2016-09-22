using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
   public class Country:BaseEntity
    { 
        public int CountryCode { get; set; }
        public string Name { get; set; }
    }   
}
