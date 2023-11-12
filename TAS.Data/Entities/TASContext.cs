﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using TAS.Data.Entities.Interfaces;

namespace TAS.Data.Entities
{
    public partial class TASContext : DbContext
    {
        public TASContext()
        {
        }

        public TASContext(DbContextOptions<TASContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; } = null!;
        public virtual DbSet<QuestionResult> QuestionResults { get; set; } = null!;
        public virtual DbSet<QuestionResultAnswer> QuestionResultAnswers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<TestResult> TestResults { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=(local);uid=sa;pwd=123;database=TAS;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasColumnName("avatar");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("createUser");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Otp)
                    .HasMaxLength(255)
                    .HasColumnName("otp");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Accounts)
                    .UsingEntity<Dictionary<string, object>>(
                        "AccountRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Account_r__role___123EB7A3"),
                        r => r.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Account_r__accou__114A936A"),
                        j =>
                        {
                            j.HasKey("AccountId", "RoleId").HasName("PK__Account___91C2B49190D12C17");

                            j.ToTable("Account_role");

                            j.IndexerProperty<int>("AccountId").HasColumnName("account_id");

                            j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        });
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.AddressId).HasColumnName("Address_id");

                entity.Property(e => e.AddressName)
                    .HasMaxLength(255)
                    .HasColumnName("Address_name");

                entity.Property(e => e.WardsId).HasColumnName("Wards_id");

                entity.HasOne(d => d.Wards)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.WardsId)
                    .HasConstraintName("FK__Address__Address__07C12930");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CitiesId)
                    .HasName("PK__Cities__966E69631E09BC73");

                entity.Property(e => e.CitiesId).HasColumnName("Cities_id");

                entity.Property(e => e.CitiesName)
                    .HasMaxLength(255)
                    .HasColumnName("Cities_name");

                entity.Property(e => e.ProvincesId).HasColumnName("Provinces_id");

                entity.HasOne(d => d.Provinces)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.ProvincesId)
                    .HasConstraintName("FK__Cities__Province__7F2BE32F");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassId).HasColumnName("class_id");

                entity.Property(e => e.ClassCode)
                    .HasMaxLength(255)
                    .HasColumnName("class_code");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(255)
                    .HasColumnName("class_name");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("createUser");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Subject)
                    .HasMaxLength(255)
                    .HasColumnName("subject");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.HasMany(d => d.Accounts)
                    .WithMany(p => p.Classes)
                    .UsingEntity<Dictionary<string, object>>(
                        "ManageClass",
                        l => l.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Manage_cl__accou__17F790F9"),
                        r => r.HasOne<Class>().WithMany().HasForeignKey("ClassId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Manage_cl__class__17036CC0"),
                        j =>
                        {
                            j.HasKey("ClassId", "AccountId").HasName("PK__Manage_c__399E5BAAC719ACA0");

                            j.ToTable("Manage_class");

                            j.IndexerProperty<int>("ClassId").HasColumnName("class_id");

                            j.IndexerProperty<int>("AccountId").HasColumnName("account_id");
                        });

                entity.HasMany(d => d.AccountsNavigation)
                    .WithMany(p => p.ClassesNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "StudentEnrollClass",
                        l => l.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Student_E__accou__1BC821DD"),
                        r => r.HasOne<Class>().WithMany().HasForeignKey("ClassId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Student_E__class__1AD3FDA4"),
                        j =>
                        {
                            j.HasKey("ClassId", "AccountId").HasName("PK__Student___399E5BAA37E0A3BE");

                            j.ToTable("Student_Enroll_class");

                            j.IndexerProperty<int>("ClassId").HasColumnName("class_id");

                            j.IndexerProperty<int>("AccountId").HasColumnName("account_id");
                        });
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId).HasColumnName("Country_id");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .HasColumnName("Country_name");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CourseDescription)
                    .HasColumnType("text")
                    .HasColumnName("course_description");

                entity.Property(e => e.CourseLevel).HasColumnName("course_level");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(255)
                    .HasColumnName("course_name");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("createUser");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.HasMany(d => d.Accounts)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseEnroll",
                        l => l.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_en__accou__2739D489"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_en__cours__2645B050"),
                        j =>
                        {
                            j.HasKey("CourseId", "AccountId").HasName("PK__Course_e__4B74D582B4384D19");

                            j.ToTable("Course_enroll");

                            j.IndexerProperty<int>("CourseId").HasColumnName("course_id");

                            j.IndexerProperty<int>("AccountId").HasColumnName("account_id");
                        });
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.DistrictsId)
                    .HasName("PK__District__A05B765FB7B72975");

                entity.Property(e => e.DistrictsId).HasColumnName("Districts_id");

                entity.Property(e => e.CitiesId).HasColumnName("Cities_id");

                entity.Property(e => e.DistrictsName)
                    .HasMaxLength(255)
                    .HasColumnName("Districts_name");

                entity.HasOne(d => d.Cities)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CitiesId)
                    .HasConstraintName("FK__Districts__Distr__02084FDA");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ProvincesId)
                    .HasName("PK__Province__9FBCE102626B37BE");

                entity.Property(e => e.ProvincesId).HasColumnName("Provinces_id");

                entity.Property(e => e.CountryId).HasColumnName("Country_id");

                entity.Property(e => e.ProvincesName)
                    .HasMaxLength(255)
                    .HasColumnName("Provinces_name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Provinces__Count__7C4F7684");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.QuestionId)
                    .ValueGeneratedNever()
                    .HasColumnName("question_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("createUser");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.HasOne(d => d.QuestionNavigation)
                    .WithOne(p => p.Question)
                    .HasForeignKey<Question>(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question__questi__339FAB6E");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK__Question__test_i__3493CFA7");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Question__2EC2154986781AD5");

                entity.ToTable("Question_answer");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.CorrectResult)
                    .HasMaxLength(255)
                    .HasColumnName("correct_result");

                entity.Property(e => e.ResultA)
                    .HasMaxLength(255)
                    .HasColumnName("resultA");

                entity.Property(e => e.ResultB)
                    .HasMaxLength(255)
                    .HasColumnName("resultB");

                entity.Property(e => e.ResultC)
                    .HasMaxLength(255)
                    .HasColumnName("resultC");

                entity.Property(e => e.ResultD)
                    .HasMaxLength(255)
                    .HasColumnName("resultD");
            });

            modelBuilder.Entity<QuestionResult>(entity =>
            {
                entity.ToTable("Question_result");

                entity.Property(e => e.QuestionResultId)
                    .ValueGeneratedNever()
                    .HasColumnName("question_result_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.TestResultId).HasColumnName("test_result_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.HasOne(d => d.QuestionResultNavigation)
                    .WithOne(p => p.QuestionResult)
                    .HasForeignKey<QuestionResult>(d => d.QuestionResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question___quest__395884C4");

                entity.HasOne(d => d.TestResult)
                    .WithMany(p => p.QuestionResults)
                    .HasForeignKey(d => d.TestResultId)
                    .HasConstraintName("FK__Question___test___3A4CA8FD");
            });

            modelBuilder.Entity<QuestionResultAnswer>(entity =>
            {
                entity.HasKey(e => e.QuestionResultId)
                    .HasName("PK__Question__1426799644859033");

                entity.ToTable("Question_result_answer");

                entity.Property(e => e.QuestionResultId)
                    .ValueGeneratedNever()
                    .HasColumnName("question_result_id");

                entity.Property(e => e.CorrectResult)
                    .HasMaxLength(255)
                    .HasColumnName("correct_result");

                entity.Property(e => e.ResultA)
                    .HasMaxLength(255)
                    .HasColumnName("resultA");

                entity.Property(e => e.ResultB)
                    .HasMaxLength(255)
                    .HasColumnName("resultB");

                entity.Property(e => e.ResultC)
                    .HasMaxLength(255)
                    .HasColumnName("resultC");

                entity.Property(e => e.ResultD)
                    .HasMaxLength(255)
                    .HasColumnName("resultD");

                entity.Property(e => e.Seleted)
                    .HasMaxLength(255)
                    .HasColumnName("seleted");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(25)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("createUser");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.TestDescription)
                    .HasColumnType("text")
                    .HasColumnName("test_description");

                entity.Property(e => e.TestDuration).HasColumnName("test_duration");

                entity.Property(e => e.TestName)
                    .HasMaxLength(255)
                    .HasColumnName("test_name");

                entity.Property(e => e.TestTotalScore).HasColumnName("test_total_score");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.HasMany(d => d.Classes)
                    .WithMany(p => p.Tests)
                    .UsingEntity<Dictionary<string, object>>(
                        "ClassTest",
                        l => l.HasOne<Class>().WithMany().HasForeignKey("ClassId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Class_tes__class__208CD6FA"),
                        r => r.HasOne<Test>().WithMany().HasForeignKey("TestId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Class_tes__test___2180FB33"),
                        j =>
                        {
                            j.HasKey("TestId", "ClassId").HasName("PK__Class_te__8C205B9A92C0676F");

                            j.ToTable("Class_test");

                            j.IndexerProperty<int>("TestId").HasColumnName("test_id");

                            j.IndexerProperty<int>("ClassId").HasColumnName("class_id");
                        });

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Tests)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseTest",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_te__cours__2A164134"),
                        r => r.HasOne<Test>().WithMany().HasForeignKey("TestId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_te__test___2B0A656D"),
                        j =>
                        {
                            j.HasKey("TestId", "CourseId").HasName("PK__Course_t__0B0EF3787BAB5596");

                            j.ToTable("Course_test");

                            j.IndexerProperty<int>("TestId").HasColumnName("test_id");

                            j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                        });
            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.ToTable("Test_result");

                entity.Property(e => e.TestResultId).HasColumnName("test_result_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.TestFinish).HasColumnName("test_finish");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.TestScore).HasColumnName("test_score");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Test_resu__accou__2EDAF651");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK__Test_resu__test___2DE6D218");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("Token");

                entity.Property(e => e.TokenId).HasColumnName("token_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Expired)
                    .HasColumnType("datetime")
                    .HasColumnName("expired");

                entity.Property(e => e.Revoked).HasColumnName("revoked");

                entity.Property(e => e.Token1)
                    .HasMaxLength(255)
                    .HasColumnName("token");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Token__expired__0C85DE4D");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.WardsId)
                    .HasName("PK__Wards__E69734F94C648B44");

                entity.Property(e => e.WardsId).HasColumnName("Wards_id");

                entity.Property(e => e.DistrictsId).HasColumnName("Districts_id");

                entity.Property(e => e.WardsName)
                    .HasMaxLength(255)
                    .HasColumnName("Wards_name");

                entity.HasOne(d => d.Districts)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictsId)
                    .HasConstraintName("FK__Wards__Districts__04E4BC85");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}