using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class MealDetailViewModelTests
    {
        private MealDetailViewModel _sut;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IMealDataService> _mealDataService;

        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _mealDataService = new Mock<IMealDataService>();
            _sut = new MealDetailViewModel(_navigationServiceMock.Object, _mealDataService.Object);
        }   

        [Test]
        public void NavigateToCommand_ShouldTriggerNavigationAndSendMeal()
        {
            _sut.Meal = new Meal();
            Meal sendMeal = null;

            MessagingCenter.Instance.Subscribe<MealDetailViewModel, Meal>(this, "sendMeal", (sender, meal) => { sendMeal = meal; });
            _sut.NavigateToCommand.Execute(null);


            _navigationServiceMock.Verify(service => service.NavigateToAsync<MealEditViewModel>(), Times.Once);
            Assert.That(sendMeal, Is.EqualTo(_sut.Meal));
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
    }
}
