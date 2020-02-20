package be.pxl.itproject.kbcfoodandgo.controllers;

import be.pxl.itproject.kbcfoodandgo.aop.annotations.LogAction;
import be.pxl.itproject.kbcfoodandgo.models.dto.UserDTO;
import be.pxl.itproject.kbcfoodandgo.models.entities.Meal;
import be.pxl.itproject.kbcfoodandgo.models.entities.Role;
import be.pxl.itproject.kbcfoodandgo.models.entities.User;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.DataService;
import be.pxl.itproject.kbcfoodandgo.services.interfaces.UserManager;
import org.apache.commons.codec.binary.Base64;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

import java.io.*;
import java.util.List;
import java.util.Optional;

@RestController
@RequestMapping("api/user")
public class UserController {

    private UserManager userManager;
    private PasswordEncoder passwordEncoder;
    private final DataService dataService;

    public static final String DEFAULT_IMAGE = "defaultImage.jpg";

    @Autowired
    public UserController(UserManager userManager, DataService dataService) {
        passwordEncoder = new BCryptPasswordEncoder(4);
        this.userManager = userManager;
        this.dataService = dataService;
    }

    @GetMapping(value = "/", produces = "application/json;charset=utf-8")
    public ResponseEntity<Iterable<User>> getAllUsers(){
        Iterable<User> allUsers = userManager.getAllUsers();

        if (((List<User>) allUsers).isEmpty()){
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(allUsers, HttpStatus.OK);
    }

    @PostMapping(value = "/list", produces = "application/json;charset=utf-8")
    public ResponseEntity<List<User>> getUsersByList(@RequestBody Iterable<Long> idList){
        List<User> allUsers = userManager.getUsersByIdList(idList);

        if (allUsers.isEmpty()){
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
        return new ResponseEntity<>(allUsers, HttpStatus.OK);
    }

    @GetMapping(value = "/{id}", produces = "application/json;charset=utf-8")
    public ResponseEntity<Optional<User>> getUserById(@PathVariable long id) {
        Optional<User> user = userManager.getUserById(id);
        if (user.isPresent()) {
            return new ResponseEntity<>(user, HttpStatus.OK);
        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @PutMapping(value = "/{id}", produces = "application/json;charset=utf-8")
    public ResponseEntity<User> addMealHistoryToUser(@PathVariable long id, @RequestBody List<Meal> meal) {
        User user = userManager.updateMealHistory(id, meal);
        return new ResponseEntity<>(user, HttpStatus.OK);
    }

    @PostMapping(value= "/login", consumes = "application/json;charset=utf-8")
    public ResponseEntity<User> login(@RequestBody UserDTO userDTO) {
        Optional<User> optionalUser = userManager.getUserByEmail(userDTO.getEmail());

        if (optionalUser.isPresent())  {
            User user = optionalUser.get();
            boolean check = passwordEncoder.matches(userDTO.getPassword(), user.getPasswordHash());

            if (check) {
                return new ResponseEntity<>(user, HttpStatus.OK);
            } else {
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }

        } else {
            return new ResponseEntity<>(HttpStatus.NOT_FOUND);
        }
    }

    @LogAction
    @PostMapping(value = "/create", consumes = "application/json;charset=utf-8")
    public ResponseEntity<User> create(@RequestBody UserDTO userDTO) {
        userDTO.setPassword(passwordEncoder.encode(userDTO.getPassword()));
        userDTO.setRole(Role.CUSTOMER);

        String imageName;
        try {
            imageName = dataService.saveImage(userDTO.getImageBase64(), (userDTO.getFirstName()+userDTO.getLastName()));

            userDTO.setImageUrl(String.format("https://s3.eu-central-1.amazonaws.com/kbc-cdn/%s", imageName));
            User user = new User(userDTO);
            User savedUser = userManager.createUser(user);

            byte[] dearr = Base64.decodeBase64(userDTO.getImageBase64());
            FileOutputStream fos = null;

            fos = new FileOutputStream(new File("C:\\FoodAndGo\\Backend\\images\\knownfaces\\" + user.getId() + ".png"));
            fos.write(dearr);
            fos.close();

            return new ResponseEntity<>(savedUser, HttpStatus.CREATED);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

        return null;
    }
}
