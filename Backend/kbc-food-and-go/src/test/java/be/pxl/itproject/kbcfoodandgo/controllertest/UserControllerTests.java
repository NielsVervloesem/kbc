package be.pxl.itproject.kbcfoodandgo.controllertest;

import be.pxl.itproject.kbcfoodandgo.controllers.UserController;
import be.pxl.itproject.kbcfoodandgo.models.dto.UserDTO;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.MealHistory;
import be.pxl.itproject.kbcfoodandgo.models.entities.Role;
import be.pxl.itproject.kbcfoodandgo.models.entities.User;
import be.pxl.itproject.kbcfoodandgo.services.UserManagerImp;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.DataService;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import java.util.ArrayList;
import java.util.List;

import static org.apache.tomcat.jni.SSL.setPassword;
import static org.mockito.Mockito.*;
import static org.springframework.http.MediaType.APPLICATION_JSON_UTF8;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.post;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

@RunWith(SpringRunner.class)
@WebMvcTest(UserController.class)
public class UserControllerTests {
    @Autowired
    private MockMvc mockMvc;

    @MockBean
    private UserManagerImp userManager;
    @MockBean
    private DataService dataService;

    private UserDTO testUserDTO;
    private User testUser;

    private List<User> returnUserList;
    private List<MealHistory> mealHistoryList;
    private User testUser1;


    @Before
    public void setUp() {
        testUser = new User("test@test.com", "$2a$04$u0RMl3DG9NQ0w2XKDkEWk.OF4r7ZKtQMdiM86ac6UfyAdSvg3Nf0m", Role.CUSTOMER);
        testUser.setId(1L);
        testUserDTO = new UserDTO(testUser.getEmail(), "test");
        testUser1 = new User()
        {{
            setId(1L);
            setEmail("testUser1");
            setSaldo(100.00);
            setPasswordHash("testPassword");
            MealHistory mealHistory1 = new MealHistory(){{
                setId(1L);
                setMealList(makeMealList());
            }};
            mealHistoryList = new ArrayList<>();
            mealHistoryList.add(mealHistory1);
            setMealHistory(mealHistoryList);
        }};
        returnUserList = new ArrayList<>();
        returnUserList.add(testUser1);
    }

    @Test
    public void loginShouldReturnOkWhenSuccessful() throws Exception {
        when(userManager.getUserByEmail(anyString())).thenReturn(java.util.Optional.ofNullable(testUser));

        mockMvc
                .perform(post("/api/user/login")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(asJsonString(testUserDTO))
                        .characterEncoding("utf-8"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.id").exists())
                .andExpect(jsonPath("email").value("test@test.com"));


        verify(userManager, times(1)).getUserByEmail(anyString());
        verifyNoMoreInteractions(userManager);
    }

    @Test
    public void loginShouldReturnBadRequestWhenPasswordIsWrong() throws Exception {
        when(userManager.getUserByEmail(anyString())).thenReturn(java.util.Optional.ofNullable(testUser));

        testUserDTO.setPassword("Wrong Password");

        mockMvc
                .perform(post("/api/user/login")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(asJsonString(testUserDTO))
                        .characterEncoding("utf-8"))
                .andExpect(status().isBadRequest());

        verify(userManager, times(1)).getUserByEmail(anyString());
        verifyNoMoreInteractions(userManager);
    }

    @Test
    public void loginShouldReturnNotFoundWhenEmailIsNotFound() throws Exception {
        when(userManager.getUserByEmail(anyString())).thenReturn(java.util.Optional.empty());

        mockMvc
                .perform(post("/api/user/login")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(asJsonString(testUserDTO))
                        .characterEncoding("utf-8"))
                .andExpect(status().isNotFound());

        verify(userManager, times(1)).getUserByEmail(anyString());
        verifyNoMoreInteractions(userManager);
    }

    @Test
    public void createShouldReturnCreatedWhenUserIsCreated() throws Exception {
        when(userManager.createUser(any(User.class))).thenReturn(testUser);
        when(dataService.saveImage((any(String.class)), anyString())).thenReturn("test");

        testUserDTO.setImageBase64("1");

        mockMvc
                .perform(post("/api/user/create")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(asJsonString(testUserDTO))
                        .characterEncoding("utf-8"))
                .andExpect(status().isCreated());

        verify(userManager, times(1)).createUser(any(User.class));
        verifyNoMoreInteractions(userManager);
    }

    @Test
    public void getAllUsersReturnsOk() throws Exception {
        when(userManager.getAllUsers()).thenReturn(returnUserList);
        mockMvc
                .perform(get("/api/user/"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(APPLICATION_JSON_UTF8))
                .andExpect(jsonPath("[0].email").value("testUser1"))
                .andExpect(jsonPath("[0].saldo").value(100))
                .andExpect(jsonPath("[0].passwordHash").value("testPassword"))
                .andExpect((jsonPath("[0].mealHistory.[0].id").value(1)));

        verify(userManager, times(1)).getAllUsers();
        verifyNoMoreInteractions(userManager);
    }

    @Test
    public void getAllUsersWhenNoUsersArePresentReturnsNotFound() throws Exception {
        mockMvc
                .perform(get("/api/user/"))
                .andExpect(status().isNotFound());

        verify(userManager, times(1)).getAllUsers();
        verifyNoMoreInteractions(userManager);
    }

    private List<Meal> makeMealList() {
        List<Meal> newMealList = new ArrayList<>();
        newMealList.add(new Meal(6,"Croque Monsieur", "Krokante sneetjes brood, smeuïge gesmolten kaas en een plakje ham.", 10.20));
        newMealList.add(new Meal(7,"Flat Angus Beef Burger", "Beefburger met cheddar, bacon, ijsbergsla, tomaat & home made burger relish.", 11.75));
        return newMealList;
    }

    public static String asJsonString(final Object obj) {
        try {
            return new ObjectMapper().writeValueAsString(obj);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}
