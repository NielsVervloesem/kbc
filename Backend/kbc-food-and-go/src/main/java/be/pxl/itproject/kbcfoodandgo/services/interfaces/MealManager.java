package be.pxl.itproject.kbcfoodandgo.services.interfaces;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import java.util.List;
import java.util.Optional;

public interface MealManager {
    List<Meal> getAllMeals();
    Optional<Meal> getMealById(long id) ;
    Meal addMeal(Meal meal);
    boolean deleteMeal(long id);
    Meal updateMeal(Meal meal);
    List<Meal> getMealsByText(String text);
    List<Meal> getMealsByIds(Iterable<Long> idList);
}
