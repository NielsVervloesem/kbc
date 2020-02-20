package be.pxl.itproject.kbcfoodandgo.services;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.Menu;
import be.pxl.itproject.kbcfoodandgo.repositories.MenuRepository;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.PusherService;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.MenuManager;
import org.springframework.stereotype.Service;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class MenuManagerImp implements MenuManager {
    private final MenuRepository menuRepository;
    private final PusherService pusherService;

    public MenuManagerImp(MenuRepository menuRepository, PusherService pusherService) {
        this.menuRepository = menuRepository;
        this.pusherService = pusherService;
    }

    @Override
    public Iterable<Menu> getAllMenus() {
        return menuRepository.findAll();
    }

    @Override
    public Optional<Menu> getMenuById(long id) {
        return menuRepository.findById(id);
    }

    @Override
    public Menu addMenu(Menu menu) {
        pusherService.onCreateMenu();
        return menuRepository.save(menu);
    }

    @Override
    public boolean deleteMenu(long id) {
        if (menuRepository.findById(id).isPresent()) {
            menuRepository.deleteById(id);
            return true;
        } else {
            return false;
        }
    }

    @Override
    public List<Meal> updateMenu(long id, List<Meal> meals) {
        Optional<Menu> menu = menuRepository.findById(id);

        if (menu.isPresent()) {
            menu.get().setMeals(meals);
            pusherService.onCreateMenu();
            return menuRepository.save(menu.get()).getMeals();
        }
        return new ArrayList<>();
    }

    @Override
    public Optional<Menu> getMenuByName(String day) {
        return menuRepository.findByMenuNameIgnoreCase(day);
    }

    @Override
    public Optional<Menu> getLastMenu() {
        return menuRepository.findFirstByOrderByIdDesc();
    }
}
