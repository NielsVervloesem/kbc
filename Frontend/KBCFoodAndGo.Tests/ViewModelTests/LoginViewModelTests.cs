using KBCFoodAndGo.Interfaces.Services;
using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class LoginViewModelTests
    {
        private LoginViewModel _sut;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IUserDataService> _userDataServiceMock;
        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();
            _userDataServiceMock = new Mock<IUserDataService>();

            _sut = new LoginViewModel(_navigationServiceMock.Object, _userDataServiceMock.Object);
        }

        [Test]
        public void NavigateToCommand_WithAdminUser_ShouldTriggerNavigationToHomeViewModel()
        {
            User adminUser = new User();
            adminUser.Role = "ADMIN";
            _sut.User = adminUser;

            _userDataServiceMock.Setup(f => f.LoginAsync(adminUser)).ReturnsAsync(adminUser);

            _sut.NavigateToCommand.Execute(null);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<HomeViewModel>(), Times.Once);
        }

        [Test]
        public void NavigateToCommand_WithCustomerUser_ShouldTriggerNavigationToMenuViewModel()
        {
            User customerUser = new User();
            customerUser.Role = "CUSTOMER";
            _sut.User = customerUser;

            _userDataServiceMock.Setup(f => f.LoginAsync(customerUser)).ReturnsAsync(customerUser);

            _sut.NavigateToCommand.Execute(null);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<MenuViewModel>(), Times.Once);
        }


        [Test]
        public void NavigateToCommand_WithEmployee_ShouldTriggerNavigationToMenuViewModel()
        {
            User employeeUser = new User();
            employeeUser.Role = "CAFETARIA_EMPLOYEE";
            _sut.User = employeeUser;

            _userDataServiceMock.Setup(f => f.LoginAsync(employeeUser)).ReturnsAsync(employeeUser);

            _sut.NavigateToCommand.Execute(null);
            _navigationServiceMock.Verify(service => service.NavigateToAsync<MenuViewModel>(), Times.Once);
        }
    }
}
