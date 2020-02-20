package be.pxl.itproject.kbcfoodandgo.services;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.repositories.MealRepository;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.MealManager;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class MealManagerImp implements MealManager {

    private final MealRepository mealRepository;

    public MealManagerImp(MealRepository mealRepository) {
        this.mealRepository = mealRepository;
    }

    @Override
    public List<Meal> getAllMeals() {
        return (List<Meal>) mealRepository.findAll();
    }

    @Override
    public Optional<Meal> getMealById(long id) {
        return mealRepository.findById(id);
    }

    @Override
    public Meal addMeal(Meal meal) {
        return mealRepository.save(meal);
    }

    @Override
    public boolean deleteMeal(long id) {
        if (mealRepository.findById(id).isPresent()) {
            mealRepository.deleteById(id);
            return true;
        } else {
            return false;
        }
    }

    @Override
    public Meal updateMeal(Meal meal) {
        return mealRepository.save(meal);
    }

    @Override
    public List<Meal> getMealsByText(String text) {
        return mealRepository.findByNameContainingIgnoreCase(text);
    }
    public List<Meal> getMealsByIds(Iterable<Long> idList) {
        return (List<Meal>) mealRepository.findAllById(idList);
    }
}