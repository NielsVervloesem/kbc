using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class MealsViewModelTests
    {
        private MealsViewModel _sut;
        private Mock<IMenuDataService> _menuDataServiceMock;
        [SetUp]
        public void Setup()
        {
            _menuDataServiceMock = new Mock<IMenuDataService>();
            _sut = new MealsViewModel(_menuDataServiceMock.Object);


        }

        [Test]
        public async Task GetMeals_ShouldLoadAllMealsOfTheLastMenu()
        {
            Menu menu = new Menu();
            ObservableCollection<Meal> mealsList = new ObservableCollection<Meal>();
            menu.Meals = mealsList;

            _menuDataServiceMock.Setup(f => f.GetMealsOfDay(_sut.CurrentDay)).ReturnsAsync(menu);

            _sut = new MealsViewModel(_menuDataServiceMock.Object);
            await _sut.GetMeals();
            _sut.CurrentDay = "Maandag";
            _sut.Meals = new List<Meal>();

            Assert.That(_sut.Meals, Is.EqualTo(menu.Meals));
            _menuDataServiceMock.Verify(repo => repo.GetMealsOfDay(It.IsAny<string>()), Times.AtLeast(2));
        }

        [Test]
        public  void  Constructor_ShouldSetMeals()
        {
            Menu menu = new Menu();
            ObservableCollection<Meal> mealsList = new ObservableCollection<Meal>();
            menu.Meals = mealsList;

            _menuDataServiceMock = new Mock<IMenuDataService>();
            _menuDataServiceMock.Setup(f => f.GetMealsOfDay(_sut.CurrentDay)).ReturnsAsync(menu);

            _sut = new MealsViewModel(_menuDataServiceMock.Object);
            _sut.CurrentDay = "Maandag";
            _sut.Meals = new List<Meal>();

            Assert.That(_sut.Meals, Is.EqualTo(menu.Meals));
            _menuDataServiceMock.Verify(repo => repo.GetMealsOfDay(It.IsAny<string>()), Times.AtLeast(1));
        }

        [Test]
        public void SettingMealsProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Meals"))
                    invoked = true;
            };
            _sut.Meals = new List<Meal>();
            Assert.True(invoked);
        }
    }
    
}
