package be.pxl.itproject.kbcfoodandgo.repositories;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.RepositoryDefinition;

import java.util.List;

@RepositoryDefinition(domainClass = Meal.class, idClass = Long.class)
public interface MealRepository extends JpaRepository<Meal, Long> {
    List<Meal> findByNameContainingIgnoreCase(String text);

}