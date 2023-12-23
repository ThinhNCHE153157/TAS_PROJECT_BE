using System;
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
        public virtual DbSet<Enterprise> Enterprises { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Part> Parts { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; } = null!;
        public virtual DbSet<QuestionResult> QuestionResults { get; set; } = null!;
        public virtual DbSet<QuestionResultAnswer> QuestionResultAnswers { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<TestResult> TestResults { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;
        public virtual DbSet<VnPayHistory> VnPayHistories { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=103.179.173.136;uid=sa;pwd=@TAS12345;database=TAS;Encrypt=False");
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

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsVerified).HasColumnName("is_verified");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Otp)
                    .HasMaxLength(255)
                    .HasColumnName("otp");

                entity.Property(e => e.Otpcreatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("otpcreatetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
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
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Accounts)
                    .UsingEntity<Dictionary<string, object>>(
                        "AccountRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Account_r__role___2180FB33"),
                        r => r.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Account_r__accou__208CD6FA"),
                        j =>
                        {
                            j.HasKey("AccountId", "RoleId").HasName("PK__Account___91C2B49123B16C8F");

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
                    .HasConstraintName("FK__Address__Address__1332DBDC");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CitiesId)
                    .HasName("PK__Cities__966E6963C8B4F1F5");

                entity.Property(e => e.CitiesId).HasColumnName("Cities_id");

                entity.Property(e => e.CitiesName)
                    .HasMaxLength(255)
                    .HasColumnName("Cities_name");

                entity.Property(e => e.ProvincesId).HasColumnName("Provinces_id");

                entity.HasOne(d => d.Provinces)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.ProvincesId)
                    .HasConstraintName("FK__Cities__Province__0A9D95DB");
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

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxStudent).HasColumnName("max_student");

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
                        l => l.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Manage_cl__accou__282DF8C2"),
                        r => r.HasOne<Class>().WithMany().HasForeignKey("ClassId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Manage_cl__class__2739D489"),
                        j =>
                        {
                            j.HasKey("ClassId", "AccountId").HasName("PK__Manage_c__399E5BAA6EF3E19E");

                            j.ToTable("Manage_class");

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

                entity.Property(e => e.CourseCost).HasColumnName("course_cost");

                entity.Property(e => e.CourseDescription)
                    .HasMaxLength(4000)
                    .HasColumnName("course_description");

                entity.Property(e => e.CourseGoal)
                    .HasMaxLength(4000)
                    .HasColumnName("course_goal");

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

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(200)
                    .HasColumnName("short_description");

                entity.Property(e => e.Status).HasColumnName("status");

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
                        l => l.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_en__accou__2EDAF651"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_en__cours__2DE6D218"),
                        j =>
                        {
                            j.HasKey("CourseId", "AccountId").HasName("PK__Course_e__4B74D582FC394AAF");

                            j.ToTable("Course_enroll");

                            j.IndexerProperty<int>("CourseId").HasColumnName("course_id");

                            j.IndexerProperty<int>("AccountId").HasColumnName("account_id");
                        });
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.DistrictsId)
                    .HasName("PK__District__A05B765FA90103D9");

                entity.Property(e => e.DistrictsId).HasColumnName("Districts_id");

                entity.Property(e => e.CitiesId).HasColumnName("Cities_id");

                entity.Property(e => e.DistrictsName)
                    .HasMaxLength(255)
                    .HasColumnName("Districts_name");

                entity.HasOne(d => d.Cities)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CitiesId)
                    .HasConstraintName("FK__Districts__Distr__0D7A0286");
            });

            modelBuilder.Entity<Enterprise>(entity =>
            {
                entity.HasKey(e => e.EnterpriseCode)
                    .HasName("PK__Enterpri__84AA8D1A15BD686E");

                entity.ToTable("Enterprise");

                entity.Property(e => e.EnterpriseCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("enterprise_code");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.EnterpriseName)
                    .HasMaxLength(255)
                    .HasColumnName("enterprise_name");

                entity.Property(e => e.ForeignName)
                    .HasMaxLength(255)
                    .HasColumnName("foreign_name");

                entity.Property(e => e.OfficeAddress)
                    .HasMaxLength(255)
                    .HasColumnName("office_address");

                entity.Property(e => e.RepresentativeName)
                    .HasMaxLength(255)
                    .HasColumnName("representative_name");

                entity.Property(e => e.ShortName)
                    .HasMaxLength(255)
                    .HasColumnName("short_name");
                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Enterprises)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Enterpris__accou__18EBB532");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Order__totalAmou__5F7E2DAC");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.ToTable("Part");

                entity.Property(e => e.PartId).HasColumnName("part_id");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.Type).HasDefaultValueSql("((0))");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK__Part__test_id__3D2915A8");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ProvincesId)
                    .HasName("PK__Province__9FBCE10252871D92");

                entity.Property(e => e.ProvincesId).HasColumnName("Provinces_id");

                entity.Property(e => e.CountryId).HasColumnName("Country_id");

                entity.Property(e => e.ProvincesName)
                    .HasMaxLength(255)
                    .HasColumnName("Provinces_name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Provinces__Count__07C12930");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

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

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note");

                entity.Property(e => e.PartId).HasColumnName("part_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question__part_i__489AC854");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.ToTable("Question_answer");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("question_answer_id");

                entity.Property(e => e.CorrectResult)
                    .HasMaxLength(255)
                    .HasColumnName("correct_result");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

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

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Question___corre__4B7734FF");
            });

            modelBuilder.Entity<QuestionResult>(entity =>
            {
                entity.ToTable("Question_result");

                entity.Property(e => e.QuestionResultId).HasColumnName("question_result_id");

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

                entity.HasOne(d => d.TestResult)
                    .WithMany(p => p.QuestionResults)
                    .HasForeignKey(d => d.TestResultId)
                    .HasConstraintName("FK__Question___test___4E53A1AA");
            });

            modelBuilder.Entity<QuestionResultAnswer>(entity =>
            {
                entity.ToTable("Question_result_answer");

                entity.Property(e => e.QuestionResultAnswerId).HasColumnName("question_result_answer_id");

                entity.Property(e => e.CorrectResult)
                    .HasMaxLength(255)
                    .HasColumnName("correct_result");

                entity.Property(e => e.QuestionResultId).HasColumnName("question_result_id");

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

                entity.HasOne(d => d.QuestionResult)
                    .WithMany(p => p.QuestionResultAnswers)
                    .HasForeignKey(d => d.QuestionResultId)
                    .HasConstraintName("FK__Question___selet__51300E55");
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

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TestDescription)
                    .HasColumnType("text")
                    .HasColumnName("test_description");

                entity.Property(e => e.TestDuration).HasColumnName("test_duration");

                entity.Property(e => e.TestName)
                    .HasMaxLength(255)
                    .HasColumnName("test_name");

                entity.Property(e => e.TestTotalScore).HasColumnName("test_total_score");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__Test__topic_id__3A4CA8FD");

                entity.HasMany(d => d.Classes)
                    .WithMany(p => p.Tests)
                    .UsingEntity<Dictionary<string, object>>(
                        "ClassTest",
                        l => l.HasOne<Class>().WithMany().HasForeignKey("ClassId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Class_tes__class__40058253"),
                        r => r.HasOne<Test>().WithMany().HasForeignKey("TestId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Class_tes__test___40F9A68C"),
                        j =>
                        {
                            j.HasKey("TestId", "ClassId").HasName("PK__Class_te__8C205B9A35DAB052");

                            j.ToTable("Class_test");

                            j.IndexerProperty<int>("TestId").HasColumnName("test_id");

                            j.IndexerProperty<int>("ClassId").HasColumnName("class_id");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test_resu__accou__44CA3770");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test_resu__test___43D61337");
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

                entity.Property(e => e.TokenType)
                    .HasMaxLength(255)
                    .HasColumnName("token_type");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Token__expired__1BC821DD");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("createUser");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TopicDescription)
                    .HasMaxLength(255)
                    .HasColumnName("topic_description");

                entity.Property(e => e.TopicName)
                    .HasMaxLength(255)
                    .HasColumnName("topic_name");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Topic__course_id__32AB8735");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("video");

                entity.Property(e => e.VideoId).HasColumnName("video_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("createUser");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TopicId).HasColumnName("topic_id");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");

                entity.Property(e => e.VideoTitle)
                    .HasMaxLength(255)
                    .HasColumnName("video_title");

                entity.Property(e => e.VideoUrl)
                    .HasMaxLength(255)
                    .HasColumnName("video_url");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__video__topic_id__367C1819");
            });

            modelBuilder.Entity<VnPayHistory>(entity =>
            {
                entity.ToTable("VnPayHistory");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.VnPayHistories)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__VnPayHist__order__625A9A57");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.WardsId)
                    .HasName("PK__Wards__E69734F995044072");

                entity.Property(e => e.WardsId).HasColumnName("Wards_id");

                entity.Property(e => e.DistrictsId).HasColumnName("Districts_id");

                entity.Property(e => e.WardsName)
                    .HasMaxLength(255)
                    .HasColumnName("Wards_name");

                entity.HasOne(d => d.Districts)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictsId)
                    .HasConstraintName("FK__Wards__Districts__10566F31");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}