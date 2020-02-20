using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services.Data;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Tests.ServiceTests
{
    [TestFixture]
    class UserServiceTests
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
        public void AddMealHistory_ShouldGetTheCorrectuser()
        {
            User user = new User();
            user.Id = 1;

            _dataRepositoryMock.Setup(f => f.OnPutAsyncReturnUser(It.IsAny<string>(), It.IsAny<List<Meal>>(), It.IsAny<string>())).Returns(Task.FromResult(user));

            List<Meal> mealList = new List<Meal>();
            mealList.Add(new Meal());
            User returnUser =  _sut.AddMealHistory(1, mealList).Result;

            _dataRepositoryMock.Verify(f => f.OnPutAsyncReturnUser(It.IsAny<string>(), It.IsAny<List<Meal>>(), It.IsAny<string>()), Times.Once());

            Assert.That(returnUser.Id, Is.EqualTo(1));
        }
    }
}
