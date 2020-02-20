package be.pxl.itproject.kbcfoodandgo.repositories;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.Menu;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.RepositoryDefinition;
import java.util.Optional;

@RepositoryDefinition(domainClass = Menu.class, idClass = Long.class)
public interface MenuRepository extends JpaRepository<Menu, Long> {
    Optional<Menu> findFirstByOrderByIdDesc();
    Optional<Menu> findByMenuNameIgnoreCase(String name);
}
