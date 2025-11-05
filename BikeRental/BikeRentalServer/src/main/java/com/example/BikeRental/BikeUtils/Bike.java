package com.example.BikeRental.Bike;

import com.example.BikeRental.Manufacturer.Manufacturer;
import com.example.BikeRental.User.User;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import java.util.Date;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
public class Bike {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer bike_id;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "manufacturer_id")
    private Manufacturer manufacturer;
    private String model;
    private Integer year;
    private Float price;
    private Enum<BikeStatus> status;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_id")
    private User currentRenter;
    private Date rentDate;

    private enum BikeStatus{
        Available,
        Rented,
        Maintenance
    }
}
