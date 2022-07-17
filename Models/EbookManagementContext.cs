using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FEBook.Models
{
    public partial class EbookManagementContext : DbContext
    {
        public EbookManagementContext()
        {
        }

        public EbookManagementContext(DbContextOptions<EbookManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookDowndoad> BookDowndoads { get; set; }
        public virtual DbSet<BookRating> BookRatings { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<ManageBook> ManageBooks { get; set; }
        public virtual DbSet<ReadingHistory> ReadingHistories { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123456;database=EbookManagement");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Account__1788CCAC6DABE52C");

                entity.ToTable("Account");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MajorId).HasColumnName("majorID");

                entity.Property(e => e.Passwords)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK__Account__majorID__403A8C7D");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.AuthorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Profiles)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.BookCover)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Languages)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.Summary)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("date");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Book__SubjectID__440B1D61");
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasKey(e => new { e.AuthorId, e.BookId })
                    .HasName("PK__BookAuth__1304F03695804D8F");

                entity.ToTable("BookAuthor");

                entity.Property(e => e.AuthorId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AuthorID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.AuthorUpdateDate).HasColumnType("date");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookAutho__Autho__5629CD9C");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookAutho__BookI__571DF1D5");
            });

            modelBuilder.Entity<BookDowndoad>(entity =>
            {
                entity.HasKey(e => e.DownloadId)
                    .HasName("PK__BookDown__73D5A7107CEC8CF0");

                entity.ToTable("BookDowndoad");

                entity.Property(e => e.DownloadId).HasColumnName("DownloadID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookDowndoads)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookDownd__BookI__52593CB8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookDowndoads)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__BookDownd__UserI__5165187F");
            });

            modelBuilder.Entity<BookRating>(entity =>
            {
                entity.HasKey(e => e.RateId)
                    .HasName("PK__BookRati__5705EA344419873C");

                entity.ToTable("BookRating");

                entity.Property(e => e.RateId).HasColumnName("rateID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.CmtContent)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DateRating).HasColumnType("date");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookRatings)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookRatin__BookI__48CFD27E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BookRatings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__BookRatin__UserI__47DBAE45");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("Major");

                entity.Property(e => e.MajorId).HasColumnName("majorID");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.MajorName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ManageBook>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BookId, e.UpdateDate })
                    .HasName("PK__ManageBo__2FA2A21FAA246875");

                entity.ToTable("ManageBook");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.UpdateDate).HasColumnType("date");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ManageBooks)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ManageBoo__BookI__5BE2A6F2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ManageBooks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ManageBoo__UserI__5AEE82B9");
            });

            modelBuilder.Entity<ReadingHistory>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BookId, e.DateRead })
                    .HasName("PK__ReadingH__96A4C38050D96ACB");

                entity.ToTable("ReadingHistory");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UserID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.DateRead).HasColumnType("datetime");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ReadingHistories)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReadingHi__BookI__4D94879B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReadingHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReadingHi__UserI__4CA06362");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.DeleteStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.MajorId).HasColumnName("majorID");

                entity.Property(e => e.Subname)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK__Subjects__majorI__3C69FB99");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
