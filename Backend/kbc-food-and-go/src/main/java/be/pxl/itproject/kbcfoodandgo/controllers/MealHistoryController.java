package be.pxl.itproject.kbcfoodandgo.controllers;

import be.pxl.itproject.kbcfoodandgo.models.dto.ChartPointDTO;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.MealHistoryManager;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.text.ParseException;
import java.util.*;

@RestController
@RequestMapping("api/mealHistory")
public class MealHistoryController {
    private final MealHistoryManager mealHistoryManager;

    @Autowired
    public  MealHistoryController(MealHistoryManager mealHistoryManager) {
        this.mealHistoryManager = mealHistoryManager;
    }

    @GetMapping(value = "/today", produces = "application/json")
    public ResponseEntity<List<ChartPointDTO>> getAllMealsFromToday() throws ParseException {
        List<ChartPointDTO> chartPoints = mealHistoryManager.getAllMealsFromToday();
        return new ResponseEntity<>(chartPoints, HttpStatus.OK);
    }

    @GetMapping(value = "profits/five", produces = "application/json")
    public ResponseEntity<List<ChartPointDTO>> getProfitsFromLastFiveDays() {
        List<ChartPointDTO> chartPoints  = mealHistoryManager.getProfitsFromLastFiveDays();
        return new ResponseEntity<>(chartPoints, HttpStatus.OK);
    }

    @GetMapping(value = "/favorite", produces = "application/json")
    public ResponseEntity<List<ChartPointDTO>> getFavoriteMeals() {
        List<ChartPointDTO> chartPoints = mealHistoryManager.getFavoriteMeals();
        return new ResponseEntity<>(chartPoints, HttpStatus.OK);
    }
}
