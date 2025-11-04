package com.example.BikeRental.Component;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/components")
public class ComponentController {
    private final ComponentService componentService;
    public ComponentController(ComponentService componentService){
        this.componentService = componentService;
    }
    @GetMapping
    public List<Component> getAllComponents(){
        return componentService.getAllComponents();
    }
    public Component addComponent(@RequestBody Component component){
        return componentService.addComponent(component);
    }
}
