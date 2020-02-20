package be.pxl.itproject.kbcfoodandgo.services.interfaces;

import be.pxl.itproject.kbcfoodandgo.models.dto.ChartPointDTO;

import java.text.ParseException;
import java.util.List;

public interface MealHistoryManager {
    List<ChartPointDTO> getAllMealsFromToday() throws ParseException;
    List<ChartPointDTO> getProfitsFromLastFiveDays();
    List<ChartPointDTO> getFavoriteMeals();
}
