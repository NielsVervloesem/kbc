package be.pxl.itproject.kbcfoodandgo.models.entities;

import be.pxl.itproject.kbcfoodandgo.models.dto.UserDTO;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name= "Users")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;
    private String email;
    private  String passwordHash;
    private Role role;
    private double saldo;
    private String imageUrl;
    private String firstName;
    private String lastName;

    @OneToMany(mappedBy = "user")
    private List<MealHistory> mealHistory;

    public User(String email, String passwordHash, Role role) {
        this.email = email;
        this.passwordHash = passwordHash;
        this.role = role;
    }

    public User(String email, String passwordHash, Role role, double saldo, String imageUrl, String firstName, String lastName) {
        this.email = email;
        this.passwordHash = passwordHash;
        this.role = role;
        this.saldo = saldo;
        this.imageUrl = imageUrl;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public User(String email, String passwordHash, Role role, double saldo) {
        this.email = email;
        this.passwordHash = passwordHash;
        this.role = role;
        this.saldo = saldo;
    }

    public User(UserDTO userDto){
        this.imageUrl = userDto.getImageUrl();
        this.passwordHash = userDto.getPassword();
        this.email = userDto.getEmail();
        this.saldo = userDto.getSaldo();
        this.firstName = userDto.getFirstName();
        this.lastName = userDto.getLastName();
        this.role = userDto.getRole();
    }

    public User() {
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPasswordHash() {
        return passwordHash;
    }

    public void setPasswordHash(String passwordHash) {
        this.passwordHash = passwordHash;
    }

    public Role getRole() {
        return role;
    }

    public void setRole(Role role) {
        this.role = role;
    }

    public double getSaldo() {
        return saldo;
    }

    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }

    public List<MealHistory> getMealHistory() {
        return mealHistory;
    }

    public void setMealHistory(List<MealHistory> mealHistory) {
        this.mealHistory = mealHistory;
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
}
