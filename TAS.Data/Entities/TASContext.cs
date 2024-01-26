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
        public virtual DbSet<AccountFlashcard> AccountFlashcards { get; set; } = null!;
        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Enterprise> Enterprises { get; set; } = null!;
        public virtual DbSet<Flashcard> Flashcards { get; set; } = null!;
        public virtual DbSet<ItemCard> ItemCards { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Part> Parts { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; } = null!;
        public virtual DbSet<QuestionResult> QuestionResults { get; set; } = null!;
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

                entity.Property(e => e.Otpexpiretime)
                    .HasColumnType("datetime")
                    .HasColumnName("otpexpiretime");

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
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Account_r__role___0D99FE17"),
                        r => r.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Account_r__accou__0CA5D9DE"),
                        j =>
                        {
                            j.HasKey("AccountId", "RoleId").HasName("PK__Account___91C2B4914A9651D3");

                            j.ToTable("Account_role");

                            j.IndexerProperty<int>("AccountId").HasColumnName("account_id");

                            j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        });
            });

            modelBuilder.Entity<AccountFlashcard>(entity =>
            {
                entity.HasKey(e => new { e.FlashcardId, e.AccountId })
                    .HasName("PK__Account___821F53FE375F71C4");

                entity.ToTable("Account_Flashcard");

                entity.Property(e => e.FlashcardId).HasColumnName("flashcard_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.IsOwn).HasColumnName("isOWn");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountFlashcards)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account_F__accou__38845C1C");

                entity.HasOne(d => d.Flashcard)
                    .WithMany(p => p.AccountFlashcards)
                    .HasForeignKey(d => d.FlashcardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account_F__flash__39788055");
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
                    .HasConstraintName("FK__Address__Address__7993056A");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CitiesId)
                    .HasName("PK__Cities__966E696300404875");

                entity.Property(e => e.CitiesId).HasColumnName("Cities_id");

                entity.Property(e => e.CitiesName)
                    .HasMaxLength(255)
                    .HasColumnName("Cities_name");

                entity.Property(e => e.ProvincesId).HasColumnName("Provinces_id");

                entity.HasOne(d => d.Provinces)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.ProvincesId)
                    .HasConstraintName("FK__Cities__Province__70FDBF69");
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
                        l => l.HasOne<Account>().WithMany().HasForeignKey("AccountId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_en__accou__1446FBA6"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Course_en__cours__1352D76D"),
                        j =>
                        {
                            j.HasKey("CourseId", "AccountId").HasName("PK__Course_e__4B74D582DF2220A9");

                            j.ToTable("Course_enroll");

                            j.IndexerProperty<int>("CourseId").HasColumnName("course_id");

                            j.IndexerProperty<int>("AccountId").HasColumnName("account_id");
                        });
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.DistrictsId)
                    .HasName("PK__District__A05B765F894D131D");

                entity.Property(e => e.DistrictsId).HasColumnName("Districts_id");

                entity.Property(e => e.CitiesId).HasColumnName("Cities_id");

                entity.Property(e => e.DistrictsName)
                    .HasMaxLength(255)
                    .HasColumnName("Districts_name");

                entity.HasOne(d => d.Cities)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CitiesId)
                    .HasConstraintName("FK__Districts__Distr__73DA2C14");
            });

            modelBuilder.Entity<Enterprise>(entity =>
            {
                entity.HasKey(e => e.EnterpriseCode)
                    .HasName("PK__Enterpri__84AA8D1A8175B37B");

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
                    .HasConstraintName("FK__Enterpris__accou__0504B816");
            });

            modelBuilder.Entity<Flashcard>(entity =>
            {
                entity.ToTable("Flashcard");

                entity.Property(e => e.FlashcardId).HasColumnName("flashcard_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createDate");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(255)
                    .HasColumnName("createUser");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.FlashcardName)
                    .HasMaxLength(255)
                    .HasColumnName("flashcard_name");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("updateDate");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(255)
                    .HasColumnName("updateUser");
            });

            modelBuilder.Entity<ItemCard>(entity =>
            {
                entity.ToTable("ItemCard");

                entity.Property(e => e.Defination)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("defination");

                entity.Property(e => e.Example)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("example");

                entity.Property(e => e.FlashcardId).HasColumnName("flashcard_id");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.NewWord)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("newWord");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.Spelling)
                    .HasMaxLength(255)
                    .HasColumnName("spelling");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.HasOne(d => d.Flashcard)
                    .WithMany(p => p.ItemCards)
                    .HasForeignKey(d => d.FlashcardId)
                    .HasConstraintName("FK__ItemCard__flashc__3C54ED00");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .HasColumnName("order_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.TotalAmount).HasColumnName("totalAmount");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Order__account_i__7F4BDEC0");
            });

            modelBuilder.Entity<Part>(entity =>
            {
                entity.ToTable("Part");

                entity.Property(e => e.PartId).HasColumnName("part_id");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Parts)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK__Part__test_id__22951AFD");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.ProvincesId)
                    .HasName("PK__Province__9FBCE1028B44FC47");

                entity.Property(e => e.ProvincesId).HasColumnName("Provinces_id");

                entity.Property(e => e.CountryId).HasColumnName("Country_id");

                entity.Property(e => e.ProvincesName)
                    .HasMaxLength(255)
                    .HasColumnName("Provinces_name");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__Provinces__Count__6E2152BE");
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
                    .HasConstraintName("FK__Question__part_i__2A363CC5");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.ToTable("Question_answer");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("question_answer_id");

                entity.Property(e => e.Answer)
                    .HasMaxLength(255)
                    .HasColumnName("answer");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(1000)
                    .HasColumnName("explanation");

                entity.Property(e => e.Iscorrect).HasColumnName("iscorrect");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK__Question___quest__2D12A970");
            });

            modelBuilder.Entity<QuestionResult>(entity =>
            {
                entity.ToTable("Question_result");

                entity.Property(e => e.QuestionResultId).HasColumnName("question_result_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("question_answer_id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.TestResultId).HasColumnName("test_result_id");

                entity.HasOne(d => d.TestResult)
                    .WithMany(p => p.QuestionResults)
                    .HasForeignKey(d => d.TestResultId)
                    .HasConstraintName("FK__Question___test___2FEF161B");
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
                    .HasConstraintName("FK__Test__topic_id__1FB8AE52");
            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.ToTable("Test_result");

                entity.Property(e => e.TestResultId).HasColumnName("test_result_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.IsPass).HasColumnName("isPass");

                entity.Property(e => e.TestFinish).HasColumnName("test_finish");

                entity.Property(e => e.TestId).HasColumnName("test_id");

                entity.Property(e => e.TestNumberCorrect).HasColumnName("test_numberCorrect");

                entity.Property(e => e.TestScore).HasColumnName("test_score");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test_resu__accou__2665ABE1");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestResults)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test_resu__test___257187A8");
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
                    .HasConstraintName("FK__Token__expired__07E124C1");
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
                    .HasConstraintName("FK__Topic__course_id__18178C8A");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("Video");

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

                entity.Property(e => e.VideoAttachment)
                    .HasMaxLength(400)
                    .HasColumnName("video_attachment");

                entity.Property(e => e.VideoDescription)
                    .HasColumnType("text")
                    .HasColumnName("video_description");

                entity.Property(e => e.VideoTitle)
                    .HasMaxLength(255)
                    .HasColumnName("video_title");

                entity.Property(e => e.VideoUrl)
                    .HasMaxLength(400)
                    .HasColumnName("video_url");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__Video__topic_id__1BE81D6E");
            });

            modelBuilder.Entity<VnPayHistory>(entity =>
            {
                entity.ToTable("VnPayHistory");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .HasColumnName("order_id");

                entity.Property(e => e.TransactionId).HasMaxLength(255);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.VnPayHistories)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__VnPayHist__order__02284B6B");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => e.WardsId)
                    .HasName("PK__Wards__E69734F99D69BC7D");

                entity.Property(e => e.WardsId).HasColumnName("Wards_id");

                entity.Property(e => e.DistrictsId).HasColumnName("Districts_id");

                entity.Property(e => e.WardsName)
                    .HasMaxLength(255)
                    .HasColumnName("Wards_name");

                entity.HasOne(d => d.Districts)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictsId)
                    .HasConstraintName("FK__Wards__Districts__76B698BF");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}