package be.pxl.itproject.kbcfoodandgo.controllertest;


import be.pxl.itproject.kbcfoodandgo.controllers.MealController;
import be.pxl.itproject.kbcfoodandgo.controllers.MealHistoryController;
import be.pxl.itproject.kbcfoodandgo.models.dto.ChartPointDTO;
import be.pxl.itproject.kbcfoodandgo.services.MealHistoryManagerImp;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import java.util.ArrayList;
import java.util.List;

import static org.mockito.Mockito.*;
import static org.springframework.http.MediaType.APPLICATION_JSON_UTF8;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@RunWith(SpringRunner.class)
@WebMvcTest(MealHistoryController.class)
public class MealHistoryControllerTests {
    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private MealHistoryManagerImp mealHistoryManagerImp;

    private List<ChartPointDTO> chartPoints;

    @Before
    public void setUp(){
        chartPoints = new ArrayList<>();
        ChartPointDTO chartPoint = new ChartPointDTO("Test", 0.0);
        chartPoints.add(chartPoint);

        for(int i = 0; i < 5; i++) {
            chartPoints.add(new ChartPointDTO());
        }
    }

    @Test
    public void getAllMealsFromTodayShouldReturnOkWhenChartPointsAreSendByManager() throws Exception {
        when(mealHistoryManagerImp.getAllMealsFromToday()).thenReturn(chartPoints);
        mockMvc
                .perform(get("/api/mealHistory/today"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("[0].label").value("Test"))
                .andExpect(jsonPath("[0].value").value(0.0));

        verify(mealHistoryManagerImp, times(1)).getAllMealsFromToday();
        verifyNoMoreInteractions(mealHistoryManagerImp);
    }

    @Test
    public void getProfitsFromLastFiveDaysShouldReturnOkWhenChartPointsAreSendByManager() throws Exception {
        when(mealHistoryManagerImp.getProfitsFromLastFiveDays()).thenReturn(chartPoints);
        mockMvc
                .perform(get("/api/mealHistory/profits/five"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("[0].label").value("Test"))
                .andExpect(jsonPath("[0].value").value(0.0));

        verify(mealHistoryManagerImp, times(1)).getProfitsFromLastFiveDays();
        verifyNoMoreInteractions(mealHistoryManagerImp);
    }

    @Test
    public void getFavoriteMealsShouldReturnOkWhenManagerReturnsChartPoints() throws Exception {
        when(mealHistoryManagerImp.getFavoriteMeals()).thenReturn(chartPoints);
        mockMvc
                .perform(get("/api/mealHistory/favorite"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("[0].label").value("Test"))
                .andExpect(jsonPath("[0].value").value(0.0));

        verify(mealHistoryManagerImp, times(1)).getFavoriteMeals();
        verifyNoMoreInteractions(mealHistoryManagerImp);
    }
}
