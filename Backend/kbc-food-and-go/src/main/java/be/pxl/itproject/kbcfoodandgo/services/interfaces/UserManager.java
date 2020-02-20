package be.pxl.itproject.kbcfoodandgo.services.interfaces;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.User;

import java.util.List;
import java.util.Optional;

public interface UserManager {
    Optional<User> getUserById(long id);
    Optional<User> getUserByEmail(String email);
    User createUser(User user);
    User updateMealHistory(long id, List<Meal> meal);
    Iterable<User> getAllUsers();
    List<User> getUsersByIdList(Iterable<Long> idList);
}
