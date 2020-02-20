using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class HomeViewModelTests
    {
        private HomeViewModel _sut;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IDialogService> _dialogServiceMock;
        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _dialogServiceMock = new Mock<IDialogService>();
            _sut = new HomeViewModel(_navigationServiceMock.Object, _dialogServiceMock.Object);
        }   

        [Test]
        public void NavigateToCommand_ShouldTriggerNavigation()
        {
            _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.LogOutCommand.Execute(null);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<LoginViewModel>(), Times.Once);
        }

        [Test]
        public void NavigateToCommand_ShouldTriggerDialog()
        {
            _dialogServiceMock.Setup(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _sut.LogOutCommand.Execute(null);
            _dialogServiceMock.Verify(f => f.ShowDialogTwoButtons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
