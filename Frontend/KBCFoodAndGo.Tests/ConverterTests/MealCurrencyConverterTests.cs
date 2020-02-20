using KBCFoodAndGo.Shared.Converters;
using NUnit.Framework;

namespace KBCFoodAndGo.Tests.ConverterTests
{
    [TestFixture]
    class MealCurrencyConverterTests
    {
        private MealCurrencyConverter _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new MealCurrencyConverter();
        }

        [Test]
        public void Convert_ShouldAddCurrencyBeforeTheValue()
        {
            double value = 20;
            var result = _sut.Convert(value, typeof(string), null, null);
            Assert.That(result, Is.EqualTo("€20"));
        }

        [Test]
        public void ConvertBack_ShouldRemoveCurrencyBeforeTheValue()
        {
            string value = "€20";
            var result = _sut.ConvertBack(value, typeof(string), null, null);
            Assert.That(result, Is.EqualTo("20"));
        }

        [Test]
        public void ConvertBack_ShouldReturnValueWhenValueIsHasNoCurrency()
        {
            string value = "20";
            var result = _sut.ConvertBack(value, typeof(string), null, null);
            Assert.That(result, Is.EqualTo("20"));
        }
    }
}
