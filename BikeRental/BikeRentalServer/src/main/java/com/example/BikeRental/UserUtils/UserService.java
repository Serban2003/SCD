package com.example.BikeRental.User;

import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.Optional;

@Service
public class UserService {
    private final UserRepository userRepository;

    public UserService(UserRepository userRepository){
        this.userRepository = userRepository;
    }

    public User addUser(User user){
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

        return userRepository.save(existingUser);
    }
}
