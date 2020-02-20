package be.pxl.itproject.kbcfoodandgo.configuration;

import be.pxl.itproject.kbcfoodandgo.models.entities.*;
import be.pxl.itproject.kbcfoodandgo.repositories.MealRepository;
import be.pxl.itproject.kbcfoodandgo.repositories.MenuRepository;
import be.pxl.itproject.kbcfoodandgo.repositories.UserRepository;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.context.annotation.Profile;
import org.springframework.stereotype.Component;
import java.util.*;

@Component
@Profile("!prod")
public class DataSeeder implements ApplicationRunner {

    private final MealRepository mealRepository;
    private final MenuRepository menuRepository;
    private final UserRepository userRepository;
    private ArrayList<Meal> mealList = new ArrayList<>();
    private ArrayList<User> userList = new ArrayList<>();

    public DataSeeder(MealRepository mealRepository, MenuRepository menuRepository, UserRepository userRepository) {
        this.mealRepository = mealRepository;
        this.menuRepository = menuRepository;
        this.userRepository = userRepository;
    }

    @Override
    public void run(ApplicationArguments args)  {
        seedMeals();
        seedMenus();
        seedUsers();
    }

    private void seedMeals() {
        mealList = new ArrayList<>();
        mealList.add(new Meal(
                "Croque Monsieur",
                "Krokante sneetjes brood, smeuïge gesmolten kaas en een plakje ham.",
                10.20,
                "https://s3.eu-central-1.amazonaws.com/kbc-cdn/croquemonsieur.jpg"));
        mealList.add(new Meal(
                "Flat Angus Beef Burger",
                "Beefburger met cheddar, bacon, ijsbergsla, tomaat & home made burger relish.",
                11.75,
                "https://s3.eu-central-1.amazonaws.com/kbc-cdn/flatangusbeefburger.jpg"));
        mealList.add(new Meal(
                "Spaghetti Bolognaise",
                "Bolognesesaus, rundergehakt, selderij, worteltjes, ui, Parmezaane kaas en peterselie.",
                13.00,
                "https://s3.eu-central-1.amazonaws.com/kbc-cdn/spaghettibolognaise.jpg"));
        mealList.add(new Meal(
                "Caesar Salade",
                "Kip, Romeinse sla, ei, parmezaan en croutons",
                10.90,
                "https://s3.eu-central-1.amazonaws.com/kbc-cdn/caesarsalade.jpg"));
        mealList.add(new Meal(
                "Coupe Dame blanche",
                "Vanille-ijs met warme chocoladesaus",
                8.5,
                "https://s3.eu-central-1.amazonaws.com/kbc-cdn/coupedameblanche.jpg"));
        mealRepository.saveAll(mealList);
    }

    private void seedMenus() {
        List<String> daysOfTheWeek = Arrays.asList("maandag", "dinsdag","woensdag","donderdag","vrijdag");

        for(String day: daysOfTheWeek){
            Menu menu = new Menu();
            menu.setMeals(mealList);
            menu.setMenuName(day);
            menuRepository.save(menu);
        }
    }


    private void seedUsers() {
        String hash = "$2a$04$u0RMl3DG9NQ0w2XKDkEWk.OF4r7ZKtQMdiM86ac6UfyAdSvg3Nf0m";
        String email = "@hotmail.com";
        double balance = 100.00;
        String imageUrlBase = "https://s3.eu-central-1.amazonaws.com/kbc-cdn/";

        userList.add(new User("admin" +  email, hash, Role.ADMIN, balance, imageUrlBase + "admin" + ".jpg", "adminFirstName", "adminLastName"));
        userList.add(new User("employee" + email, hash, Role.CAFETARIA_EMPLOYEE, balance, imageUrlBase + "employee" + ".jpg", "employeeFirstName", "employeeLastName"));

        for (int i = 0; i <= 10; i++) {
            userList.add(new User("customer" + i + email, hash, Role.CUSTOMER, balance, imageUrlBase + "customer" + i + ".jpg", "customerFirstName", "customerLastName"));
        }

        userRepository.saveAll(userList);
    }
}