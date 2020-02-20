package be.pxl.itproject.kbcfoodandgo.managertest;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.Menu;
import be.pxl.itproject.kbcfoodandgo.repositories.MenuRepository;
import be.pxl.itproject.kbcfoodandgo.services.MenuManagerImp;
import be.pxl.itproject.kbcfoodandgo.services.PusherServiceImp;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnitRunner;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

@RunWith(MockitoJUnitRunner.class)
@SpringBootTest
public class MenuManagerImpTests {

    @InjectMocks
    private MenuManagerImp menuManagerImp;

    @Mock
    private MenuRepository menuRepoMock;

    @Mock
    private PusherServiceImp pusherServiceImp;

    private List<Menu> expectedReturnMenus;
    private List<Meal> menuMealList1;
    private List<Meal> menuMealList2;
    private Meal testMeal1;
    private Meal testMeal2;
    private Meal testMeal3;
    private Meal testMeal4;

    private Menu testMenu1;
    private Menu testMenu2;

    @Before
    public void setUp() {
        expectedReturnMenus = new ArrayList<>();
        menuMealList1 = new ArrayList<>();
        menuMealList2 = new ArrayList<>();

        createMeals();

        menuMealList1.add(testMeal1);
        menuMealList1.add(testMeal2);

        menuMealList2.add(testMeal3);
        menuMealList2.add(testMeal4);

        createMenus();

        expectedReturnMenus.add(testMenu1);
        expectedReturnMenus.add(testMenu2);

    }

    public void createMeals(){
        testMeal1 = new Meal() {{
            setId(1);
            setName("Hot-Dog");
            setPrice(5.5);
            setShortDescription("A nice hot-dog");
        }};

        testMeal2 = new Meal() {{
            setId(2);
            setName("spaghetti");
            setPrice(11.5);
            setShortDescription("Just like my mom used to make");
        }};

        testMeal3 = new Meal() {{
            setId(3);
            setName("Tosti");
            setPrice(5.5);
            setShortDescription("A ham and cheese tosti");
        }};

        testMeal4 = new Meal() {{
            setId(4);
            setName("Pizza");
            setPrice(11.5);
            setShortDescription("A salami pizza");
        }};
    }

    public void createMenus(){
        testMenu1 = new Menu() {
            {
                setId((long) 1);
                setMeals(menuMealList1);
            }
        };

        testMenu2 = new Menu() {
            {
                setId((long) 2);
                setMeals(menuMealList2);
            }
        };
    }


    @Test
    public void getAllMenusShouldReturnAllMenus() {
        when(menuRepoMock.findAll()).thenReturn(expectedReturnMenus);

        List<Menu> realReturnMenus = (List<Menu>) menuManagerImp.getAllMenus();

        Assert.assertNotNull(realReturnMenus);
        Assert.assertEquals(realReturnMenus.size(), expectedReturnMenus.size());
        Mockito.verify(menuRepoMock, Mockito.times(1)).findAll();
        Mockito.verifyNoMoreInteractions(menuRepoMock);
    }

    @Test
    public void getMenuByIdShouldReturnMenuWithCorrectIdAndOnlyOneMenu(){
        when(menuRepoMock.findById(1L)).thenReturn(Optional.ofNullable(testMenu1));

        Optional<Menu> returnMenu = menuManagerImp.getMenuById(1L);

        Assert.assertNotNull(returnMenu);
        Assert.assertEquals(returnMenu.get().getId(), testMenu1.getId());
        Mockito.verify(menuRepoMock, Mockito.times(1)).findById(1L);
        Mockito.verifyNoMoreInteractions(menuRepoMock);
    }

    @Test
    public void updateMenuShouldChangeTheMenuValues() {
        Menu changedMenu = new Menu();
        changedMenu.setId(1L);
        changedMenu.setMeals(menuMealList2);

        when(menuRepoMock.findById(Mockito.anyLong())).thenReturn(Optional.of(changedMenu));
        when(menuRepoMock.save(Mockito.any(Menu.class))).thenReturn(changedMenu);

        List<Meal> returnMeals = menuManagerImp.updateMenu(1L, menuMealList2);

        Assert.assertNotNull(returnMeals);
        Assert.assertEquals(returnMeals, changedMenu.getMeals());

        Mockito.verify(menuRepoMock, Mockito.times(1)).save(Mockito.any(Menu.class));
        Mockito.verify(menuRepoMock, Mockito.times(1)).findById(Mockito.anyLong());
        Mockito.verifyNoMoreInteractions(menuRepoMock);
    }

    @Test
    public void shouldAddNewMenu() {
        menuManagerImp.addMenu(testMenu1);
        Assert.assertTrue(expectedReturnMenus.contains(testMenu1));
        verify(menuRepoMock, Mockito.times(1)).save(testMenu1);
        verify(pusherServiceImp, Mockito.times(1)).onCreateMenu();
    }

    @Test
    public void returnLastMenuShouldReturnTheLastMenu(){
        Meal lastMeal1 = new Meal() {{
            setId(5);
            setName("lasagne");
            setPrice(5.5);
            setShortDescription("Nice vegetarian lasagne");
        }};

        Meal lastMeal2 = new Meal() {{
            setId(6);
            setName("soup");
            setPrice(11.5);
            setShortDescription("Fresh tomato soup");
        }};

        List<Meal> menuMealList3 = new ArrayList<>();

        menuMealList3.add(lastMeal1);
        menuMealList3.add(lastMeal2);

        Menu newestMenu = new Menu(){{
           setId(3L);
           setMeals(menuMealList3);
        }};

        when(menuRepoMock.findFirstByOrderByIdDesc()).thenReturn(Optional.ofNullable(newestMenu));

        menuManagerImp.addMenu(newestMenu);

        Optional<Menu> lastMenu = menuManagerImp.getLastMenu();

        Assert.assertNotNull(lastMenu);
        Assert.assertEquals(newestMenu.getId(), lastMenu.get().getId());
    }


}

