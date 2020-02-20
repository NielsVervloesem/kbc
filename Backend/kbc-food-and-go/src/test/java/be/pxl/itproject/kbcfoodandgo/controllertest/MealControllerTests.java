package be.pxl.itproject.kbcfoodandgo.controllertest;

import be.pxl.itproject.kbcfoodandgo.controllers.MealController;
import be.pxl.itproject.kbcfoodandgo.models.dto.MealDTO;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.services.DataServiceImp;
import be.pxl.itproject.kbcfoodandgo.services.MealManagerImp;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.result.MockMvcResultMatchers;

import java.util.ArrayList;
import java.util.List;

import static org.mockito.Mockito.*;
import static org.springframework.http.MediaType.APPLICATION_JSON_UTF8;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@RunWith(SpringRunner.class)
@WebMvcTest(MealController.class)
public class MealControllerTests {
    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private ObjectMapper objectMapper;

    @MockBean
    private MealManagerImp mealManagerImp;

    @MockBean
    private DataServiceImp dataServiceImp;

    private MealDTO testMealDTO;

    private List<Meal> returnMealList;
    private Meal testMeal;


    @Before
    public void setUp(){
        testMealDTO = new MealDTO()
        {{
            setName("Hotdog");
            setPrice(5.5);
            setShortDescription("A nice hotdog");
            setImageBase64("defaultImage.jpg");
        }};

        testMeal = new Meal() {
            {
                setId(1);
                setName("Hotdog");
                setPrice(5.5);
                setShortDescription("A nice hotdog");
            }
        };

        returnMealList = new ArrayList<>();
        returnMealList.add(testMeal);
    }

    @Test
    public void getAllMealsReturnsOk() throws Exception {
        when(mealManagerImp.getAllMeals()).thenReturn(returnMealList);

        mockMvc
                .perform(get("/api/meal/"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("[0].name").value("Hotdog"))
                .andExpect(jsonPath("[0].price").value(5.5))
                .andExpect(jsonPath("[0].shortDescription").value("A nice hotdog"));

        verify(mealManagerImp, times(1)).getAllMeals();
        verifyNoMoreInteractions(mealManagerImp);
    }

    @Test
    public void getAllMealsWhenNoMealsArePresentReturnsNotFound() throws Exception {
        mockMvc
                .perform(get("/api/meal/"))
                .andExpect(status().isNotFound());

        verify(mealManagerImp, times(1)).getAllMeals();
        verifyNoMoreInteractions(mealManagerImp);
    }

    @Test
    public void getMealByIdReturnsOk() throws Exception {
        when(mealManagerImp.getMealById(1)).thenReturn(java.util.Optional.ofNullable(testMeal));
        mockMvc
                .perform(get("/api/meal/{id}", 1))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("$.name").value("Hotdog"))
                .andExpect(jsonPath("$.price").value(5.5))
                .andExpect(jsonPath("$.shortDescription").value("A nice hotdog"));


        verify(mealManagerImp, times(1)).getMealById(1);
        verifyNoMoreInteractions(mealManagerImp);
    }

    @Test
    public void getMealByWhenNoMealsArePresentReturnsNotFound() throws Exception {
        mockMvc
                .perform(get("/api/meal/{id}", 1))
                .andExpect(status().isNotFound());



        verify(mealManagerImp, times(1)).getMealById(1);
        verifyNoMoreInteractions(mealManagerImp);
    }

    @Test
    public void updateMealByIdShouldUpdateTheMealAndReturnOk() throws Exception {
        when(mealManagerImp.updateMeal(Mockito.any(Meal.class))).thenReturn(testMeal);

        mockMvc.perform(put(String.format("/api/meal/%d", testMeal.getId()))
                .contentType(APPLICATION_JSON_UTF8)
                .content(objectMapper.writeValueAsString(testMealDTO))
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.id").value(1))
                .andExpect(jsonPath("$.name").value("Hotdog"))
                .andExpect(jsonPath("$.price").value(5.5))
                .andExpect(jsonPath("$.shortDescription").value("A nice hotdog"));

        verify(mealManagerImp, times(1)).updateMeal(Mockito.any(Meal.class));
        verifyNoMoreInteractions(mealManagerImp);

    }

    @Test
    public void searchMealByTextShouldReturnOk() throws Exception {
        when(mealManagerImp.getMealsByText(Mockito.any(String.class))).thenReturn(returnMealList);

        mockMvc.perform(get(String.format("/api/meal/search/%s", testMeal.getName().substring(4)))
                .contentType(APPLICATION_JSON_UTF8)
                .content(objectMapper.writeValueAsString(testMealDTO))
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("[0].id").value(1))
                .andExpect(jsonPath("[0].name").value("Hotdog"))
                .andExpect(jsonPath("[0].price").value(5.5))
                .andExpect(jsonPath("[0].shortDescription").value("A nice hotdog"));

        verify(mealManagerImp, times(1)).getMealsByText(Mockito.any(String.class));
        verifyNoMoreInteractions(mealManagerImp);

    }


    @Test
    public void addMealShouldCreateMealAndReturnCreatedStatus() throws Exception {
        when(mealManagerImp.addMeal(Mockito.any(Meal.class))).thenReturn(testMeal);

        mockMvc
                .perform(post("/api/meal/")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(asJsonString(testMealDTO))
                        .characterEncoding("utf-8"))
                .andExpect(status().isCreated())
                .andExpect(MockMvcResultMatchers.jsonPath("$.id").exists());

        verify(dataServiceImp, times(0)).saveImage(Mockito.any(),Mockito.any());
    }

    @Test
    public void addIncompleteMealShouldReturnBadRequest() throws Exception {

        when(mealManagerImp.addMeal(Mockito.any(Meal.class))).thenReturn(testMeal);

        mockMvc
                .perform(post("/api/meal/")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\\\"id\\\":1,\\\"shortDescription\\\":\\\"A nice meal\\\",\\\"price\\\":0.0}")
                        .characterEncoding("utf-8"))
                .andExpect(status().isBadRequest());
    }

    @Test
    public void deleteMealEndpointShouldReturnOK() throws Exception {
        when(mealManagerImp.deleteMeal(1)).thenReturn(true);

        mockMvc.perform(
                delete("/api/meal/{id}", "1"))
                .andExpect(status().isOk());
    }

    @Test
    public void deleteMealEndpointShouldReturnNotFoundWhenTheMealIDIsntFound() throws Exception {
        when(mealManagerImp.deleteMeal(1)).thenReturn(false);

        mockMvc.perform(
                delete("/api/meal/{id}", "1"))
                .andExpect(status().isNotFound());
    }

    public static String asJsonString(final Object obj) {
        try {
            return new ObjectMapper().writeValueAsString(obj);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}