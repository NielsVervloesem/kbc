using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGoResto.Interfaces;
using KBCFoodAndGoResto.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    public class ScanResultViewModelTests
    {
        private ScanResultViewModel _sut;

        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IUserDataService> _userServiceMock;
        private Mock<IMealDataService> _mealDataServiceMock;
        private Mock<IScanService> _scanServiceMock;
        private Mock<IMenuDataService> _menuServiceMock;

        private List<ScannedUser> scannedUsers;
        private ScannedUser scannedUser;

        private List<ScannedMeal> scannedMeals;
        private ScannedMeal scannedMeal;

        private Shared.Models.Menu menu;
        private ObservableCollection<Meal> mealList;
        private Meal meal1;
        private Meal meal2;

        private User user;

       [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _userServiceMock = new Mock<IUserDataService>();
            _mealDataServiceMock = new Mock<IMealDataService>();
            _scanServiceMock = new Mock<IScanService>();
            _menuServiceMock = new Mock<IMenuDataService>();

            scannedUsers = new List<ScannedUser>();
            scannedUser = new ScannedUser();
            scannedUser.Id = 1;
            scannedUsers.Add(scannedUser);

            scannedMeals = new List<ScannedMeal>();
            scannedMeal = new ScannedMeal();
            scannedMeal.Id = 1;
            scannedMeals.Add(scannedMeal);

            menu = new Shared.Models.Menu();
            mealList = new ObservableCollection<Meal>();
            meal1 = new Meal();
            meal1.Id = 1; 
            meal2 = new Meal();
            meal2.Id = 2;
            mealList.Add(meal1);
            mealList.Add(meal2);
            menu.Meals = mealList;

            user = new User();
            scannedUser.Id = 1;

            _scanServiceMock.Setup(f => f.MakeMealScan()).ReturnsAsync(scannedMeals);
            _scanServiceMock.Setup(f => f.MakePersonScan()).ReturnsAsync(scannedUsers);
            _menuServiceMock.Setup(f => f.GetLastMenu()).ReturnsAsync(menu);
            _userServiceMock.Setup(f => f.GetUserById(It.IsAny<int>())).ReturnsAsync(user);
            _mealDataServiceMock.Setup(f => f.GetMealById(It.IsAny<long>())).ReturnsAsync(meal1);

            _sut = new ScanResultViewModel(_scanServiceMock.Object,_navigationServiceMock.Object, _mealDataServiceMock.Object, _userServiceMock.Object, _menuServiceMock.Object);
            _sut.ScannedMeal = meal1;
        }

        [Test]
        public void GetHelpCommand_ShouldTriggerNavigationToNumPadView()
        {
            _sut.GetHelpCommand.Execute(null);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<NumPadViewModel>(), Times.Once);
        }

        [Test]
        public void ConfirmCommandCommand_ShouldTriggerAddMealHistoryAndNavigationToScanView()
        {
            _sut.ConfirmCommand.Execute(null);

            _userServiceMock.Setup(f => f.AddMealHistory(It.IsAny<long>(), It.IsAny<List<Meal>>())).ReturnsAsync(new User());

            _userServiceMock.Verify(service => service.AddMealHistory(It.IsAny<long>(), It.IsAny<List<Meal>>()), Times.Once);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<ScanViewModel>(), Times.Once);
        }


        [Test]
        public void ConfirmCommandCommand_ShouldTriggerMessaging()
        {
            var message1Sent = false;
            var message2Sent = false;
            var message3Sent = false;

            MessagingCenter.Subscribe(this, "UsersToNumPad", (ScanResultViewModel vm, List<ScannedUser> users) =>
            {
                message1Sent = true;
            });

            MessagingCenter.Subscribe(this, "UsersToNumPad", (ScanResultViewModel vm, List<ScannedUser> users) =>
            {
                message2Sent = true;
            });

            MessagingCenter.Subscribe(this, "MealsToNumPad", (ScanResultViewModel vm, List<ScannedMeal> meals) =>
            {
                message3Sent = true;
            });

            _sut.GetHelpCommand.Execute(null);

            Assert.IsTrue(message1Sent);
            Assert.IsTrue(message2Sent);
            Assert.IsTrue(message3Sent);
        }




        [Test]
        public void SetupCommand_ShouldSetAlotOfProperties()
        {
            Assert.That(_sut.ScannedUsers, Is.Not.Empty);
            Assert.That(_sut.ScannedMeals, Is.Not.Empty);
            Assert.That(_sut.FilterdScannedMeals.Count, Is.EqualTo(1));
            Assert.That(_sut.ScannedMeal, Is.EqualTo(meal1));
            Assert.That(_sut.ScannedPerson, Is.EqualTo(user));

            _menuServiceMock.Verify(service => service.GetLastMenu(), Times.Once);
            _mealDataServiceMock.Verify(service => service.GetMealById(It.IsAny<long>()), Times.Once);
            _userServiceMock.Verify(service => service.GetUserById(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void RescanCommand_ShouldSetAlotOfProperties()
        {
            _sut.ReScanCommand.Execute(null);

            Assert.That(_sut.ScannedUsers, Is.Not.Empty);
            Assert.That(_sut.ScannedMeals, Is.Not.Empty);
            Assert.That(_sut.FilterdScannedMeals.Count, Is.EqualTo(1));
            Assert.That(_sut.ScannedMeal, Is.EqualTo(meal1));
            Assert.That(_sut.ScannedPerson, Is.EqualTo(user));

            _menuServiceMock.Verify(service => service.GetLastMenu(), Times.Exactly(2));
            _mealDataServiceMock.Verify(service => service.GetMealById(It.IsAny<long>()), Times.Exactly(2));
            _userServiceMock.Verify(service => service.GetUserById(It.IsAny<int>()), Times.Exactly(2));
        }

        [Test]
        public void SettingScannedMealProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("ScannedMeal"))
                    invoked = true;
            };
            _sut.ScannedMeal = new Meal();
            Assert.True(invoked);
        }

        [Test]
        public void SettingScannedPersonProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("ScannedPerson"))
                    invoked = true;
            };
            _sut.ScannedPerson = new User();
            Assert.True(invoked);
        }
    }
}