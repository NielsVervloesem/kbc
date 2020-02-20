package be.pxl.itproject.kbcfoodandgo.managertest;

import be.pxl.itproject.kbcfoodandgo.models.dto.ChartPointDTO;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.MealHistory;
import be.pxl.itproject.kbcfoodandgo.models.entities.User;
import be.pxl.itproject.kbcfoodandgo.repositories.MealHistoryRepository;
import be.pxl.itproject.kbcfoodandgo.repositories.MealRepository;
import be.pxl.itproject.kbcfoodandgo.services.MealHistoryManagerImp;
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
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

import static org.mockito.Mockito.when;

@RunWith(MockitoJUnitRunner.class)
@SpringBootTest
public class MealHistoryManagerImpTest {
    @InjectMocks
    private MealHistoryManagerImp mealHistoryManagerImp;

    @Mock
    private MealHistoryRepository mealHistoryRepoMock;

    private List<MealHistory> mealHistories;
    private Meal meal;

    @Before
    public void setUp() throws ParseException {
        mealHistories = new ArrayList<>();
        SimpleDateFormat formatter = new SimpleDateFormat("dd/MM/yyyy");

        List<Meal> meals = new ArrayList<>();
        meal = new Meal("Test", "", 0.0, "");
        meals.add(meal);

        Calendar cal = Calendar.getInstance();
        MealHistory mealHistory1= new MealHistory(meals, new User(), formatter.parse(formatter.format(cal.getTime())), 100.0);

        cal.add(Calendar.DATE, -1);
        MealHistory mealHistory2 = new MealHistory(meals, new User(), formatter.parse(formatter.format(cal.getTime())), 100.0);

        mealHistories.add(mealHistory1);
        mealHistories.add(mealHistory2);
    }

    @Test
    public void getAllMealsFromTodayShouldReturnListFromAllMealsFromToday() throws ParseException {
        when(mealHistoryRepoMock.findAll()).thenReturn(mealHistories);
        List<ChartPointDTO> returnedChartPoints = mealHistoryManagerImp.getAllMealsFromToday();

        Assert.assertNotNull(returnedChartPoints);
        Assert.assertEquals(1, returnedChartPoints.size());
        Assert.assertEquals(returnedChartPoints.get(0).getLabel(), meal.getName());
        Assert.assertEquals(1.0, returnedChartPoints.get(0).getValue(),  0);
    }

    @Test
    public void getProfitsFromLastFiveDaysShouldReturnTheProfits() {
        SimpleDateFormat formatter = new SimpleDateFormat("dd/MM");

        when(mealHistoryRepoMock.findAll()).thenReturn(mealHistories);
        List<ChartPointDTO> returnedChartPoints = mealHistoryManagerImp.getProfitsFromLastFiveDays();

        Assert.assertNotNull(returnedChartPoints);
        Assert.assertEquals(5, returnedChartPoints.size());
        Assert.assertEquals(100.0, returnedChartPoints.get(4).getValue(),  0);
    }

    @Test
    public void getFavoriteMealsShouldReturnListOfChartPoints() {
        when(mealHistoryRepoMock.findAll()).thenReturn(mealHistories);
        List<ChartPointDTO> returnedChartPoints = mealHistoryManagerImp.getFavoriteMeals();

        Assert.assertNotNull(returnedChartPoints);
        Assert.assertEquals(1, returnedChartPoints.size());
        Assert.assertEquals(returnedChartPoints.get(0).getLabel(), meal.getName());
        Assert.assertEquals(2.0, returnedChartPoints.get(0).getValue(),  0);
    }
}

