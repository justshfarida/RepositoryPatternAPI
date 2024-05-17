using Microsoft.EntityFrameworkCore;
using RepositoryPatternApi.Domain.Entities;
using RepositoryPatternAPI.Data.Entities;
using System.Reflection.Emit;

namespace RepositoryPatternAPI.Data
{
    public class ApplicationDBContext:DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>().HasKey(sc => new { sc.ISBN, sc.Id });
            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.Id);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.ISBN);
            }
        ////protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ////{
        ////    optionsBuilder
        ////        .UseLazyLoadingProxies()
        ////        .UseSqlServer("DefaultConnection"); // Ensure you have the correct connection string
        ////}
    }
}
