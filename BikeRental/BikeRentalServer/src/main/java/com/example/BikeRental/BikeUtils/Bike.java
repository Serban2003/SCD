package com.example.BikeRental.BikeUtils;

import com.example.BikeRental.ManufacturerUtils.Manufacturer;
import com.example.BikeRental.UserUtils.User;
import com.fasterxml.jackson.annotation.JsonProperty;
import jakarta.persistence.*;
import lombok.*;

import java.util.Date;

@Entity
@Table(name="bikes")
@Data
@AllArgsConstructor
@NoArgsConstructor
public class Bike {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @JsonProperty(access = JsonProperty.Access.READ_ONLY)
    private Integer bike_id;

    @Column(nullable = false)
    private Integer manufacturer_id;

    @Column(nullable = false)
    private String model;

    @Column(nullable = false)
    private Integer year;

    @Column(nullable = false)
    private Float price;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private BikeStatus status;

    private Integer currentRenter_id;
    private Date rentDate;

    public enum BikeStatus{
        AVAILABLE,
        RENTED,
        MAINTENANCE
    }
}
