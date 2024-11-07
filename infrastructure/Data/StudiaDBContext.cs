
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class StudiaDBContext : DbContext
    {
        public StudiaDBContext(DbContextOptions<StudiaDBContext> options) : base(options) 
        {
        }
        public DbSet<UserEntity>Users{get; set; }
        public DbSet<FolderEntity>Folders{get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("users").HasKey(u => u.id_user);
            modelBuilder.Entity<FolderEntity>().ToTable("folders").HasKey(f => f.id_folder);
            modelBuilder.Entity<FolderEntity>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.id_user_id);
        }
    }
}
