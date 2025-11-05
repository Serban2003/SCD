package com.example.BikeRental.ManufacturerUtils;

import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class ManufacturerService {
    private final ManufacturerRepository manufacturerRepository;

    public ManufacturerService(ManufacturerRepository manufacturerRepository){
        this.manufacturerRepository = manufacturerRepository;
    }

    public Manufacturer addManufacturer(Manufacturer manufacturer){
        return manufacturerRepository.save(manufacturer);
    }

    public List<Manufacturer> getAllManufacturers(){
        return manufacturerRepository.findAll();
    }

    public Optional<Manufacturer> getManufacturer(Integer id){
        return manufacturerRepository.findById(id);
    }

    public void deleteManufacturer(Integer id){
        manufacturerRepository.deleteById(id);
    }

    @Transactional
    public Manufacturer updateManufacturer(Integer id, Manufacturer payloadManufacturer){
        Manufacturer existingManufacturer = manufacturerRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Manufacturer not found: " + id));

        existingManufacturer.setName(payloadManufacturer.getName());
        existingManufacturer.setDescription(payloadManufacturer.getDescription());

        return manufacturerRepository.save(existingManufacturer);
    }
}
