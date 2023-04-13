package laba4.car;

public final class Passenger extends Car {
    private Double max_speed;

    public Passenger(String reg_znak, CarType type, String marka, String color, Engine engine, Integer wheels_quantity, Double max_speed) {
        super(reg_znak, marka, type, color, engine, wheels_quantity);
        this.max_speed = max_speed;
    }


    public static Passenger createPassengerCar(CarType type, Double max_speed) {
        Car car = Car.createCar(type);
        Passenger passenger = new Passenger(car.getReg_znak(), car.getType(), car.getMarka(), car.getColor(), car.getEngine(), car.getWheels_quantity(), max_speed);
        passenger.max_speed = max_speed;
        return passenger;
    }

    public void printInfo() {
        super.printInfo();
        System.out.println("Максимальная скорость" + this.max_speed);
    }
}
