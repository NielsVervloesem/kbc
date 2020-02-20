package be.pxl.itproject.kbcfoodandgo.controllertest;

import be.pxl.itproject.kbcfoodandgo.controllers.MenuController;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.Menu;
import be.pxl.itproject.kbcfoodandgo.services.DataServiceImp;
import be.pxl.itproject.kbcfoodandgo.services.MenuManagerImp;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.hamcrest.Matchers;
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
@WebMvcTest(MenuController.class)
public class MenuControllerTests {
    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private ObjectMapper objectMapper;

    @MockBean
    private MenuManagerImp menuManagerImp;

    @MockBean
    private DataServiceImp dataServiceImp;


    private Menu testmenu1;

    private List<Menu> returnMenuList;
    private Menu incompleteMenu;

    private List<Meal> mealList;
    private Meal testmeal1;
    private Meal testmeal2;

    @Before
    public void setUp() {
        testmeal1 = new Meal("hotdog", "a nice hotdog", 5.5,"");
        testmeal2 = new Meal();
        testmeal2.setName("incomplete meal");
        mealList = new ArrayList<>();
        mealList.add(testmeal1);
        mealList.add(testmeal2);

        testmenu1 = new Menu() {{
            setId((long) 1);
            setMeals(mealList);
        }};

        incompleteMenu = new Menu() {{
            setId((long) 1);
        }};

        returnMenuList = new ArrayList<>();
        returnMenuList.add(testmenu1);
    }

    @Test
    public void getAllMenusReturnsOk() throws Exception {
        when(menuManagerImp.getAllMenus()).thenReturn(returnMenuList);
        mockMvc
                .perform(get("/api/menu/"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("[0].id").value(1))
                .andExpect(jsonPath("[0].meals").isArray())
                .andExpect(jsonPath("[0].meals", Matchers.hasSize(2)))
                .andExpect((jsonPath("[0].meals.[0].name").value("hotdog")))
                .andExpect((jsonPath("[0].meals.[1].name").value("incomplete meal")));


        verify(menuManagerImp, times(1)).getAllMenus();
        verifyNoMoreInteractions(menuManagerImp);
    }

    @Test
    public void getAllMenusWhenNoMenusArePresentReturnsNotFound() throws Exception {
        mockMvc
                .perform(get("/api/menu/"))
                .andExpect(status().isNotFound());

        verify(menuManagerImp, times(1)).getAllMenus();
        verifyNoMoreInteractions(menuManagerImp);
    }

    @Test
    public void getMenuByIdReturnsOk() throws Exception {
        when(menuManagerImp.getMenuById(1)).thenReturn(java.util.Optional.ofNullable(testmenu1));

        mockMvc
                .perform(get("/api/menu/{id}", 1))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("$.id").value(1))
                .andExpect(jsonPath("$.meals").isArray())
                .andExpect(jsonPath("$.meals", Matchers.hasSize(2)))
                .andExpect((jsonPath("$.meals.[0].name").value("hotdog")))
                .andExpect((jsonPath("$.meals.[1].name").value("incomplete meal")));

        verify(menuManagerImp, times(1)).getMenuById(1);
        verifyNoMoreInteractions(menuManagerImp);
    }

    @Test
    public void getMenuByIdWhenNoMealsArePresentReturnsNotFound() throws Exception {
        mockMvc
                .perform(get("/api/menu/{id}", 1))
                .andExpect(status().isNotFound());

        verify(menuManagerImp, times(1)).getMenuById(1);
        verifyNoMoreInteractions(menuManagerImp);
    }

    @Test
    public void updateMenuByIdShouldUpdateTheMenuAndReturnOk() throws Exception {
        when(menuManagerImp.updateMenu(Mockito.anyLong(), Mockito.anyList())).thenReturn(mealList);

        mockMvc.perform(put(String.format("/api/menu/%d", testmenu1.getId()))
                .contentType(APPLICATION_JSON_UTF8)
                .content(objectMapper.writeValueAsString(mealList))
                .accept(MediaType.APPLICATION_JSON))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.[0].id").value(0))
                .andExpect(jsonPath("$").isArray())
                .andExpect(jsonPath("$", Matchers.hasSize(2)))
                .andExpect((jsonPath("$.[0].name").value("hotdog")))
                .andExpect((jsonPath("$.[1].name").value("incomplete meal")));

        verify(menuManagerImp, times(1)).updateMenu(Mockito.anyLong(), Mockito.anyList());
        verifyNoMoreInteractions(menuManagerImp);
    }

    @Test
    public void addMenuShouldCreateMenuAndReturnCreatedStatus() throws Exception {

        when(menuManagerImp.addMenu(Mockito.any(Menu.class))).thenReturn(testmenu1);

        mockMvc
                .perform(post("/api/menu/")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(asJsonString(testmenu1))
                        .characterEncoding("utf-8"))
                .andExpect(status().isCreated())
                .andExpect(MockMvcResultMatchers.jsonPath("$.id").exists());
    }

    @Test
    public void addIncompleteMenuShouldReturnBadRequest() throws Exception {

        when(menuManagerImp.addMenu(Mockito.any(Menu.class))).thenReturn(testmenu1);

        mockMvc
                .perform(post("/api/menu/")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content("{\\\"id}")
                        .characterEncoding("utf-8"))
                .andExpect(status().isBadRequest());
    }

    @Test
    public void deleteMenuEndpointShouldReturnOK() throws Exception {
        when(menuManagerImp.deleteMenu(1)).thenReturn(true);

        mockMvc.perform(
                delete("/api/menu/{id}", "1"))
                .andExpect(status().isOk());
    }

    @Test
    public void deleteMenuEndpointShouldReturnNotFoundWhenTheMenuIdIsNotFound() throws Exception {
        when(menuManagerImp.deleteMenu(1)).thenReturn(false);

        mockMvc.perform(
                delete("/api/menu/{id}", "1"))
                .andExpect(status().isNotFound());
    }

    @Test
    public void getLastMenuWhenNoMenussArePresentReturnsNotFound() throws Exception {
        mockMvc
                .perform(get("/api/menu/last"))
                .andExpect(status().isNotFound());

        verify(menuManagerImp, times(1)).getLastMenu();
        verifyNoMoreInteractions(menuManagerImp);
    }

    @Test
    public void getLastMenuReturnsOk() throws Exception {
        when(menuManagerImp.getLastMenu()).thenReturn(java.util.Optional.ofNullable(testmenu1));

        mockMvc
                .perform(get("/api/menu/last", 1))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("$.id").value(testmenu1.getId()))
                .andExpect(jsonPath("$.meals").isArray())
                .andExpect(jsonPath("$.meals", Matchers.hasSize(2)))
                .andExpect((jsonPath("$.meals.[0].name").value("hotdog")))
                .andExpect((jsonPath("$.meals.[1].name").value("incomplete meal")));



        verify(menuManagerImp, times(1)).getLastMenu();
        verifyNoMoreInteractions(menuManagerImp);
    }

    public static String asJsonString(final Object obj) {
        try {
            return new ObjectMapper().writeValueAsString(obj);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}