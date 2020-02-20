using KBCFoodAndGo.Shared.Converters;
using NUnit.Framework;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ConverterTests
{
    [TestFixture]
    class Base64ImageConverterTests
    {
        private Base64ImageConverter _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Base64ImageConverter();
        }

        [Test]
        public void Convert_ShouldConvertBase64StringToImage()
        {
            string base64String = "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==";
            ImageSource imageSource = (ImageSource) _sut.Convert(base64String, typeof(object), null, null);
            Assert.That(imageSource, Is.Not.Null);
            Assert.That(imageSource, Is.TypeOf<StreamImageSource>());
        }

        [Test]
        public void Convert_ShouldReturnDefaultImagePathWhenValueIsNullOrEmpty()
        {
            string base64String = "";
            var value = _sut.Convert(base64String, typeof(object), null, null);
            Assert.That(value, Is.Not.Null);
            Assert.That(value,Is.TypeOf<FileImageSource>());
        }

        [Test]
        public void ConvertBack_ShouldReturnSameValue()
        {
            Image image = new Image();
            var value = _sut.ConvertBack(image, typeof(object), null, null);
            Assert.That(image, Is.EqualTo(value));
        }
    }
}   
