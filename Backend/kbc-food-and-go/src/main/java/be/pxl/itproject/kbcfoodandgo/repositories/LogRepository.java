package be.pxl.itproject.kbcfoodandgo.repositories;
import be.pxl.itproject.kbcfoodandgo.models.entities.Log;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.RepositoryDefinition;

@RepositoryDefinition(domainClass = Log.class, idClass = Long.class)
public interface LogRepository extends JpaRepository<Log, Long> {
}

