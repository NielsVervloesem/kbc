using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class ManageViewModelTests
    {
        private ManageViewModel _sut;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IMealDataService> _mealDataServiceMock;
        private Mock<IDialogService> _dialogServiceMock;

        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _mealDataServiceMock = new Mock<IMealDataService>();
            _dialogServiceMock = new Mock<IDialogService>();
            _sut = new ManageViewModel(_mealDataServiceMock.Object, _navigationServiceMock.Object, _dialogServiceMock.Object);
        }

        [Test]
        public void NavigateToMealCreateViewModelCommand_ShouldTriggerNavigation()
        {
            _sut.AddMealCommand.Execute(null);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<MealCreateViewModel>(), Times.Once);
        }

        [Test]
        public void NavigateToMenuCreateViewModelCommand_ShouldTriggerNavigation()
        {
            _sut.AddMenuCommand.Execute(null);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<MenuCreateViewModel>(), Times.Once);
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
            _sut.Meals = new ObservableCollection<Meal>();
            Assert.True(invoked);
        }

        [Test]
        public async Task GetMeals_ShouldLoadAllMeals()
        {
            List<Meal> mealsList = new List<Meal> { new Meal() };

            _mealDataServiceMock = new Mock<IMealDataService>();
            _mealDataServiceMock.Setup(f => f.GetAllMeals()).ReturnsAsync(mealsList);

            _sut = new ManageViewModel(_mealDataServiceMock.Object, _navigationServiceMock.Object, _dialogServiceMock.Object);
            await _sut.GetMeals();

            Assert.That(_sut.Meals, Is.EqualTo(mealsList));
            _mealDataServiceMock.Verify(repo => repo.GetAllMeals(), Times.AtLeast(2));
        }

        [Test]
        public void SelectionChanged_ShouldTriggerNavigationAndSendSelectedMeal()
        {
            var selectedMeal = new Meal();
            Meal returnedMeal = null;
            _mealDataServiceMock.Setup(f => f.GetMealById(It.IsAny<long>())).ReturnsAsync(selectedMeal);
            MessagingCenter.Instance.Subscribe<ManageViewModel, long>(this, "sendMeal",
                async (sender, id) =>
                {
                    returnedMeal = await (_mealDataServiceMock.Object.GetMealById(id));
                });
            _sut.SelectionChanged.Execute(selectedMeal);

            _navigationServiceMock.Verify(service => service.NavigateToAsync<MealDetailViewModel>(), Times.Once);
            
            Assert.That(returnedMeal, Is.EqualTo(selectedMeal));
        }
        [Test]
        public void OnDelete_ShouldDeleteMeal()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.Meals = new ObservableCollection<Meal>();
            var meal = new Meal();
            _sut.Meals.Add(meal);
            _sut.OnDelete.Execute(meal);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.That(_sut.Meals,Is.Empty);
        }
    }
}
