using System.ComponentModel.DataAnnotations;

namespace Dal.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

    }
}
