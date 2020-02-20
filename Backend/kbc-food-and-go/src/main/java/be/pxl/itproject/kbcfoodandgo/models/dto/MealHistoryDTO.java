package be.pxl.itproject.kbcfoodandgo.models.dto;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.MealHistory;
import be.pxl.itproject.kbcfoodandgo.models.entities.User;
import org.springframework.stereotype.Service;
import java.util.List;

@Service
public class MealHistoryDTO {
    private long id;

    private List<Meal> mealList;

    private User user;

    private double totalPrice;

    public MealHistoryDTO() {
    }

    public MealHistoryDTO(MealHistory mealHistory){
        this.setId(mealHistory.getId());
        this.setMealList(mealHistory.getMealList());
        this.setUser(mealHistory.getUser());
        this.setTotalPrice(mealHistory.getTotalPrice());
    }

    public MealHistoryDTO(long id, List<Meal> mealList, User user, double totalPrice) {
        this.id = id;
        this.mealList = mealList;
        this.user = user;
        this.totalPrice = totalPrice;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public List<Meal> getMealList() {
        return mealList;
    }

    public void setMealList(List<Meal> mealList) {
        this.mealList = mealList;
    }

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
    }

    public double getTotalPrice() {
        return totalPrice;
    }

    public void setTotalPrice(double totalPrice) {
        this.totalPrice = totalPrice;
    }
}
