
using AutoMapper;
using TAS.Application.Services;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.EF;
using TAS.Test.Mock;

namespace TAS.Test
{
    public class TestNUnitTests
    {
        private UserRegisterRequestDto UserRegister;
        private UserLoginRequestDto UserLogin;
        private IAccountService accountService;
        private IMapper mapper;
        private IUnitOfWork unitOfWork;
        [SetUp]
        public void Setup()
        {
            unitOfWork = MockUnitOfWork.GetUnitOfWork();
            mapper = MockAutoMapper.GetMapper();
            accountService = new AccountService(unitOfWork, mapper);
            UserRegister = new UserRegisterRequestDto
            {
                Username = "thinh",
                Password = "123456",
                Email = "congthinh@gmail.com",
                Phone = "0328299716",
                FirstName = "thinh",
                LastName = "nguyen"
            };
             UserLogin = new UserLoginRequestDto
            {
                UserName = "thinh",
                Password = "thinh1601"
            };

        }
        [Test]
        public void Resgiter_Data()
        {
            // Arrange

            // Act
            var result = UserRegister;
            Assert.That(result, Is.Not.Null
                .And.TypeOf<UserRegisterRequestDto>()
                .And.Property("Username").Length.GreaterThan(3)
                .And.Property("Username").Length.LessThan(20)
                .And.Property("Password").Length.GreaterThan(5)
                .And.Property("Password").Length.LessThan(20)
                .And.Property("Email").Contains("@")
                .And.Property("Phone").Length.GreaterThan(9)
                .And.Property("Phone").Length.LessThan(12)
                .And.Property("FirstName").Length.GreaterThan(3)
                .And.Property("FirstName").Length.LessThan(20)
                .And.Property("LastName").Length.GreaterThan(3)
                .And.Property("LastName").Length.LessThan(20)
                );
        }
        [Test]
        public void ResgiterService()
        {
            // Arrange
            // Act
            var result = accountService.UserRegister(UserRegister).Result;

            // Assert
            Assert.IsTrue(result);

        }
        [Test]
        public void LoginService()
        {
            // Arrange
            // Act
            var resultLogin = accountService.UserLogin(UserLogin).Result;
            // Assert
            Assert.NotNull(resultLogin);

        }
        [Test]
        public void ListUser()
        {
            // Arrange
            // Act
            var result = accountService.GetAccounts().Result;
            // Assert
            Assert.NotNull(result);

        }
    }
}
