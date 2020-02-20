package be.pxl.itproject.kbcfoodandgo.models.dto;

import be.pxl.itproject.kbcfoodandgo.models.entities.MealHistory;
import be.pxl.itproject.kbcfoodandgo.models.entities.Role;
import be.pxl.itproject.kbcfoodandgo.models.entities.User;

public class UserDTO {
    private Long id;
    private String email;
    private String password;
    private double saldo;
    private MealHistory mealHistory;
    private String imageBase64;
    private String imageUrl;
    private String firstName;
    private String lastName;
    private Role role;

    public UserDTO(){
    }

    public UserDTO(User user){
        this.setId(user.getId());
        this.setImageUrl(user.getImageUrl());
        this.setPassword(user.getPasswordHash());
        this.setEmail(user.getEmail());
        this.setSaldo(user.getSaldo());
        this.setFirstName(user.getFirstName());
        this.setLastName(user.getLastName());
        this.setRole(user.getRole());
    }

    public UserDTO(String email, String password) {
        this.email = email;
        this.password = password;
    }

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public double getSaldo() {
        return saldo;
    }

    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }

    public MealHistory getMealHistory() {
        return mealHistory;
    }

    public void setMealHistory(MealHistory mealHistory) {
        this.mealHistory = mealHistory;
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

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public Role getRole() {
        return role;
    }

    public void setRole(Role role) {
        this.role = role;
    }
}
