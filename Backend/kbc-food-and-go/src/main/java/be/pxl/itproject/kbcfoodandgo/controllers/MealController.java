package be.pxl.itproject.kbcfoodandgo.controllers;

import be.pxl.itproject.kbcfoodandgo.aop.annotations.LogAction;
import be.pxl.itproject.kbcfoodandgo.models.dto.MealDTO;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.DataService;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.MealManager;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("api/meal")
public class MealController {

    private final MealManager mealManager;
    private final DataService dataService;

    public static final String DEFAULT_IMAGE = "defaultImage.jpg";

    public MealController(MealManager mealManager, DataService dataService) {
        this.mealManager = mealManager;
        this.dataService = dataService;
    }

    @GetMapping(value = "/", produces = "application/json;charset=utf-8")
    public ResponseEntity<List<MealDTO>> getAllMeals() {
        List<Meal> allMeals = mealManager.getAllMeals();
        return changeMealListToMealDTOList(allMeals);
    }


    @PostMapping(value = "/list", produces = "application/json;charset=utf-8")
    public ResponseEntity<List<MealDTO>> getMealByIdList(@RequestBody Iterable<Long> idList) {
        List<Meal> allMeals = mealManager.getMealsByIds(idList);
        return changeMealListToMealDTOList(allMeals);
    }

    @GetMapping(value = "/search/{text}", produces = "application/json;charset=utf-8")
    public ResponseEntity<List<MealDTO>> getMealsByText(@PathVariable String text)  {
        List<Meal> allMeals = mealManager.getMealsByText(text);
        return changeMealListToMealDTOList(allMeals);
    }

    @GetMapping(value = "/{id}", produces = "application/json;charset=utf-8")
    public ResponseEntity<MealDTO> getMealByIdList(@PathVariable long id) {
        Optional<Meal> meal = mealManager.getMealById(id);
        if (meal.isPresent()) {
            MealDTO mealDTO = new MealDTO(meal.get());
            return new ResponseEntity<>(mealDTO, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @LogAction
    @PostMapping(value = "/", produces = "application/json;charset=utf-8")
    public ResponseEntity<Meal> addMeal(@RequestBody MealDTO mealDTO) throws IOException {
        String imageName;
        if (!mealDTO.getImageBase64().equals(DEFAULT_IMAGE)) {
            imageName = dataService.saveImage(mealDTO.getImageBase64(), mealDTO.getName());
        } else {
            imageName = DEFAULT_IMAGE;
        }
        mealDTO.setImageUrl("https://s3.eu-central-1.amazonaws.com/kbc-cdn/" + imageName);
        Meal savedMeal = mealManager.addMeal(new Meal(mealDTO));
        return new ResponseEntity<>(savedMeal, HttpStatus.CREATED);
    }

    @LogAction
    @DeleteMapping(value = "/{id}", produces = "application/json;charset=utf-8")
    public ResponseEntity<Meal> deleteMeal(@PathVariable long id) {
        Optional<Meal> meal = mealManager.getMealById(id);
        if (meal.isPresent()) {
            int pos = meal.get().getImageUrl().lastIndexOf('/') + 1;
            String imageName = meal.get().getImageUrl().substring(pos);
            if (!imageName.equals(DEFAULT_IMAGE)) {
                dataService.deleteImage(imageName);
            }
        }
        if (mealManager.deleteMeal(id)) {
            return new ResponseEntity<>(HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @PutMapping(value = "/{id}", produces = "application/json;charset=utf-8")
    public ResponseEntity<Meal> updateMeal(@RequestBody MealDTO mealDTO, @PathVariable Long id) {
        Meal meal = new Meal(mealDTO);
        Meal returnMeal = mealManager.updateMeal(meal);
        return new ResponseEntity<>(returnMeal, HttpStatus.OK);
    }


    private ResponseEntity<List<MealDTO>> changeMealListToMealDTOList(List<Meal> allMeals) {
        List<MealDTO> allMealsDTO = new ArrayList<>();

        for (Meal meal : allMeals) {
            MealDTO mealDTO = new MealDTO(meal);
            allMealsDTO.add(mealDTO);
        }
        if ((allMealsDTO).isEmpty()) {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(allMealsDTO, HttpStatus.OK);
    }
}


