package be.pxl.itproject.kbcfoodandgo.models.entities;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonIgnore;

import javax.persistence.*;
import java.util.Date;
import java.util.List;

@Entity
@Table(name = "MealHistories")
public class MealHistory {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;

    @ManyToMany
    @JoinTable(
            name = "mealhistory_meal",
            joinColumns = @JoinColumn(name = "mealhistory_id"),
            inverseJoinColumns = @JoinColumn(name = "meal_id"))
    private List<Meal> mealList;

    @ManyToOne
    @JoinColumn(name = "userId")
    @JsonIgnore
    private User user;

    @JsonFormat(pattern = "dd-MM-yyyy HH:mm:ss")
    private Date date;

    private double totalPrice;

    public MealHistory(){
    }

    public MealHistory(List<Meal> mealList, User user, Date date, double totalPrice) {
        this.mealList = mealList;
        this.user = user;
        this.date = date;
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

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
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

