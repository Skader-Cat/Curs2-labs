package laba4.car;

public final class Truck extends Car {
    private Integer carrying_capacity;

    public Truck(String reg_znak, CarType type, String marka, String color, Engine engine, Integer wheels_quantity, Integer carrying_capacity) {
        super(reg_znak, marka, type, color, engine, wheels_quantity);
        this.carrying_capacity = carrying_capacity;
    }

    public void printInfo() {
        super.printInfo();
        System.out.println("Грузоподъемность: " + this.carrying_capacity);
    }


    public static Truck createTruck(CarType type, Integer carrying_capacity) {
        Car car = Car.createCar(type);
        Truck truck = new Truck(car.getReg_znak(), car.getType(), car.getMarka(), car.getColor(), car.getEngine(), car.getWheels_quantity(), carrying_capacity);
        truck.carrying_capacity = carrying_capacity;
        return truck;
    }
}

