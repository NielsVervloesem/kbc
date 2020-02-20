using NUnit.Framework;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGoResto.Interfaces;
using KBCFoodAndGoResto.ViewModels;
using Moq;
using System.Collections.Generic;
using Xamarin.Forms;
using KBCFoodAndGo.Shared.Interfaces.Services;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    public class EmployeeViewModelTests
    {
        private EmployeeHelpViewModel _sut;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IMealDataService> _mealService;
        private Mock<IUserDataService> _userService;

        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _mealService = new Mock<IMealDataService>();
            _userService = new Mock<IUserDataService>();

            _sut = new EmployeeHelpViewModel(_navigationServiceMock.Object, _mealService.Object, _userService.Object);

            Meal meal = new Meal
            {
                Id = 1
            };
            _sut.SelectedMeal = meal;

            User user = new User
            {
                Id = 1
            };
            _sut.SelectedUser = user;

            List<ScannedUser> userList = new List<ScannedUser>();
            ScannedUser scannedUser1 = new ScannedUser
            {
                Id = 1
            };
            ScannedUser scannedUser2 = new ScannedUser
            {
                Id = 2
            };

            userList.Add(scannedUser1);
            userList.Add(scannedUser2);

            List<ScannedMeal> mealList = new List<ScannedMeal>();
            ScannedMeal scannedMeal1 = new ScannedMeal
            {
                Id = 1
            };
            ScannedMeal scannedMeal2 = new ScannedMeal
            {
                Id = 2
            };

            mealList.Add(scannedMeal1);
            mealList.Add(scannedMeal2);

            _sut.ScanMeal = mealList;
            _sut.ScanUser = userList;
        }

        [Test]
        public void SettingUsersProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Users"))
                    invoked = true;
            };
            _sut.Users = new List<User>();
            Assert.True(invoked);
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

        [Test]
        public void SelectionMealChangedCommand_ShouldUpdateMealPropertiesOfMealsListAndSetSelectedMeal()
        {
            Meal meal1 = new Meal();
            meal1.Id = 1;

            Meal meal2 = new Meal();
            meal2.Id = 2;

            List<Meal> mealList = new List<Meal>();
            mealList.Add(meal1);
            mealList.Add(meal2);

            _sut.Meals = mealList;
            _sut.SelectionMealChanged.Execute(meal1);

            Assert.That(_sut.Meals[0].BackgroundColor, Is.EqualTo(Color.FromHex("#00AEEF")));
            Assert.That(_sut.Meals[1].BackgroundColor, Is.EqualTo(Color.Transparent));
            Assert.That(_sut.SelectedMeal, Is.EqualTo(meal1));
        }

        [Test]
        public void SelectionUserChangedCommand_ShouldUpdateUserPropertiesOfUsersListAndSetSelectedUser()
        {
            User user1 = new User();
            user1.Id = 1;

            User user2 = new User();
            user2.Id = 2;

            List<User> userList = new List<User>();
            userList.Add(user1);
            userList.Add(user2);

            _sut.Users = userList;
            _sut.SelectionUserChanged.Execute(user1);

            Assert.That(_sut.Users[0].BackgroundColor, Is.EqualTo(Color.FromHex("#00AEEF")));
            Assert.That(_sut.Users[1].BackgroundColor, Is.EqualTo(Color.Transparent));
            Assert.That(_sut.SelectedUser, Is.EqualTo(user1));
        }


        [Test]
        public void ConfirmCommandCommand_ShouldTriggerAddMealHistoryAndNavigationToScanView()
        {
            _sut.ConfirmCommand.Execute(null);

            _userService.Setup(f => f.AddMealHistory(It.IsAny<long>(), It.IsAny<List<Meal>>())).ReturnsAsync(new User());

            _userService.Verify(service => service.AddMealHistory(It.IsAny<long>(), It.IsAny<List<Meal>>()), Times.Once);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<ScanViewModel>(), Times.Once);
        }

        [Test]
        public void GetMealsCommand_ShouldSetMealsAndSelectedMealProperty()
        {
            List<Meal> mealList = new List<Meal>();
            mealList.Add(new Meal());

            _mealService.Setup(f => f.GetMealByIdList(It.IsAny<List<long>>())).ReturnsAsync(mealList);

            _sut.GetMealsCommand.Execute(null);

            _mealService.Verify(service => service.GetMealByIdList(It.IsAny<List<long>>()), Times.Once);
            Assert.That(_sut.Meals, Is.Not.Null);
            Assert.That(_sut.SelectedMeal, Is.Not.Null);
        }

        [Test]
        public void GetUsersCommand_ShouldSetUsersAndSelectedUserProperty()
        {
            List<User> userList = new List<User>();
            userList.Add(new User());

            _userService.Setup(f => f.GetUsersByIdList(It.IsAny<List<long>>())).ReturnsAsync(userList);

            _sut.GetUserCommand.Execute(null);

            _userService.Verify(service => service.GetUsersByIdList(It.IsAny<List<long>>()), Times.Once);
            Assert.That(_sut.Users, Is.Not.Null);
            Assert.That(_sut.SelectedUser, Is.Not.Null);
        }
    }
}
