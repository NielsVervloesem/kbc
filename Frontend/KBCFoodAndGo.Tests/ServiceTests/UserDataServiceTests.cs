using System.Threading.Tasks;
using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services.Data;
using Moq;
using NUnit.Framework;

namespace KBCFoodAndGo.Tests.ServiceTests
{
    [TestFixture]
    class UserDataServiceTests
    {
        private UserDataService _sut;
        private Mock<IDataRepository> _dataRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _dataRepositoryMock = new Mock<IDataRepository>();
            _sut = new UserDataService(_dataRepositoryMock.Object);
        }

        [Test]
        public void CreateAsync_ShouldAddUser()
        {
            User user = new User();
            _dataRepositoryMock.Setup(f => f.OnPostAsync<User>(It.IsAny<string>(), user, It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            User addedUser = _sut.CreateAsync(user).Result;

            Assert.That(addedUser, Is.EqualTo(user));
            _dataRepositoryMock.Verify(f => f.OnPostAsync(It.IsAny<string>(), user, It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void LoginAsync_ShouldReturnUser_WhenLoginIsSuccess()
        {
            User user = new User(1, "test@test.com", "test", "ADMIN");
            _dataRepositoryMock.Setup(f => f.OnPostAsync<User>(It.IsAny<string>(), user, It.IsAny<string>())).Returns(Task.FromResult(user));
            User loggedInUser = _sut.LoginAsync(user).Result;

            Assert.That(loggedInUser, Is.EqualTo(user));
            _dataRepositoryMock.Verify(f => f.OnPostAsync(It.IsAny<string>(), user, It.IsAny<string>()), Times.Once());
        }


        [Test]
        public void GetUserById_ShouldReturnUserWithCorrectId()
        {
            User user = new User(1, "test@test.com", "test", "ADMIN");
            _dataRepositoryMock.Setup(f => f.OnGetAsync<User>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(user));
            User returnUser = _sut.GetUserById(1).Result;

            Assert.That(returnUser, Is.EqualTo(user));
            _dataRepositoryMock.Verify(f => f.OnGetAsync<User>(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}
