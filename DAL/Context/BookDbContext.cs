using Dal.Entities;
using DAL.Entities.Base;
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
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<AttributeType> AttributeTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AttributeType>()
                .HasMany<Book>(s => s.Books)
                .WithMany(c => c.AttributeTypes)
                .Map(cs =>
                {
                    cs.MapLeftKey("BookID");
                    cs.MapRightKey("AttributeTypeId");
                    cs.ToTable("Attributes");
                });
        }
    }
}
