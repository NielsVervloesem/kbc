package be.pxl.itproject.kbcfoodandgo.models.dto;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;

public class MealDTO {
    private long id;
    private String name;
    private String shortDescription;
    private String imageBase64;
    private String imageUrl;
    private double price;

    public MealDTO() {
    }

    public MealDTO(Meal meal)  {
        this.setId(meal.getId());
        this.setName(meal.getName());
        this.setPrice(meal.getPrice());
        this.setShortDescription(meal.getShortDescription());
        this.setImageUrl(meal.getImageUrl());
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

    public String getImageBase64() {
        return imageBase64;
    }

    public void setImageBase64(String imageBase64) {
        this.imageBase64 = imageBase64;
    }

    public String getImageUrl() {
        return imageUrl;
    }

    public void setImageUrl(String imageUrl) {
        this.imageUrl = imageUrl;
    }

    @Override
    public String toString() {
        return String.format("%s met prijs:\"%.2f\" en beschrijving:\"%s\"", name, price, shortDescription);
    }
}
