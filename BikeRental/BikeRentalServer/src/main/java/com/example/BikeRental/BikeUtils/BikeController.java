package com.example.BikeRental.BikeUtils;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.NoSuchElementException;

@RestController
@RequestMapping("/bikes")
public class BikeController {

    private final BikeService bikeService;

    public BikeController(BikeService bikeService) {
        this.bikeService = bikeService;
    }

    @GetMapping
    public List<Bike> getBikes(
            @RequestParam(name = "status", required = false) String status,
            @RequestParam(name = "manufacturer_id", required = false) Integer manufacturerId,
            @RequestParam(name = "start_year", required = false) Integer startYear,
            @RequestParam(name = "end_year", required = false) Integer endYear,
            @RequestParam(name = "min_price", required = false) Float minPrice,
            @RequestParam(name = "max_price", required = false) Float maxPrice
    ) {

        if (status != null) {
            return bikeService.getBikesByStatus(status);
        }

        if (manufacturerId != null) {
            return bikeService.getBikesByManufacturer(manufacturerId);
        }

        if (startYear != null && endYear != null) {
            return bikeService.getBikesByYearRange(startYear, endYear);
        }

        if (minPrice != null && maxPrice != null) {
            return bikeService.getBikesByPriceRange(minPrice, maxPrice);
        }

        return bikeService.getAllBikes();
    }

    @GetMapping("/{id}")
    public ResponseEntity<Bike> getById(@PathVariable Integer id) {
        return bikeService.getBike(id)
                .map(ResponseEntity::ok)
                .orElseThrow(() -> new NoSuchElementException("Bike not found with id: " + id));
    }

    @PostMapping
    public ResponseEntity<Bike> addBike(@RequestBody Bike bike) {
        Bike created = bikeService.addBike(bike);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @PutMapping("/{id}")
    public ResponseEntity<Bike> updateBike(@PathVariable Integer id, @RequestBody Bike bike) {
        Bike updated = bikeService.updateBike(id, bike);
        return ResponseEntity.ok(updated);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteBike(@PathVariable Integer id) {
        bikeService.deleteBike(id);
        return ResponseEntity.noContent().build();
    }
    @GetMapping("/rented/{user_id}")
    public ResponseEntity<List<Bike>> getUserRentedBikes(@PathVariable Integer user_id) {
        List<Bike> bikes = bikeService.getRentedBikesByUser(user_id);
        return ResponseEntity.ok(bikes);
    }

}
