using System.Collections.Generic;
using System.Threading.Tasks;
using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services.Data;
using Moq;
using NUnit.Framework;

namespace KBCFoodAndGo.Tests.ServiceTests
{ 
    [TestFixture]
    class MealHistoryDataServiceTests
    {
        private MealHistoryDataService _sut;
        private Mock<IDataRepository> _dataRepositoryMock;

        private List<ChartPoint> _chartPoints;
        private ChartPoint _chartPoint;

        [SetUp]
        public void SetUp()
        {
            _dataRepositoryMock = new Mock<IDataRepository>();
            _sut = new MealHistoryDataService(_dataRepositoryMock.Object);

            _chartPoints = new List<ChartPoint>();
            _chartPoint = new ChartPoint();
        }

        [Test]
        public void GetAllMealsFromToday_ShouldGetChartPointForAllMeals()
        {
            _dataRepositoryMock.Setup(f => f.OnGetAsync<List<ChartPoint>>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(_chartPoints));
            
            _chartPoints.Add(_chartPoint);
            List<ChartPoint> returnedChartPoints = _sut.GetAllMealsFromToday().Result;

            Assert.That(_chartPoints, Is.EqualTo(returnedChartPoints));
            Assert.That(returnedChartPoints.Contains(_chartPoint));
            _dataRepositoryMock.Verify(f => f.OnGetAsync<List<ChartPoint>>(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void GetProfitFromLastFiveDays_ShouldGetChartPointsWithProfitsFromLastFiveDays()
        {
            _dataRepositoryMock.Setup(f => f.OnGetAsync<List<ChartPoint>>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(_chartPoints));

            _chartPoints.Add(_chartPoint);
            List<ChartPoint> returnedChartPoints = _sut.GetProfitFromLastFiveDays().Result;

            Assert.That(_chartPoints, Is.EqualTo(returnedChartPoints));
            Assert.That(returnedChartPoints.Contains(_chartPoint));
            _dataRepositoryMock.Verify(f => f.OnGetAsync<List<ChartPoint>>(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void GetAllTimeFavoriteMeals_ShouldReturnChartPointsForTheFavoriteMeals()
        {
            _dataRepositoryMock.Setup(f => f.OnGetAsync<List<ChartPoint>>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(_chartPoints));

            _chartPoints.Add(_chartPoint);
            List<ChartPoint> returnedChartPoints = _sut.GetProfitFromLastFiveDays().Result;

            Assert.That(_chartPoints, Is.EqualTo(returnedChartPoints));
            Assert.That(returnedChartPoints.Contains(_chartPoint));
            _dataRepositoryMock.Verify(f => f.OnGetAsync<List<ChartPoint>>(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }
    }
}
