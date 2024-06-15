using Microsoft.EntityFrameworkCore;
using RepositoryPatternApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RepositoryPatternAPI.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace RepositoryPatternAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorBook>().HasKey(sc => new { sc.ISBN, sc.Id });

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.Id);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.ISBN);

            // Configure Identity entity properties
            //modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(iul => new { iul.LoginProvider, iul.ProviderKey });
            //modelBuilder.Entity<IdentityUserRole<string>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            //modelBuilder.Entity<IdentityUserToken<string>>().HasKey(iut => new { iut.UserId, iut.LoginProvider, iut.Name });
        }
    }
}
