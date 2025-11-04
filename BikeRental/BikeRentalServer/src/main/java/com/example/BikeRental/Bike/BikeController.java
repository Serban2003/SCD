package com.example.BikeRental.Bike;

import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/bikes")
public class BikeController {
    private final BikeService bikeService;
    public BikeController(BikeService bikeService){
        this.bikeService = bikeService;
    }
    @GetMapping("/get-bikes")
    public List<Bike> getAllBikes() {
        return bikeService.getAllBikes();
    }
    @PostMapping("/create-bike")
    public Bike addBike(@RequestBody Bike bike){
        return bikeService.addBike(bike);
    }
}
