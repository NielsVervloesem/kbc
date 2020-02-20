package be.pxl.itproject.kbcfoodandgo.managertest;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.repositories.MealRepository;
import be.pxl.itproject.kbcfoodandgo.services.MealManagerImp;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnitRunner;
import org.springframework.boot.test.context.SpringBootTest;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import static org.mockito.Mockito.when;

@RunWith(MockitoJUnitRunner.class)
@SpringBootTest
public class MealManagerImpTests {

    @InjectMocks
    private MealManagerImp mealManagerImp;

    @Mock
    private MealRepository mealRepoMock;

    private List<Meal> expectedReturnMeals;
    private List<Meal> expectedSearchReturnMeals;
    private Meal testMeal1;

    @Before
    public void setUp() {
        expectedReturnMeals = new ArrayList<>();
        expectedSearchReturnMeals = new ArrayList<>();
        testMeal1 = new Meal() {{
            setId(1);
            setName("Hot-Dog");
            setPrice(5.5);
            setShortDescription("A nice hot-dog");
        }};

        Meal testMeal2 = new Meal() {{
            setId(2);
            setName("spaghetti");
            setPrice(11.5);
            setShortDescription("Just like my mom used to make");
        }};

        expectedReturnMeals.add(testMeal1);
        expectedReturnMeals.add(testMeal2);
        expectedSearchReturnMeals.add(testMeal1);

    }


    @Test
    public void getAllMealsShouldReturnAllMeals() throws IOException {
        when(mealRepoMock.findAll()).thenReturn(expectedReturnMeals);

        List<Meal> realReturnMeals = mealManagerImp.getAllMeals();

        Assert.assertNotNull(realReturnMeals);
        Assert.assertEquals(realReturnMeals.size(), expectedReturnMeals.size());
        Mockito.verify(mealRepoMock, Mockito.times(1)).findAll();
        Mockito.verifyNoMoreInteractions(mealRepoMock);
    }

    @Test
    public void getMealByIdShouldReturnMealWithCorrectIdAndOnlyOneMeal() {
        when(mealRepoMock.findById(1L)).thenReturn(Optional.ofNullable(testMeal1));

        Optional<Meal> returnMeal = mealManagerImp.getMealById(1L);

        Assert.assertNotNull(returnMeal);
        Mockito.verify(mealRepoMock, Mockito.times(1)).findById(1L);
        Mockito.verifyNoMoreInteractions(mealRepoMock);
    }

    @Test
    public void getMealByTextShouldReturnMealWithCorrectIdAndOnlyOneMeal() {
        when(mealRepoMock.findByNameContainingIgnoreCase("hot")).thenReturn(expectedSearchReturnMeals);

        List<Meal> returnMeal = mealManagerImp.getMealsByText("hot");

        Assert.assertNotNull(returnMeal);
        Assert.assertEquals(testMeal1.getId(), returnMeal.get(0).getId());
        Mockito.verify(mealRepoMock, Mockito.times(1)).findByNameContainingIgnoreCase("hot");
        Mockito.verifyNoMoreInteractions(mealRepoMock);
    }

    @Test
    public void updateMealShouldChangeTheMealValues() {
        Meal changedMeal = new Meal();

        changedMeal.setId(1);
        changedMeal.setName("changed");
        changedMeal.setPrice(9);
        changedMeal.setShortDescription("changed");

        when(mealRepoMock.save(testMeal1)).thenReturn(changedMeal);

        Meal returnMeal = mealManagerImp.updateMeal(testMeal1);

        Assert.assertNotNull(returnMeal);
        Assert.assertEquals(testMeal1.getId(), changedMeal.getId());
        Assert.assertNotEquals(testMeal1.getPrice(), changedMeal.getPrice(),0.001);
        Assert.assertNotEquals(testMeal1.getName(), changedMeal.getName());
        Assert.assertNotEquals(testMeal1.getShortDescription(), changedMeal.getShortDescription());

        Mockito.verify(mealRepoMock, Mockito.times(1)).save(testMeal1);
        Mockito.verifyNoMoreInteractions(mealRepoMock);
    }

    @Test
    public void shouldAddNewMeal() throws IOException {
        List<Meal> mealsBeforeInsert = mealManagerImp.getAllMeals();

        int countBeforeInsert = 0;
        for (Object i : mealsBeforeInsert) {
            countBeforeInsert++;
        }

        mealManagerImp.addMeal(testMeal1);

        List<Meal> mealsAfterInsert = mealManagerImp.getAllMeals();

        int countAfterInsert = 0;
        for (Object i : mealsAfterInsert) {
            countAfterInsert++;
        }


        System.out.println(countBeforeInsert);
        System.out.println(countAfterInsert);

        Assert.assertEquals(countBeforeInsert++, countAfterInsert);
    }
}
