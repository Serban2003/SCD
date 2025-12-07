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

    @Query(value = "SELECT * FROM bikes WHERE status = :status", nativeQuery = true)
    List<Bike> findByStatus(@Param("status") String status);

    @Query(value = "SELECT * FROM bikes WHERE manufacturer_id = :manufacturerId", nativeQuery = true)
    List<Bike> findByManufacturerId(@Param("manufacturerId") Integer manufacturerId);

    @Query(
            value = """
                SELECT * FROM bikes
                WHERE year BETWEEN :startYear AND :endYear
                """,
            nativeQuery = true)
    List<Bike> findByYearBetween(@Param("startYear") Integer startYear,
                                 @Param("endYear") Integer endYear);

    @Query(
            value = """
                SELECT * FROM bikes
                WHERE price BETWEEN :minPrice AND :maxPrice
                """,
            nativeQuery = true)
    List<Bike> findByPriceBetween(@Param("minPrice") Float minPrice,
                                  @Param("maxPrice") Float maxPrice);

}
