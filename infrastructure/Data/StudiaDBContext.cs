
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
        public DbSet<MaterialEntity> Materials { get; set; }
        public DbSet<ExamEntity> Exams { get; set; }
        public DbSet<ResumeEntity> Resumes { get; set; }
        public DbSet<FlashcardEntity> Flashcards { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }

        public DbSet<ReminderEntity> Reminders { get; set; }
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

            modelBuilder.Entity<ResumeEntity>().ToTable("resume").HasKey(r => r.id_resume);
            modelBuilder.Entity<ResumeEntity>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.id_user_id);

            modelBuilder.Entity<FlashcardEntity>().ToTable("flashcards").HasKey(r => r.id_flashcard);
            modelBuilder.Entity <FlashcardEntity>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.id_user_id);

            modelBuilder.Entity<MaterialEntity>().ToTable("material").HasKey(m => m.id_material);

            modelBuilder.Entity<MaterialEntity>().HasOne(m => m.User).WithMany().HasForeignKey(m => m.id_user_id);
            modelBuilder.Entity<MaterialEntity>().HasOne(m => m.Exam).WithMany().HasForeignKey(m => m.id_exam_id).IsRequired(false);
            modelBuilder.Entity<MaterialEntity>().HasOne(m => m.Resume).WithMany().HasForeignKey(m => m.id_resume_id).IsRequired(false);
            modelBuilder.Entity<MaterialEntity>().HasOne(m => m.Flashcard).WithMany().HasForeignKey(m => m.id_flashcard_id).IsRequired(false);

            modelBuilder.Entity<ExamEntity>().ToTable("exams").HasKey(e => e.id_exam);

            modelBuilder.Entity<ExamEntity>().HasOne(e => e.User).WithMany().HasForeignKey(e => e.id_user_id);
            modelBuilder.Entity<ExamEntity>().HasOne(e => e.Type).WithMany().HasForeignKey(e => e.id_type_id);
            modelBuilder.Entity<ExamEntity>().Property(e => e.difficulty).HasConversion<int>();

            modelBuilder.Entity<TypeEntity>().ToTable("types").HasKey(t => t.id_type);

            modelBuilder.Entity<RatingEntity>().ToTable("ratings").HasKey(r => r.id_rating);

            modelBuilder.Entity<RatingEntity>().HasOne(r => r.User).WithMany().HasForeignKey(r => r.id_user_id);
            modelBuilder.Entity<RatingEntity>().HasOne(r => r.Exam).WithMany().HasForeignKey(r => r.id_exam_id).IsRequired(false);
            modelBuilder.Entity<RatingEntity>().HasOne(r => r.Flashcard).WithMany().HasForeignKey(r => r.id_flashcard_id).IsRequired(false);
            modelBuilder.Entity<RatingEntity>().HasOne(r => r.Resume).WithMany().HasForeignKey(r => r.id_resume_id).IsRequired(false);
            modelBuilder.Entity<RatingEntity>().HasOne(r => r.Note).WithMany().HasForeignKey(r => r.id_notes_id).IsRequired(false);

            modelBuilder.Entity<ReminderEntity>().ToTable("reminders").HasKey(r => r.id_reminder); 
            modelBuilder.Entity<ReminderEntity>().HasOne(r => r.User).WithMany().HasForeignKey(r => r.id_user_id); modelBuilder.Entity<ReminderEntity>().HasOne(r => r.Exam).WithMany().HasForeignKey(r => r.id_exam_id).IsRequired
                (false);
            modelBuilder.Entity<ReminderEntity>().HasOne(r => r.Flashcard).WithMany().HasForeignKey(r => r.id_flashcard_id).IsRequired(false); 
            modelBuilder.Entity<ReminderEntity>().HasOne(r => r.Resume).WithMany().HasForeignKey(r => r.id_resume_id).IsRequired(false);

        }
    }
}
