package be.pxl.itproject.kbcfoodandgo.models.dto;

import java.util.List;

public class MenuDTO {

    private long id;

    private List<MealDTO> meals;

    public MenuDTO() { }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public List<MealDTO> getMeals() {
        return meals;
    }

    public void setMeals(List<MealDTO> meals) {
        this.meals = meals;
    }
}
