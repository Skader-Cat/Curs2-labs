package laba4.car;

import java.util.Scanner;

import static laba4.car.Main.makeEngine;

public final class Bus extends Car {
    private Integer people_capacity;

    public Bus(String reg_znak, CarType type, String marka, String color, Engine engine, Integer wheels_quantity, Integer people_capacity) {
        super(reg_znak, marka, type, color, engine, wheels_quantity);
        this.people_capacity = people_capacity;
    }


    public void setPeople_capacity(Integer people_capacity) {
        this.people_capacity = people_capacity;
    }

    public Integer getPeople_capacity() {
        return people_capacity;
    }


    public void printInfo() {
        super.printInfo();
        System.out.println("Вместительность человек " + this.people_capacity);
    }
}

