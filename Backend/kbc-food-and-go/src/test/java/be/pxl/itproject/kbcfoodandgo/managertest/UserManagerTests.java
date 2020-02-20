package be.pxl.itproject.kbcfoodandgo.managertest;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.MealHistory;
import be.pxl.itproject.kbcfoodandgo.models.entities.Role;
import be.pxl.itproject.kbcfoodandgo.models.entities.User;
import be.pxl.itproject.kbcfoodandgo.repositories.UserRepository;
import be.pxl.itproject.kbcfoodandgo.services.UserManagerImp;
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

import static org.apache.tomcat.jni.SSL.setPassword;
import static org.mockito.Mockito.*;

@RunWith(MockitoJUnitRunner.class)
@SpringBootTest
public class UserManagerTests {

    @InjectMocks
    private UserManagerImp userManager;

    @Mock
    private UserRepository userRepositoryMock;

    private List<User> returnUserList;
    private User testUser;
    private User testUser1;
    private List<MealHistory> mealHistoryList;

    @Before
    public void setUp() {
        testUser = new User("Test@hotmail.com", "test", Role.CUSTOMER);
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
    public void getUserByIdShouldReturnUserWithCorrectId() {
        when(userManager.getUserById(anyLong())).thenReturn(Optional.ofNullable(testUser));

        User returnedUser = userManager.getUserById(1L).get();

        Assert.assertNotNull(returnedUser);
        Assert.assertEquals(testUser, returnedUser);
        verify(userRepositoryMock, times(1)).findById(anyLong());
        verifyNoMoreInteractions(userRepositoryMock);
    }

    @Test
    public void getAllUsersShouldReturnAllUsers() throws IOException {
        when(userRepositoryMock.findAll()).thenReturn(returnUserList);

        Iterable<User> realReturnUsers = userManager.getAllUsers();

        int counter = 0;

        for (User user : realReturnUsers) {
            counter++;
        }
        Assert.assertNotNull(realReturnUsers);
        Assert.assertEquals(counter, returnUserList.size());
        Mockito.verify(userRepositoryMock, Mockito.times(1)).findAll();
        Mockito.verifyNoMoreInteractions(userRepositoryMock);
    }

    @Test
    public void getUserByEmailShouldReturnUserWithCorrectEmail() {
        when(userManager.getUserByEmail(anyString())).thenReturn(Optional.ofNullable(testUser));

        User returnedUser = userManager.getUserByEmail("zefufazfEZDS").get();

        Assert.assertNotNull(returnedUser);
        Assert.assertEquals(testUser, returnedUser);
        verify(userRepositoryMock, times(1)).findByEmail(anyString());
        verifyNoMoreInteractions(userRepositoryMock);
    }

    @Test
    public void createUserShouldReturnCreatedUser() {
        when(userManager.createUser(any(User.class))).thenReturn(testUser);

        User returnedUser = userManager.createUser(testUser);

        Assert.assertNotNull(returnedUser);
        Assert.assertEquals(testUser, returnedUser);
        verify(userRepositoryMock, times(1)).save(any(User.class));
        verifyNoMoreInteractions(userRepositoryMock);
    }

    private List<Meal> makeMealList() {
        List<Meal> newMealList = new ArrayList<>();
        newMealList.add(new Meal(6,"Croque Monsieur", "Krokante sneetjes brood, smeuïge gesmolten kaas en een plakje ham.", 10.20));
        newMealList.add(new Meal(7,"Flat Angus Beef Burger", "Beefburger met cheddar, bacon, ijsbergsla, tomaat & home made burger relish.", 11.75));
        return newMealList;
    }
}
