package be.pxl.itproject.kbcfoodandgo.services;

import be.pxl.itproject.kbcfoodandgo.models.dto.ChartPointDTO;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.MealHistory;
import be.pxl.itproject.kbcfoodandgo.repositories.MealHistoryRepository;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.MealHistoryManager;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

@Service
public class MealHistoryManagerImp implements MealHistoryManager {
    private final MealHistoryRepository mealHistoryRepository;

    @Autowired
    public MealHistoryManagerImp(MealHistoryRepository mealHistoryRepository) {
        this.mealHistoryRepository = mealHistoryRepository;
    }

    @Override
    public List<ChartPointDTO> getAllMealsFromToday() throws ParseException {
        SimpleDateFormat formatter = new SimpleDateFormat("dd/MM/yyyy");

        List<MealHistory> mealHistories = mealHistoryRepository.findAll();
        Date now = formatter.parse(formatter.format(new Date()));
        Map<String, Integer> map = new HashMap<>();

        for (MealHistory mealHistory : mealHistories) {
            Date mealHistoryDate = formatter.parse(formatter.format(mealHistory.getDate()));
            if (mealHistoryDate.toString().equals(now.toString())) {
                for (Meal meal : mealHistory.getMealList()) {
                    if (!map.containsKey(meal.getName())) {
                        map.put(meal.getName(), 1);
                    } else {
                        map.replace(meal.getName(), map.get(meal.getName()) + 1);
                    }
                }
            }
        }

        return getTopFiveFromMap(map, true);
    }

    @Override
    public List<ChartPointDTO> getProfitsFromLastFiveDays() {
        SimpleDateFormat formatter = new SimpleDateFormat("dd/MM", Locale.forLanguageTag("nl-BE"));


        List<MealHistory> mealHistories = mealHistoryRepository.findAll();
        final Calendar cal = Calendar.getInstance(Locale.forLanguageTag("nl-BE"));
        List<ChartPointDTO> chartPoints = new ArrayList<>();

        while (chartPoints.size() != 5) {
            if (!cal.getTime().toString().contains("Sun") && !cal.getTime().toString().contains("Sat")) {
                ChartPointDTO chartPoint = new ChartPointDTO(formatter.format(cal.getTime()), 0.0);
                chartPoints.add(chartPoint);
            }
             cal.add(Calendar.DATE, -1);
        }

        for (MealHistory mealHistory: mealHistories) {
            String date = formatter.format(mealHistory.getDate());
            for (ChartPointDTO chartPointDTO : chartPoints) {
                if (chartPointDTO.getLabel().equals(date)) {
                    chartPointDTO.setValue(chartPointDTO.getValue() + mealHistory.getTotalPrice());
                }
            }
        }

        Collections.reverse(chartPoints);
        return chartPoints;
    }

    @Override
    public List<ChartPointDTO> getFavoriteMeals() {
        List<MealHistory> mealHistories = mealHistoryRepository.findAll();
        Map<String, Integer> map = new HashMap<>();

        for (MealHistory mealHistory : mealHistories) {
            for (Meal meal : mealHistory.getMealList()) {
                if (!map.containsKey(meal.getName())) {
                    map.put(meal.getName(), 1);
                } else {
                    map.replace(meal.getName(), map.get(meal.getName()) + 1);
                }
            }
        }

        return getTopFiveFromMap(map, false);
    }

    private List<ChartPointDTO> getTopFiveFromMap(Map<String, Integer> map, boolean includeOthers) {
        List<ChartPointDTO> chartPoints = new ArrayList<>();
        
        int counter = Math.min(map.size(), 5);

        if (includeOthers && counter == 5) {
                counter = 4;
        }

        for (int i = 0; i < counter; i++) {
            String key = Collections.max(map.entrySet(), Comparator.comparingInt(Map.Entry::getValue)).getKey();
            int value = map.get(key);
            chartPoints.add(new ChartPointDTO(key, value));
            map.remove(key);
        }

        double othersValue = 0;
        if (includeOthers && counter == 4) {
            for (Map.Entry<String, Integer> entry : map.entrySet()) {
                othersValue += entry.getValue();
            }
            chartPoints.add(new ChartPointDTO("Andere", othersValue));
        }

        return chartPoints;
    }
}
