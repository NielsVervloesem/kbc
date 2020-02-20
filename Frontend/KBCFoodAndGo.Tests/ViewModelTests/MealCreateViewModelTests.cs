using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Validators;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;
using System.Reflection;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class MealCreateViewModelTests
    {
        private MealCreateViewModel _sut;
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
            _sut = new MealCreateViewModel(
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
        public void ViewTitle_ShouldHaveFixedName()
        {
            Assert.That(_sut.Title, Is.EqualTo("Maaltijd Aanmaken"));
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
        public void DeleteMealCommand_ShouldSetMealNullAndReturnToOneViewBack()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.Meal = new Meal();
            _sut.DeleteMeal.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Null(_sut.Meal);
            _navigationServiceMock.Verify(f => f.PopBackAsync(), Times.Once);
        }

        [Test]
        public void DeleteMealCommand_ShouldSetMealNotNullAndNotReturnToOneViewBacWhenMealIsNull()
        {
            _sut.Meal = null;
            _sut.DeleteMeal.Execute(null);
            _navigationServiceMock.Verify(f => f.PopBackAsync(), Times.Never);
        }

        [Test]
        public void AddMealCommand_ShouldSendAPositiveMessageWhenTheMealIsValid()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.Meal = new Meal { Name = "test", Price = 1.0, ShortDescription = "test", Id = 1 };
            bool messageReceived = false;
            MessagingCenter.Subscribe<MealCreateViewModel, bool>(
                this, "reloadList", (sender, arg) =>
                {
                    messageReceived = true;
                });
            _sut.AddMeal.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.True(messageReceived);
        }

        [Test]
        public void AddMealCommand_ShouldNotSendAPositiveMessageWhenTheMealIsInvalid()
        {
            _sut.Meal = new Meal();
            bool messageReceived = false;
            MessagingCenter.Subscribe<MealCreateViewModel, bool>(
                this, "reloadList", (sender, arg) =>
                {
                    messageReceived = true;
                });
            _sut.AddMeal.Execute(null);
            Assert.False(messageReceived);
        }

        [Test]
        public void AddMealCommand_ShouldNavigateOneViewBackWhenMealWasAddedSuccessful()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
           
            _sut.Meal = new Meal { Name = "test", Price = 1.0, ShortDescription = "test", Id = 1 };
            _sut.AddMeal.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _navigationServiceMock.Verify(f => f.PopBackAsync(),Times.Once);
            _dialogServiceMock.Verify(f => f.ShowDialog(It.IsAny<string>(),It.IsAny<string>(),It.IsAny<string>()),Times.Once);
        }


        [Test]
        public void AddMealCommand_ShouldUpdateTheMealWhenValid()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.Meal = new Meal {Name = "test", Price = 1.0, ShortDescription = "test", Id = 1 };
            var updateMeal = _sut.Meal;
            updateMeal.Name = "update";
            _mealDataServiceMock.Setup(f => f.UpdateMeal(_sut.Meal.Id, updateMeal)).ReturnsAsync( updateMeal);
            _sut.AddMeal.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.That(_sut.Meal.Name, Is.EqualTo(updateMeal.Name));
            _dialogServiceMock.Verify(f => f.ShowDialog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void AddMealCommand_ShouldNotAddTheMealWhenInvalid()
        {
            _sut.Meal = new Meal();
            _sut.AddMeal.Execute(null);
            _mealDataServiceMock.Verify(service => service.AddMeal(_sut.Meal), Times.Never);
            _dialogServiceMock.Verify(f => f.ShowDialog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.That(_sut.Meal.Name,Is.EqualTo(null));
        }

        [Test]
        public void AddMealCommand_ShouldAddTheMealWhenValid()
        {
            var result = _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.Meal = new Meal { Name = "test", Price = 1.0, ShortDescription = "test", Id = 1 };
            _sut.AddMeal.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mealDataServiceMock.Verify(service => service.AddMeal( _sut.Meal), Times.Once);
            _dialogServiceMock.Verify(f => f.ShowDialog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void SetDefaultImage_ShouldSetImageWithTheDefaultImage()
        {
            MethodInfo methodInfo = typeof(MealCreateViewModel).GetMethod("SetDefaultImage", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { };
            if (methodInfo != null) methodInfo.Invoke(_sut, parameters);
            Assert.That(_sut.Image, Is.Not.Null);
            _mediaFileServiceMock.Verify(f => f.ConvertFromBase64String(It.IsAny<string>()), Times.Never);
        }
    }
}
