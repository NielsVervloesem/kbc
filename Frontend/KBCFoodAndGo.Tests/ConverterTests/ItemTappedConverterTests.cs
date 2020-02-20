using KBCFoodAndGo.Shared.Converters;
using KBCFoodAndGo.Shared.Models;
using NUnit.Framework;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ConverterTests
{
    [TestFixture]
    class ItemTappedConverterTests
    {

        private ItemTappedConverter _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ItemTappedConverter();
        }

        [Test]
        public void ConvertItemTapped_ShouldConvertBehaviourToMealObject()
        {
            Meal meal = new Meal();
            List<Meal> mealList = new List<Meal>();
            mealList.Add(meal);

            ItemTappedEventArgs item = new ItemTappedEventArgs(mealList,meal, 0);
            var selectedMeal = _sut.Convert(item, typeof(object), null, null);
            Assert.That(selectedMeal, Is.InstanceOf<Meal>());
        }

        [Test]
        public void Convert_ShouldReturnNullWhenInvalidObject()
        {
            Meal meal = new Meal();
            var selectedMeal = _sut.Convert(meal, typeof(object), null, null);
            Assert.IsNull(selectedMeal);
        }
    }
}

