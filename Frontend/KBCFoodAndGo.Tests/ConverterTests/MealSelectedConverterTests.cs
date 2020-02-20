using KBCFoodAndGo.Shared.Converters;
using KBCFoodAndGo.Shared.Models;
using NUnit.Framework;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ConverterTests
{
    [TestFixture]
    class MealSelectedConverterTests
    {
        private MealSelectedConverter _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new MealSelectedConverter();
        }

        [Test]
        public void Convert_ShouldConvertBehaviourToMealObject()
        {
            Meal meal = new Meal();
            SelectedItemChangedEventArgs item = new SelectedItemChangedEventArgs(meal,0);
            var selectedMeal = _sut.Convert(item, typeof(object), null, null);
            Assert.That(selectedMeal,Is.InstanceOf<Meal>());
        }

        [Test]
        public void Convert_ShouldReturnNullWhenInvalidObject()
        {
            Meal meal = new Meal();
            var selectedMeal = _sut.Convert(meal, typeof(object), null, null);
            Assert.IsNull(selectedMeal);
        }

        [Test]
        public void ConvertBack_ShouldReturnSameValue()
        {
            Meal meal = new Meal();
            var selectedMeal = _sut.ConvertBack(meal, typeof(object), null, null);
            Assert.That(selectedMeal, Is.EqualTo(meal));
        }
    }
}   
