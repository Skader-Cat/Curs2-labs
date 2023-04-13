package laba4.autobase;

import laba4.car.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class Autobase {
    public int carQuantity;
    List<Car> cars_on_base = new ArrayList<>();
    List<Car> cars_on_reys = new ArrayList<>();
    List<Car> cars_on_refund = new ArrayList<>();

    public Autobase(int carQuantity){
        this.carQuantity = carQuantity;
        int i = 0;
        while (i < carQuantity){
            Scanner cin = new Scanner(System.in);
            System.out.println("Машина №"+(i) +" :");
            System.out.println("Какой вид транспорта хотите создать? (автобус, грузовой, легковой, полицейский) или введите " +
                    "'всё', если закончили");
            var result = cin.nextLine().toUpperCase();
            if(result == "всё"){
                break;
            }
            var answer = CarType.valueOf(result);

            switch (answer){
                case АВТОБУС -> {
                    makeBus(answer);
                    break;
                }
                case ГРУЗОВОЙ -> {
                    makeTruck(answer);
                    break;
                }
                case ЛЕГКОВОЙ -> {
                    makePass(answer);
                    break;
                }
                case ПОЛИЦЕЙСКИЙ -> {
                    makePolice(answer);
                    break;
                }
            }
            i++;
        }
    }

    private void makeBus(CarType type){
        Scanner cin = new Scanner(System.in);
        Car car = Car.createCar(type);

        System.out.println("\nВведите максимальное количество пассажиров автобуса: ");
        Bus bus = new Bus(car.getReg_znak(), type , car.getMarka(), car.getColor(), car.getEngine(), car.getWheels_quantity(), cin.nextInt());
        bus.printInfo();
        cars_on_base.add(bus);
    }

    private void makePolice(CarType type) {
        Scanner cin = new Scanner(System.in);
        Car car = Car.createCar(type);
        System.out.println("Введите максимальную прочность автомобиля: ");
        Police police = new Police(car.getReg_znak(), type , car.getMarka(), car.getColor(), car.getEngine(), car.getWheels_quantity(), cin.nextInt());
        police.printInfo();
        cars_on_base.add(police);
    }

    private void makePass(CarType type) {
        Scanner cin = new Scanner(System.in);
        Car car = Car.createCar(type);
        System.out.println("Введите максимальную скорость автомобиля: ");
        Passenger passenger = new Passenger(car.getReg_znak(), type , car.getMarka(), car.getColor(), car.getEngine(), car.getWheels_quantity(), cin.nextDouble());
        passenger.printInfo();
        cars_on_base.add(passenger);
    }

    private void makeTruck(CarType type) {
        Scanner cin = new Scanner(System.in);
        Car car = Car.createCar(type);
        System.out.println("Введите максимальную грузоподъемность грузовика: ");
        Truck truck = new Truck(car.getReg_znak(), type , car.getMarka(), car.getColor(), car.getEngine(), car.getWheels_quantity(), cin.nextInt());
        truck.printInfo();
        cars_on_base.add(truck);
    }

    public void printBaseCarInfo(){
        System.out.println("На базе находится машин: "+cars_on_base.size());
        System.out.println("Имеются следующие автомобили");
        for(int i = 0; i < cars_on_base.size(); i++){
            System.out.println("Машина №" + i+1 );
            cars_on_base.get(i).printInfo();
            System.out.println("-".repeat(10));
        }
    }

    public void printReysCarInfo(){
        System.out.println("~".repeat(10));
        System.out.println("В рейсе находится машин: "+cars_on_reys.size());
        System.out.println("Имеются следующие автомобили");
        for(int i = 0; i < cars_on_reys.size(); i++){
            System.out.println("Машина №" + i );
            cars_on_reys.get(i).printInfo();
            System.out.println("-".repeat(10));
        }
        System.out.println("~".repeat(10));
    }

    public void printRefundCarInfo(){
        System.out.println("~".repeat(10));
        System.out.println("На ремонте находится машин: "+cars_on_refund.size());
        System.out.println("Имеются следующие автомобили");
        for(int i = 0; i < cars_on_refund.size(); i++){
            System.out.println("Машина №" + i );
            cars_on_refund.get(i).printInfo();
            System.out.println("-".repeat(10));
        }
        System.out.println("~".repeat(10));
    }
    public Car getFromCarsByType(CarType type, List<Car> some_cars_list){
        System.out.println("~".repeat(10));
        for(int i = 0; i < some_cars_list.size(); i++){
            if(some_cars_list.get(i).getType() == type){
                var car = some_cars_list.get(i);
                some_cars_list.remove(i);
                return car;
            }
        }
        System.out.println("Заданный тип транспорта не обнаружен.");
        System.out.println("~".repeat(10));
        return null;
    }

    public void appendToReys(Car car){
        cars_on_reys.add(car);
        System.out.println("Отправлен в рейс " + car.getType());
    }

    public void appendToRefund(Car car){
        cars_on_refund.add(car);
        System.out.println("Отправлен в ремонт " + car.getType());
    }

    public void appendToBase(Car car){
        if(cars_on_base.size() < carQuantity){
            cars_on_base.add(car);
        }
        else{
            System.out.println("Автопарк переполнен!");
        }
        System.out.println("Отправлен на базу " + car.getType());
    }

    public void removeFromBase(Car car){
        cars_on_base.remove(car);
    }

    public void removeFromRefund(Car car){
        cars_on_refund.remove(car);
    }

    public void removeFromReys(Car car){
        cars_on_reys.remove(car);
    }
}