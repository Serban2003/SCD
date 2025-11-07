package com.example.BikeRental;

import com.example.BikeRental.Config.SecurityConfig;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;

public class EncodePasswordsTest {
    public static void main(String[] args) {
        PasswordEncoder encoder = new BCryptPasswordEncoder(); // create encoder
        String rawPassword = "daniel";
        String encodedPassword = encoder.encode(rawPassword);

        System.out.println("Raw: " + rawPassword);
        System.out.println("Encoded: " + encodedPassword);
    }
}
