using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Base
{
    public class Attributes:BaseEntity
    {       
        public string Name { get; set; }
        [ForeignKey(nameof(AttributeType))]
        public int AttributeTypeId { get; set; }
        [ForeignKey(nameof(Books))]
        public int? BookID { get; set; }
        public string Value { get; set; }
        public virtual AttributeType AttributeType { get; set; }
        public virtual Book Books { get; set; }
    }
}
