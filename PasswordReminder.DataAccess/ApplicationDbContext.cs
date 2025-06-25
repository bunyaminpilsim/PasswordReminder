using Microsoft.EntityFrameworkCore;
using PasswordReminder.Entities;

namespace PasswordReminder.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PasswordRecord> PasswordRecords { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Email unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Relations
            modelBuilder.Entity<PasswordRecord>()
                .HasOne(p => p.User)
                .WithMany(u => u.Passwords)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<PasswordRecord>()
                .HasOne(p => p.Category)
                .WithMany(c => c.PasswordRecords)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
