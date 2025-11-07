package com.example.BikeRental.UserUtils;

import com.example.BikeRental.BikeUtils.Bike;
import jakarta.transaction.Transactional;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.Optional;

@Service
public class UserService {
    private final UserRepository userRepository;
    private final PasswordEncoder passwordEncoder;

    public UserService(UserRepository userRepository, PasswordEncoder passwordEncoder){
        this.userRepository = userRepository;
        this.passwordEncoder = passwordEncoder;
    }

    public User addUser(User user){
        user.setPassword(passwordEncoder.encode(user.getPassword()));
        return userRepository.save(user);
    }

    public List<User> getAllUsers(){
        return userRepository.findAll();
    }

    public Optional<User> getUser(Integer id){
        return userRepository.findById(id);
    }

    public void deleteUser(Integer id){
        userRepository.deleteById(id);
    }

    @Transactional
    public User updateUser(Integer id, User payloadUser){
        User existingUser = userRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("User not found: " + id));

        existingUser.setFirstname(payloadUser.getFirstname());
        existingUser.setLastname(payloadUser.getLastname());
        existingUser.setEmail(payloadUser.getEmail());

        if (payloadUser.getPassword() != null && !payloadUser.getPassword().isBlank()) {
            existingUser.setPassword(passwordEncoder.encode(payloadUser.getPassword()));
        }

        return userRepository.save(existingUser);
    }

    public User login(String email, String rawPassword) {
        User user = userRepository.findByEmail(email)
                .orElseThrow(() -> new BadCredentialsException("Invalid email or password."));
        if (!passwordEncoder.matches(rawPassword, user.getPassword())) {
            throw new BadCredentialsException("Invalid email or password.");
        }
        return user;
    }

}
