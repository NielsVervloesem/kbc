using KBCFoodAndGo.Shared.Interfaces.Services;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Tests.ViewModelTests
{
    [TestFixture]
    class LogViewModelTests
    {
        private LogViewModel _sut;
        private Mock<ILogDataService> _logDataServiceMock;
        [SetUp]
        public void Setup()
        {
            _logDataServiceMock = new Mock<ILogDataService>();
            _sut = new LogViewModel(_logDataServiceMock.Object);
        }   

        [Test]
        public async Task GetLogs_ShouldLoadAllLogs()
        {
            List<Log> logList = new List<Log> { new Log() };

            _logDataServiceMock = new Mock<ILogDataService>();
            _logDataServiceMock.Setup(f => f.GetAllLogs()).ReturnsAsync(logList);

            _sut = new LogViewModel(_logDataServiceMock.Object);
            await _sut.GetLogs();

            Assert.That(_sut.Logs, Is.EqualTo(logList));
            _logDataServiceMock.Verify(repo => repo.GetAllLogs(), Times.AtLeast(2));
        }

        [Test]
        public  void  Constructor_ShouldSetLogs()
        {
            List<Log> logList = new List<Log>();

            _logDataServiceMock = new Mock<ILogDataService>();
            _logDataServiceMock.Setup(f => f.GetAllLogs()).ReturnsAsync(logList);

            _sut = new LogViewModel(_logDataServiceMock.Object);

            Assert.That(_sut.Logs, Is.EqualTo(logList));
            _logDataServiceMock.Verify(repo => repo.GetAllLogs(), Times.AtLeast(1));
        }

        [Test]
        public void SettingLogsProperty_ShouldRaisePropertyChanged()
        {
            bool invoked = false;

            _sut.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Logs"))
                    invoked = true;
            };
            _sut.Logs = new List<Log>();
            Assert.True(invoked);
        }
    }
}
