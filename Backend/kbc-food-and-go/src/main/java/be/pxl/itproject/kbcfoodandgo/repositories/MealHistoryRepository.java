package be.pxl.itproject.kbcfoodandgo.repositories;

import be.pxl.itproject.kbcfoodandgo.models.entities.MealHistory;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.RepositoryDefinition;

@RepositoryDefinition(domainClass = MealHistory.class, idClass = Long.class)
public interface MealHistoryRepository extends JpaRepository<MealHistory, Long> {
}
