package be.pxl.itproject.kbcfoodandgo.controllers;

import be.pxl.itproject.kbcfoodandgo.models.dto.MealDTO;
import be.pxl.itproject.kbcfoodandgo.models.dto.MenuDTO;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.Menu;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.MenuManager;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("api/menu")
public class MenuController {
    private MenuManager menuManager;

    @Autowired
    public MenuController(MenuManager menuManager){
        this.menuManager = menuManager;
    }

    @GetMapping(value = "/", produces = "application/json;charset=utf-8")
    public ResponseEntity<Iterable<Menu>> getAllMenus(){
        Iterable<Menu> allMenus = menuManager.getAllMenus();

        if (((List<Menu>) allMenus).isEmpty()){
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(allMenus, HttpStatus.OK);
    }

    @GetMapping(value = "/name/{menuName}", produces = "application/json;charset=utf-8")
    public ResponseEntity<Optional<Menu>> getMenuByName(@PathVariable String menuName){
        Optional<Menu> dayMenu = menuManager.getMenuByName(menuName);
        if (dayMenu.isPresent()){
            return new ResponseEntity<>(dayMenu, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @GetMapping(value = "/{id}", produces = "application/json;charset=utf-8")
    public ResponseEntity<Optional<Menu>> getMenuById(@PathVariable long id){
        Optional<Menu> menu = menuManager.getMenuById(id);
        if (menu.isPresent()){
            return new ResponseEntity<>(menu, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @GetMapping(value = "/last", produces = "application/json;charset=utf-8")
    public ResponseEntity<MenuDTO> getLastMenu()  {
        Optional<Menu> menu = menuManager.getLastMenu();

        if (menu.isPresent()){
            MenuDTO menuDTO = new MenuDTO();
            menuDTO.setId(menu.get().getId());
            List<MealDTO> allMealsDTO = new ArrayList<>();

            for (Meal meal : menu.get().getMeals()) {
                MealDTO mealDTO = new MealDTO(meal);
                allMealsDTO.add(mealDTO);
            }

            menuDTO.setMeals(allMealsDTO);

            return new ResponseEntity<>(menuDTO, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @PostMapping(value = "/", produces = "application/json;charset=utf-8")
    public ResponseEntity<Menu> addMenu(@RequestBody MenuDTO menuDto){
        Menu savedMenu = menuManager.addMenu(new Menu(menuDto));
        return new ResponseEntity<>(savedMenu, HttpStatus.CREATED);
    }

    @DeleteMapping(value = "/{id}", produces = "application/json;charset=utf-8")
    public ResponseEntity<Menu> deleteMenu(@PathVariable long id){
        if(menuManager.deleteMenu(id)){
            return new ResponseEntity<>(HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @PutMapping(value = "/{id}", produces = "application/json;charset=utf-8")
    public ResponseEntity<List<Meal>> updateMeal(@RequestBody List<Meal> meals,@PathVariable Long id){
        List<Meal> returnMenu = menuManager.updateMenu(id, meals);
        return new ResponseEntity<>(returnMenu, HttpStatus.OK);
    }


}
