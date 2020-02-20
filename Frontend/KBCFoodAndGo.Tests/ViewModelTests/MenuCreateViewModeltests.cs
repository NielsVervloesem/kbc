using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class MenuCreateViewModeltests
    {

        private MenuCreateViewModel _sut;
        private Mock<IMenuDataService> _menuDataServiceMock;
        private Mock<IMealDataService> _mealDataServiceMock;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IDialogService> _dialogServiceMock;

        private List<Meal> mealList = new List<Meal>();
        Menu menu = new Menu();

        [SetUp]
        public void Setup()
        {
            _menuDataServiceMock = new Mock<IMenuDataService>();
            _mealDataServiceMock = new Mock<IMealDataService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _dialogServiceMock = new Mock<IDialogService>();

            menu.Meals = new ObservableCollection<Meal>(mealList);

            _mealDataServiceMock.Setup(f => f.GetAllMeals()).ReturnsAsync(mealList);
            _menuDataServiceMock.Setup(f => f.GetMealsOfDay(It.IsAny<string>())).ReturnsAsync(menu);

            _sut = new MenuCreateViewModel(_menuDataServiceMock.Object, _mealDataServiceMock.Object, _navigationServiceMock.Object, _dialogServiceMock.Object);          
    }
        [Test]
        public void NavigateToMenuCreateViewModelCommand_ShouldTriggerNavigation()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.CancelCommand.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _navigationServiceMock.Verify(service => service.PopBackAsync(), Times.Once);
        }

        [Test]
        public void OnSelectionChange_ShouldNotAddMealToListIfThatListContainTheMeal()
        {
            var meal = new Meal();
            _sut.SelectedMealsMonday.Add(meal);
            _sut.SelectionChanged.Execute(meal);
            Assert.That(_sut.SelectedMealsMonday, Is.Empty);
        }
        [Test]
        public void OnSelectionChange_ShouldAddMealToListIfThatListDoesNotContainTheMeal()
        {
            var meal = new Meal();
            
            _sut.SelectionChanged.Execute(meal);
            Assert.That(_sut.SelectedMealsMonday, Is.Not.Empty);
        }

        [Test]
        public void GetMeals_ShouldSetMealsPropertyWithAllMeals()
        {

            Assert.That(_sut.Meals, Is.EqualTo(mealList));
            _mealDataServiceMock.Verify(repo => repo.GetAllMeals(), Times.Once);
            _menuDataServiceMock.Verify(repo => repo.GetMealsOfDay(It.IsAny<string>()), Times.AtLeast(5));
        }

        [Test]
        public void GetSelectedMeals_ShouldSetSelectedMealsPropertyWithAllSelectedMeals()
        {
        
            _menuDataServiceMock.Setup(f => f.GetLastMenu()).ReturnsAsync(menu);

            Assert.That(_sut.Meals, Is.EqualTo(menu.Meals));
            _menuDataServiceMock.Verify(repo => repo.GetMealsOfDay("Maandag"), Times.AtLeast(1));
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
        public void HighlightSelectedMeals_ShouldEditPropertiesOfTheMealsInTheMealsList()
        {
            _sut.Meals = new ObservableCollection<Meal>();
            _sut.SelectedMeals = new ObservableCollection<Meal>();
            var meal = new Meal();
            meal.Id = 1;
            var meal2 = new Meal();
            meal2.Id = 2;

            _sut.Meals.Add(meal);
            _sut.Meals.Add(meal2);

            _sut.SelectedMeals.Add(meal);

            _sut.HighlightSelectedMeals(_sut.SelectedMeals);

            Assert.That(_sut.Meals[0].IsChecked, Is.True);
            Assert.That(_sut.Meals[1].IsChecked, Is.False);
        }

        [Test]
        public void NavigateToMealCreateViewModelCommand_ShouldTriggerNavigation()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.SelectedMeals = new ObservableCollection<Meal>();
            _sut.ConfirmCommand.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _navigationServiceMock.Verify(service => service.PopBackAsync(), Times.Once);
        }
    }
}
