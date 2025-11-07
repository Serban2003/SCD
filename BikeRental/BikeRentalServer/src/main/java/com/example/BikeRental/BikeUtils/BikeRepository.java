package com.example.BikeRental.BikeUtils;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface BikeRepository extends JpaRepository<Bike, Integer> {
    @Query(
            value = """
              SELECT * 
              FROM bikes 
              WHERE current_renter_id = :user_id 
                AND status = 'RENTED'
              ORDER BY rent_date DESC
              """,
            nativeQuery = true)
    List<Bike> findRentedBikesByUser(@Param("user_id") Integer user_id);
}
