using Dal.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Dal
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDBContext Create()
        {
            return new ApplicationDBContext();
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Country> Countires { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
