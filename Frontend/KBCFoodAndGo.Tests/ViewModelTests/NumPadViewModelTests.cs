using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGoResto.Interfaces;
using KBCFoodAndGoResto.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Xamarin.Forms;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    public class NumPadViewModelTests
    {

        private NumPadViewModel _sut;
        private Mock<INavigationService> _navigationServiceMock;

        [SetUp]
        public void Setup()
        {
            _navigationServiceMock = new Mock<INavigationService>();

            _sut = new NumPadViewModel(_navigationServiceMock.Object);
        }

        [Test]
        public void SettingInputCodeProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("InputCode"))
                    invoked = true;
            };
            _sut.InputCode = "1234";
            Assert.True(invoked);
        }

        [Test]
        public void ClearCommand_ShouldSetInputCodeToEmpty()
        {
            _sut.InputCode = "45";
            _sut.ClearCommand.Execute(null);

            Assert.That(_sut.InputCode, Is.Empty);
        }

        [Test]
        public void EnterCodeCommand_ShouldAddNumberToInputCode()
        {
            _sut.EnterCodeCommand.Execute("4");

            Assert.That(_sut.InputCode, Is.Not.Empty);
            Assert.That(_sut.InputCode, Is.EqualTo("4"));
        }

        [Test]
        public void OkCommand_ShouldNavigateToEmployeeViewModelIfInputCodeEqualsToPassword()
        {
            _sut.InputCode = "1234";
            _sut.Password = "1234";

            _sut.OkCommand.Execute(null);

            _navigationServiceMock.Verify(service => service.NavigateToAsync<EmployeeHelpViewModel>(), Times.Once);
        }


        [Test]
        public void OkCommand_ShouldNotNavigateToEmployeeViewModelIfInputCodeDoesNOtEqualsToPassword()
        {
            _sut.InputCode = "1234";
            _sut.Password = "9876";

            _sut.OkCommand.Execute(null);

            _navigationServiceMock.Verify(service => service.NavigateToAsync<EmployeeHelpViewModel>(), Times.Never);
        }

        [Test]
        public void OkCommand_ShouldTriggerMessageToEmployeeViewModel()
        {
            var message1Sent = false;
            var message2Sent = false;

            MessagingCenter.Subscribe<NumPadViewModel, List<ScannedMeal>>(this, "sendScannedMeals",
                   (sender, filterdScannedMeals) =>
                   {
                       message1Sent = true;
                   });


            MessagingCenter.Subscribe<NumPadViewModel, List<ScannedMeal>>(this, "sendScannedMeals",
                   (sender, filterdScannedMeals) =>
                   {
                       message2Sent = true;
                   });

            _sut.OkCommand.Execute(null);

            Assert.IsTrue(message1Sent);
            Assert.IsTrue(message2Sent);
        }
    }
}
