using KBCFoodAndGo.Shared.Interfaces.Repository;
using KBCFoodAndGo.Shared.Models;
using KBCFoodAndGo.Shared.Services.Data;
using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KBCFoodAndGo.Tests.ServiceTests
{
    [TestFixture]
    class MenuDataServiceTests
    {
        private MenuDataService _sut;
        private Mock<IDataRepository> _dataRepositoryMock;
        [SetUp]
        public void Setup()
        {
            _dataRepositoryMock = new Mock<IDataRepository>();
            _sut = new MenuDataService(_dataRepositoryMock.Object);
        }       
         
        [Test]
        public void GetLastMenu_ShouldGetLastMenu()
        {
            Menu menu = new Menu();
            Meal meal = new Meal();
            ObservableCollection<Meal> mealList = new ObservableCollection<Meal>();
            mealList.Add(meal);
            menu.Meals = mealList;

            _dataRepositoryMock.Setup(f => f.OnGetAsync<Menu>(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(menu));


            Menu returnMenu = _sut.GetLastMenu().Result;
            Assert.That(menu.Meals.Count, Is.EqualTo(returnMenu.Meals.Count));
            Assert.That(returnMenu, Is.EqualTo(menu));
            _dataRepositoryMock.Verify(f => f.OnGetAsync<Menu>(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void AddMeal_ShouldAddANewMeal()
        {
            Menu menu = new Menu();
            _dataRepositoryMock.Setup(f => f.OnPostAsync(It.IsAny<string>(), menu, It.IsAny<string>())).Returns(Task.FromResult(menu));
            Menu addedMenu = _sut.AddMenu(menu).Result;
            Assert.That(menu, Is.EqualTo(addedMenu));
            _dataRepositoryMock.Verify(f => f.OnPostAsync(It.IsAny<string>(), menu, It.IsAny<string>()), Times.Once());
        }
    }

}
