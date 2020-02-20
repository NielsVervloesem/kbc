package be.pxl.itproject.kbcfoodandgo.models.entities;
import be.pxl.itproject.kbcfoodandgo.models.dto.MealDTO;
import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "Meals")
public class Meal {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private long id;
    private String name;
    private String shortDescription;
    private double price;
    private String imageUrl;

    @ManyToMany(cascade = {CascadeType.ALL})
    private List<Menu> menu;

    @ManyToMany
    private List<MealHistory> mealHistoryList;

    @ManyToOne
    @JoinColumn(name = "mealHistory_id")
    private MealHistory mealHistory;

    public Meal() {
    }

    public Meal(String name, String shortDescription, double price,String imageUrl) {
        this.name = name;
        this.shortDescription = shortDescription;
        this.price = price;
        this.imageUrl = imageUrl;
    }

    public Meal(long id, String name, String shortDescription, double price) {
        this.id = id;
        this.name = name;
        this.shortDescription = shortDescription;
        this.price = price;
    }

    public Meal(MealDTO mealDTO) {
        this.id = mealDTO.getId();
        this.name = mealDTO.getName();
        this.shortDescription = mealDTO.getShortDescription();
        this.price = mealDTO.getPrice();
        this.imageUrl = mealDTO.getImageUrl();
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getShortDescription() {
        return shortDescription;
    }

    public void setShortDescription(String shortDescription) {
        this.shortDescription = shortDescription;
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public String getImageUrl() {
        return imageUrl;
    }

    @Override
    public String toString() {
        return String.format("%s met prijs:\"%.2f\" en beschrijving:\"%s\"", name, price, shortDescription);
    }
}
