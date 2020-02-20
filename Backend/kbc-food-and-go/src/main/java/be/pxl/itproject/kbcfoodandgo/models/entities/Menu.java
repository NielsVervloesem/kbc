package be.pxl.itproject.kbcfoodandgo.models.entities;
import be.pxl.itproject.kbcfoodandgo.models.dto.MealDTO;
import be.pxl.itproject.kbcfoodandgo.models.dto.MenuDTO;
import javax.persistence.*;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "Menus")
public class Menu {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    private String menuName;

    @ManyToMany
    @JoinTable(
            name = "menu_meal",
            joinColumns = @JoinColumn(name = "menu_id"),
            inverseJoinColumns = @JoinColumn(name = "meal_id"))
    private List<Meal> meals;

    public Menu(MenuDTO menuDTO){
        this.id = menuDTO.getId();
        List<Meal> mealList= new ArrayList<>();
        for (MealDTO meal: menuDTO.getMeals() ) {
            mealList.add(new Meal(meal));
        }
        this.setMeals(mealList);
    }

    public Menu(){ }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public List<Meal> getMeals() {
        return meals;
    }

    public void setMeals(List<Meal> meals) {
        this.meals = meals;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getMenuName() {
        return menuName;
    }

    public void setMenuName(String menuName) {
        this.menuName = menuName;
    }

    public void setMealsDTO(List<MealDTO> meals) {
        for(MealDTO mealDto: meals){
            Meal meal = new Meal(mealDto);
            this.meals.add(meal);
        }
    }
}
