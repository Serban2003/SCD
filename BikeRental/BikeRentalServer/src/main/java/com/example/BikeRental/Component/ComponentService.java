package com.example.BikeRental.Component;

import com.example.BikeRental.Bike.Bike;
import com.example.BikeRental.Bike.BikeRepository;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class ComponentService {
    private final ComponentRepository componentRepository;
    public ComponentService(ComponentRepository componentRepository){
        this.componentRepository = componentRepository;
    }
    public Component addComponent(Component component){
        return componentRepository.save(component);
    }
    public List<Component> getAllComponents(){
        return componentRepository.findAll();
    }
    public Optional<Component> getComponent(Integer id){
        return componentRepository.findById(id);
    }
    public void deleteComponent(Integer id){
        componentRepository.deleteById(id);
    }
    @Transactional
    public Component updateComponent(Integer id, Component payloadComponent){
        Component existingComponent = componentRepository.findById(id)
                .orElseThrow(() -> new IllegalArgumentException("Component not found: " + id));

        existingComponent.setName(payloadComponent.getName());
        existingComponent.setManufacturer(payloadComponent.getManufacturer());

        return componentRepository.save(existingComponent);
    }
}
