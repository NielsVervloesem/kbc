using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace KBCFoodAndGo.Tests.ViewModelTests
{

    [TestFixture]
    class MealHistoryViewModelTests
    {
        private MealHistoryViewModel _sut;
        private Mock<IUserDataService> _userService;
        private Mock<LocalStorage> _localStorage;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserDataService>();
            _localStorage = new Mock<LocalStorage>();
            _sut = new MealHistoryViewModel(_userService.Object);
        }

        [Test]
        public void SettingMealHistoryGroupProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("MealHistoryGroup"))
                    invoked = true;
            };
            _sut.MealHistoryGroup = new ObservableCollection<MealHistoryGroup>();
            Assert.True(invoked);
        }

    }
}