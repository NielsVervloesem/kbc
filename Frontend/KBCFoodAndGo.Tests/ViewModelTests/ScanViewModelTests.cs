using KBCFoodAndGoResto.Interfaces;
using KBCFoodAndGoResto.ViewModels;
using Moq;
using NUnit.Framework;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class ScanViewModelTests
    {
        private ScanViewModel _sut;
        private Mock<INavigationService> _navigationServiceMock;
        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _sut = new ScanViewModel(_navigationServiceMock.Object);
        }

        [Test]
        public void ScanMeal_ShouldTriggerNavigation()
        {
            _sut.ScanCommand.Execute(null);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<ScanResultViewModel>(), Times.Once);
        }
    }
}
