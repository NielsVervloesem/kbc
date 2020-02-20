using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services.Data;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Tests.ServiceTests
{
    [TestFixture]
    class MealDataServiceTests
    {
        private MealDataService _sut;
        private Mock<IDataRepository> _dataRepositoryMock;
        [SetUp]
        public void Setup()
        {
            _dataRepositoryMock = new Mock<IDataRepository>();
            _sut = new MealDataService(_dataRepositoryMock.Object);
        }

        [Test]
        public void UpdateMeal_ShouldUpdateTheMeal()
        {
            Meal meal = new Meal();
            Meal mealUpdate = new Meal {Name = "test"};
            _dataRepositoryMock.Setup(f => f.OnPutAsync(It.IsAny<string>(), mealUpdate, It.IsAny<string>())).Returns(Task.FromResult(mealUpdate));
            meal = _sut.UpdateMeal(meal.Id,mealUpdate).Result;
            Assert.That(meal, Is.EqualTo(mealUpdate));
            _dataRepositoryMock.Verify(f => f.OnPutAsync(It.IsAny<string>(), meal, It.IsAny<string>()), Times.Once());

        }

        [Test]
        public void GetAllMeals_ShouldGetAllTheMeals()
        {
            List<Meal> mealList = new List<Meal>();
            _dataRepositoryMock.Setup(f => f.OnGetAsync<List<Meal>>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(mealList));
            Meal meal = new Meal();
            mealList.Add(meal);
            List<Meal> meals = _sut.GetAllMeals().Result.ToList();
            Assert.That(meals.Count,Is.EqualTo(mealList.Count));
            Assert.That(meals.Contains(meal));
            _dataRepositoryMock.Verify(f => f.OnGetAsync<List<Meal>>(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public async Task DeleteMeal_ShouldDeleteTheMeal()
        {
            Meal meal = new Meal();
            _dataRepositoryMock.Setup(f => f.OnDeleteAsync(It.IsAny<string>(), It.IsAny<string>()));
            await _sut.DeleteMeal(meal.Id);
            _dataRepositoryMock.Verify(f => f.OnDeleteAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void AddMeal_ShouldAddANewMeal()
        {
            Meal meal = new Meal();
            _dataRepositoryMock.Setup(f => f.OnPostAsync(It.IsAny<string>(),meal, It.IsAny<string>())).Returns(Task.FromResult(meal));
            Meal addedMeal = _sut.AddMeal(meal).Result;
            Assert.That(meal,Is.EqualTo(addedMeal));
            _dataRepositoryMock.Verify(f => f.OnPostAsync(It.IsAny<string>(), meal, It.IsAny<string>()),Times.Once());
        }
    }
}
