package be.pxl.itproject.kbcfoodandgo.services.interfaces;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.Menu;

import java.util.List;
import java.util.Optional;

public interface MenuManager {
    Iterable<Menu> getAllMenus();
    Optional<Menu> getMenuById(long id);
    Menu addMenu(Menu menu);
    boolean deleteMenu(long id);
    List<Meal> updateMenu(long id, List<Meal> meals);
    Optional<Menu> getLastMenu();
    Optional<Menu> getMenuByName(String day);
}
