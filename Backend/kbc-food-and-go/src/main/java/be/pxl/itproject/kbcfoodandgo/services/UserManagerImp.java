package be.pxl.itproject.kbcfoodandgo.services;

import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.MealHistory;
import be.pxl.itproject.kbcfoodandgo.models.entities.User;
import be.pxl.itproject.kbcfoodandgo.repositories.LogRepository;
import be.pxl.itproject.kbcfoodandgo.repositories.MealHistoryRepository;
import be.pxl.itproject.kbcfoodandgo.repositories.UserRepository;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.PusherService;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.UserManager;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Optional;

@Service
public class UserManagerImp implements UserManager {
    private final UserRepository userRepository;
    private final MealHistoryRepository mealHistoryRepository;
    private final PusherService pusherService;

    public UserManagerImp(UserRepository userRepository, MealHistoryRepository mealHistoryRepository, PusherService pusherService) {
        this.userRepository = userRepository;
        this.mealHistoryRepository = mealHistoryRepository;
        this.pusherService = pusherService;
    }

    @Override
    public Optional<User> getUserById(long id) {
        return userRepository.findById(id);
    }

    @Override
    public Optional<User> getUserByEmail(String email) {
        return userRepository.findByEmail(email);
    }

    @Override
    public User createUser(User user) {
        return userRepository.save(user);
    }

    @Override
    public User updateMealHistory(long id, List<Meal> meals) {
        Optional<User> user = userRepository.findById(id);
        if(user.isPresent()){
            MealHistory mealHistory = new MealHistory();
            mealHistory.setMealList(meals);
            LocalDateTime ldt = LocalDateTime.now();
            Date now = Date.from( ldt.atZone( ZoneId.systemDefault()).toInstant());
            mealHistory.setDate(now);
            mealHistory.setUser(user.get());

            for(Meal meal:meals){
                mealHistory.setTotalPrice(mealHistory.getTotalPrice() + meal.getPrice());
            }

            List<MealHistory> mealHistories = new ArrayList<>();
            mealHistories.add(mealHistory);
            user.get().setMealHistory(mealHistories);
            mealHistoryRepository.save(mealHistory);
        }

        pusherService.onAddMealHistory();
        return user.map(userRepository::save).orElse(null);
    }

    @Override
    public Iterable<User> getAllUsers() {
        return userRepository.findAll();
    }

    @Override
    public List<User> getUsersByIdList(Iterable<Long> idList) {
        return userRepository.findAllById(idList);
    }
}
