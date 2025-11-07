package com.example.BikeRental.BikeUtils;

import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;
import java.util.List;
import java.util.Optional;

@Service
public class BikeService {
    private final BikeRepository bikeRepository;

    public BikeService(BikeRepository bikeRepository){
        this.bikeRepository = bikeRepository;
    }

    public Bike addBike(Bike bike){
        return bikeRepository.save(bike);
    }

    public List<Bike> getAllBikes(){
        return bikeRepository.findAll();
    }

    public Optional<Bike> getBike(Integer id){
        return bikeRepository.findById(id);
    }

    public void deleteBike(Integer id){
        bikeRepository.deleteById(id);
    }

    @Transactional
    public Bike updateBike(Integer id, Bike payloadBike){
        Bike existingBike = bikeRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Bike not found: " + id));

        existingBike.setManufacturer_id(payloadBike.getManufacturer_id());
        existingBike.setModel(payloadBike.getModel());
        existingBike.setYear(payloadBike.getYear());
        existingBike.setPrice(payloadBike.getPrice());
        existingBike.setStatus(payloadBike.getStatus());
        existingBike.setCurrentRenter_id(payloadBike.getCurrentRenter_id());
        existingBike.setRentDate(payloadBike.getRentDate());

        return bikeRepository.save(existingBike);
    }
    public List<Bike> getRentedBikesByUser(Integer user_id) {
        return bikeRepository.findRentedBikesByUser(user_id);
    }

}
