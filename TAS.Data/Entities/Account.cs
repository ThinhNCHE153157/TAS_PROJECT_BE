using System;
using System.Collections.Generic;
using TAS.Data.Entities.Interfaces;

namespace TAS.Data.Entities
{
    public partial class Account : IDateTracking
    {
        public Account()
        {
            AccountFlashcards = new HashSet<AccountFlashcard>();
            Enterprises = new HashSet<Enterprise>();
            Orders = new HashSet<Order>();
            TestResults = new HashSet<TestResult>();
            Tokens = new HashSet<Token>();
            Courses = new HashSet<Course>();
            Roles = new HashSet<Role>();
        }

        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public bool? IsVerified { get; set; }
        public string? Otp { get; set; }
        public DateTime? Otpexpiretime { get; set; }
        public string? Address { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<AccountFlashcard> AccountFlashcards { get; set; }
        public virtual ICollection<Enterprise> Enterprises { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}