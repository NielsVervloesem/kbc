using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Validators;
using KBCFoodAndGo.ViewModels;
using KBCFoodAndGo.Views;
using Moq;
using NUnit.Framework;
using System.Reflection;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class MealEditViewModelTests
    {
        private MealEditViewModel _sut;
        private Mock<IMealDataService> _mealDataServiceMock;
        private Mock<INavigationService> _navigationServiceMock;
        private MealValidator _mealValidator;
        private Mock<IDialogService> _dialogServiceMock;
        private Mock<IMediaFileService> _mediaFileServiceMock;


        [SetUp]
        public void Setup()
        {
            _mealDataServiceMock = new Mock<IMealDataService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _mealValidator = new MealValidator();
            _mediaFileServiceMock = new Mock<IMediaFileService>();
            _dialogServiceMock = new Mock<IDialogService>();
            _sut = new MealEditViewModel(
                _navigationServiceMock.Object,
                _mealDataServiceMock.Object,
                _mealValidator,
                _dialogServiceMock.Object,
                _mediaFileServiceMock.Object);
        }
        [Test]
        public void SettingMealProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Meal"))
                    invoked = true;
            };
            _sut.Meal = new Meal();
            Assert.True(invoked);
        }
        [Test]
        public void SettingImageProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Image"))
                    invoked = true;
            };
            _sut.Image = new Image();
            Assert.True(invoked);
        }

        [Test]
        public void SettingTitleProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Title"))
                    invoked = true;
            };
            _sut.Title = new string("");
            Assert.True(invoked);
        }
        [Test]
        public void DeleteMealCommand_ShouldSendAPositiveMessageWhenTheMealIsNotNull()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            _sut.Meal = new Meal();
            bool messageReceived = false;
            MessagingCenter.Subscribe<MealEditViewModel, bool>(
                this, "reloadList", (sender, arg) =>
                {
                    messageReceived = true;
                });
            _sut.DeleteMeal.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.True(messageReceived);
        }

        [Test]
        public void DeleteMealCommand_ShouldNotSendAPositiveMessageWhenTheMealIsNull()
        {
            bool messageReceived = false;
            MessagingCenter.Subscribe<MealEditViewModel, bool>(
                this, "reloadList", (sender, arg) =>
                {
                    messageReceived = true;
                });
            _sut.DeleteMeal.Execute(null);
            Assert.False(messageReceived);
        }

        [Test]
        public void DeleteMealCommand_ShouldTriggerNavigation()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.Meal = new Meal();
            _sut.DeleteMeal.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _navigationServiceMock.Verify(service => service.PopUntilDestination(typeof(HomeView)), Times.Once);
        }

        [Test]
        public void AddMealCommand_ShouldUpdateTheMealWhenValid()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            _sut.Meal = new Meal { Name = "test", Price = 1.0, ShortDescription = "test", Id = 1 };
            _sut.AddMeal.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mealDataServiceMock.Verify(service => service.UpdateMeal(_sut.Meal.Id, _sut.Meal), Times.Once);
            _dialogServiceMock.Verify(f => f.ShowDialog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddMealCommand_ShouldNotUpdateTheMealWhenInvalid()
        {
            _sut.Meal = new Meal();
            _sut.AddMeal.Execute(null);
            _mealDataServiceMock.Verify(service => service.UpdateMeal(_sut.Meal.Id, _sut.Meal), Times.Never);
            _dialogServiceMock.Verify(f => f.ShowDialog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void SetDefaultImage_ShouldExecuteWhenMealHasNoBase64ImageProperty()
        {
            Meal meal = new Meal();
            MethodInfo methodInfo = typeof(MealEditViewModel).GetMethod("CheckMealImage", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { meal };
            if (methodInfo != null) methodInfo.Invoke(_sut, parameters);
            Assert.That(_sut.Image, Is.Not.Null);
            _mediaFileServiceMock.Verify(f => f.ConvertFromBase64String(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void SetDefaultImage_ShouldSetImageWithTheDefaultImage()
        {
            MethodInfo methodInfo = typeof(MealEditViewModel).GetMethod("SetDefaultImage", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { };
            if (methodInfo != null) methodInfo.Invoke(_sut, parameters);
            Assert.That(_sut.Image, Is.Not.Null);
            _mediaFileServiceMock.Verify(f => f.ConvertFromBase64String(It.IsAny<string>()), Times.Never);
        }
    }
}
