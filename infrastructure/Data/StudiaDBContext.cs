
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
        public DbSet<NoteEntity>Notes{get; set; }
        public DbSet<ResponseEntity>Responses{get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("users").HasKey(u => u.id_user);

            modelBuilder.Entity<FolderEntity>().ToTable("folders").HasKey(f => f.id_folder);
            modelBuilder.Entity<FolderEntity>()
               .HasOne(f => f.User)
               .WithMany()
               .HasForeignKey(f => f.id_user_id);

            modelBuilder.Entity<NoteEntity>().ToTable("notes").HasKey(n => n.id_note);
            modelBuilder.Entity<NoteEntity>()
                .HasOne(n => n.Folder)
                .WithMany(f => f.Notes)
                .HasForeignKey(n => n.id_folder_id);

            modelBuilder.Entity<ResponseEntity>().ToTable("responses").HasKey(r => r.id_response);
            modelBuilder.Entity<ResponseEntity>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.id_user_id);
        }
    }
}
