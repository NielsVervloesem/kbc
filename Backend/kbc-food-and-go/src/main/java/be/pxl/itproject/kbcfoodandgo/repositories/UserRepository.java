package be.pxl.itproject.kbcfoodandgo.repositories;

import be.pxl.itproject.kbcfoodandgo.models.entities.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.RepositoryDefinition;

import java.util.Optional;

@RepositoryDefinition(domainClass = User.class, idClass = Long.class)
public interface UserRepository  extends JpaRepository<User, Long> {
    Optional<User> findByEmail(String email);
}
