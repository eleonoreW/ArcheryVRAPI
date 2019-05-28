using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Archery.Models
{
    public partial class ArcheryVRContext : DbContext
    {
        public ArcheryVRContext()
        {
        }

        public ArcheryVRContext(DbContextOptions<ArcheryVRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<Profil> Profil { get; set; }
        public virtual DbSet<Progression> Progression { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Resultat> Resultat { get; set; }
        public virtual DbSet<Sujet> Sujet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:archeryvr2019dbserver.database.windows.net,1433;Initial Catalog=ArcheryVR;Persist Security Info=False;User ID=sqladmin;Password=Azerty123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasIndex(e => e.Nom)
                    .HasName("UQ__Grade__C7D1C61EC8F8C467")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Profil>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Couleur)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Progression>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.ProfilId).HasColumnName("ProfilID");

                entity.Property(e => e.Xpanglais).HasColumnName("XPAnglais");

                entity.Property(e => e.Xpfrancais).HasColumnName("XPFrancais");

                entity.Property(e => e.Xpmaths).HasColumnName("XPMaths");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Progression)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Progression_FK_Grade");

                entity.HasOne(d => d.Profil)
                    .WithMany(p => p.Progression)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Progression_FK_Profil");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BadAnswers)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Explanation)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.GoodAnswers)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.QuestionText)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.SujetId).HasColumnName("SujetID");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Question_FK_Grade");

                entity.HasOne(d => d.Sujet)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.SujetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Question_FK_Sujet");
            });

            modelBuilder.Entity<Resultat>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateResultat).HasColumnType("date");

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.ProfilId).HasColumnName("ProfilID");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Resultat)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Resultat_FK_Grade");

                entity.HasOne(d => d.Profil)
                    .WithMany(p => p.Resultat)
                    .HasForeignKey(d => d.ProfilId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Resultat_FK_Profil");
            });

            modelBuilder.Entity<Sujet>(entity =>
            {
                entity.HasIndex(e => e.Nom)
                    .HasName("UQ__Sujet__C7D1C61E1917427C")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nom)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
