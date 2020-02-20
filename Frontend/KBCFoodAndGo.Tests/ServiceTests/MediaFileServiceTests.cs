using KBCFoodAndGo.Shared.Services;
using NUnit.Framework;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ServiceTests
{
    [TestFixture]
    class MediaFileServiceTests
    {
        private MediaFileService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new MediaFileService();
        }

        [Test]
        public void GetImageFromMediaFile_ShouldReturnImage()
        {
            var path = Path.Combine(Environment.SpecialFolder.Resources.ToString(), "defaultImage.png");
            MediaFile mediaFile = new MediaFile(path, (Func<MemoryStream>) null, null);
            Image image =  _sut.GetImageFromMediaFile(mediaFile);
            Assert.That(image,Is.Not.Null);
            Assert.That(image,Is.TypeOf<Image>());
        }

        [Test]
        public void ConvertFromBase64String_ShouldReturnImage()
        {
            string base64String = "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==";
            Image image = _sut.ConvertFromBase64String(base64String);
            Assert.That(image, Is.Not.Null);
            Assert.That(image, Is.TypeOf<Image>());
        }

        [Test]
        public async Task ConvertToBase64String_ShouldReturnBase64String()
        {
            var path = Path.Combine(Environment.SpecialFolder.Resources.ToString(), "defaultImage.png");
            MediaFile mediaFile = new MediaFile(path, () => new MemoryStream(), null);
            string base64String = await _sut.ConvertToBase64String(mediaFile);
            Assert.That(base64String, Is.Not.Null);
            Assert.That(base64String, Is.TypeOf<string>());
            Assert.That(Convert.FromBase64String(base64String),Is.TypeOf<Byte[]>());
        }
    }
}
