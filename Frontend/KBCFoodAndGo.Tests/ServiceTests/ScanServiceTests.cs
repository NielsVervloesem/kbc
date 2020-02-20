using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services.Data;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Tests.ServiceTests
{
    [TestFixture]
    class ScanServiceTests
    {
        private ScanService _sut;
        private Mock<IDataRepository> _dataRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _dataRepositoryMock = new Mock<IDataRepository>();
            _sut = new ScanService(_dataRepositoryMock.Object);
        }

    }
}