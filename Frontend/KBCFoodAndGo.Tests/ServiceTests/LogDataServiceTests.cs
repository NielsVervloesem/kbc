using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services.Data;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Tests.ServiceTests
{
    [TestFixture]
    class LogDataServiceTests
    {
        private LogDataService _sut;
        private Mock<IDataRepository> _dataRepositoryMock;
        [SetUp]
        public void Setup()
        {
            _dataRepositoryMock = new Mock<IDataRepository>();
            _sut = new LogDataService(_dataRepositoryMock.Object);
        }

        [Test]
        public void GetAllLogs_ShouldGetAllTheLogs()
        {
            List<Log> logList = new List<Log>();
            _dataRepositoryMock.Setup(f => f.OnGetAsync<List<Log>>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(logList));
            Log log = new Log();
            logList.Add(log);
            List<Log> logs = _sut.GetAllLogs().Result.ToList();
            Assert.That(logs.Count,Is.EqualTo(logList.Count));
            Assert.That(logs.Contains(log));
            _dataRepositoryMock.Verify(f => f.OnGetAsync<List<Log>>(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}
