package com.example.BikeRental.UserUtils;

import com.example.BikeRental.BikeUtils.Bike;
import com.example.BikeRental.BikeUtils.BikeService;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.List;
import java.util.Map;
import java.util.NoSuchElementException;

@RestController
@RequestMapping("/users")
public class UserController {
    private final UserService userService;

    public UserController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping
    public List<User> getAllUsers() {
        return userService.getAllUsers();
    }

    @GetMapping("/{id}")
    public ResponseEntity<User> getById(@PathVariable Integer id) {
        return userService.getUser(id)
                .map(ResponseEntity::ok)
                .orElseThrow(() -> new NoSuchElementException("User not found with id: " + id));
    }

    @PostMapping
    public ResponseEntity<User> addUser(@RequestBody User user) {
        User createdUser = userService.addUser(user);
        return ResponseEntity.status(HttpStatus.CREATED).body(createdUser);
    }

    @PutMapping("/{id}")
    public ResponseEntity<User> updateUser(
            @PathVariable Integer id,
            @RequestBody User user) {
        User updated = userService.updateUser(id, user); // may throw IllegalArgumentException if not found
        return ResponseEntity.ok(updated);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteUser(@PathVariable Integer id) {
        userService.deleteUser(id); // throw NoSuchElementException if missing
        return ResponseEntity.noContent().build();
    }

    @PostMapping("/register")
    public ResponseEntity<User> register(@RequestBody User user) {
        if (user.getFirstname() == null || user.getFirstname().isBlank()
                || user.getLastname() == null || user.getLastname().isBlank()
                || user.getEmail() == null || user.getEmail().isBlank()
                || user.getPassword() == null || user.getPassword().isBlank()) {
            throw new IllegalArgumentException("Invalid input: all fields are required.");
        }

        userService.addUser(user);
        return ResponseEntity.status(HttpStatus.CREATED).body(user);
    }

    @PostMapping("/login")
    public ResponseEntity<User> login(@RequestBody Map<String, String> body) {
        String em = body.getOrDefault("email", "").trim();
        String pw = body.getOrDefault("password", "").trim();

        if (em.isEmpty() || pw.isEmpty()) {
            throw new IllegalArgumentException("Invalid input: email and password are required.");
        }

        User user = userService.login(em, pw);
        return ResponseEntity.ok(user);
    }
}
