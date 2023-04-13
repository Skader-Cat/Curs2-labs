package laba4.car;


import java.util.Locale;
import java.util.Scanner;

import static laba4.car.Main.makeEngine;

public class Car {
    private String reg_znak;
    private final String marka;
    CarType type;
    private String color;
    private final Engine engine;
    private Integer wheels_quantity;

    public Car(String reg_znak, String marka, CarType type, String color, Engine engine, Integer wheels_quantity) {
        if(type == CarType.ПОЛИЦЕЙСКИЙ){
            this.reg_znak = reg_znak;
        } else if (reg_znak == null) {
            this.reg_znak = "";
        } else {
            try {
                this.reg_znak = verifyRegZnak(reg_znak);
            } catch (IllegalArgumentException e) {
                System.out.println("Ошибка: " + e.getMessage());
            }
        }
        this.marka = marka;
        this.type = type;
        this.color = color;
        this.engine = engine;
        this.wheels_quantity = wheels_quantity;
    }

    public Car(String marka, CarType type, String color, Engine engine, Integer wheels_quantity) {
        this.marka = marka;
        this.type = type;
        this.color = color;
        this.engine = engine;
        this.wheels_quantity = wheels_quantity;
    }

    public static String verifyRegZnak(String reg_znak) {
        String regex = "[АВЕКМНОРСТУХ]\\d{3}[АВЕКМНОРСТУХ]{2}\\d{2}\\sRUS";
        var jest = reg_znak.matches(regex);
        if (reg_znak != null && reg_znak.matches(regex)) {
            return reg_znak;
        } else {
            throw new IllegalArgumentException("Регистрационный знак не соответствует шаблону");
        }
    }

    public void printInfo() {
        System.out.println("Марка: " + this.marka);
        System.out.println("Тип: " + this.type);
        System.out.println("Цвет: " + this.color);
        System.out.println("Двигатель--->");
        engine.printInfo();
        System.out.println("Количество колес: " + this.wheels_quantity);
        System.out.println("Регистрационный номер: " + this.reg_znak);
    }

    public String getRegistrationNumber() {
        return reg_znak;
    }

    public void setRegistrationNumber(String registrationNumber) {
        this.reg_znak = registrationNumber;
    }

    public void changeColor() {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Введите новый цвет машины: ");
        String newColor = scanner.nextLine();
        this.color = newColor;
        System.out.println("Цвет машины изменен на " + newColor);
    }

    public void changeRegNumber() {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Введите новый регистрационный номер: ");
        String newRegNumber = scanner.nextLine();
        try {
            this.reg_znak = verifyRegZnak(newRegNumber);
            System.out.println("Регистрационный номер изменен на " + newRegNumber);
        } catch (IllegalArgumentException e) {
            System.out.println("Ошибка: " + e.getMessage());
        }
    }

    public static void actions(Car car) {
        System.out.println("_______________________________\nСоздан автомобиль: ");
        car.printInfo();

        while (true) {
            Scanner scan = new Scanner(System.in);
            System.out.println("-----------------------------\nВведите, что хотите сделать:\n" +
                    "Вывести информацию о машине (0)\n" +
                    "Изменить рег.номер (1)\n" +
                    "Изменить цвет (2)");
            int answer = scan.nextInt();

            switch (answer) {
                case 0:
                    car.printInfo();
                    break;

                case 1:
                    car.changeRegNumber();
                    break;

                case 2:
                    car.changeColor();
                    break;
            }
        }
    }

    public static Car createCar(CarType type) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Создается " + type.toString().toLowerCase(Locale.ROOT) + "----->");
        System.out.print("Введите марку автомобиля: ");
        String marka = scanner.nextLine();

        /*
        System.out.print("Введите тип автомобиля (легковой, грузовой, автобус, полицейский): ");
        String typeString = scanner.nextLine();
        CarType type = CarType.valueOf(typeString.toUpperCase());
        */

        System.out.print("Введите цвет автомобиля: ");
        String color = scanner.nextLine();

        System.out.println("Создание двигателя в машине----->");
        var engine = makeEngine();

        System.out.print("Введите количество колес автомобиля: ");
        Integer wheels_quantity = scanner.nextInt();

        if(type != CarType.ПОЛИЦЕЙСКИЙ) {
            System.out.print("Вы хотите задать регистрационный номер? (Y/N): ");
            String input = scanner.next();
            String reg_znak = "";
            if (input.equalsIgnoreCase("y")) {
                System.out.print("Введите регистрационный номер автомобиля: ");
                reg_znak = scanner.next() + " " + scanner.next();
            }

            if (reg_znak.isEmpty()) {
                Car car = new Car(marka, type, color, engine, wheels_quantity);
                return car;
            } else {
                try {
                    Car car = new Car(reg_znak, marka, type, color, engine, wheels_quantity);
                    return car;
                } catch (IllegalArgumentException e) {
                    System.out.println("Ошибка: " + e.getMessage());
                }
            }
        }
        else {
            System.out.print("Вы хотите задать регистрационный номер? (Y/N): ");
            String input = scanner.next();
            String reg_znak = "";
            if (input.equalsIgnoreCase("y")) {
                System.out.print("Введите регистрационный номер автомобиля: ");
                reg_znak = scanner.next();
                return  new Police(reg_znak, type, marka, color, engine, wheels_quantity, null);
            }
            else {
                return  new Car(marka, type, color, engine, wheels_quantity);
            }
        }
        return null;
    }

    public String getReg_znak() {
        return reg_znak;
    }

    public String getMarka() {
        return marka;
    }

    public String getColor() {
        return color;
    }

    public Engine getEngine() {
        return engine;
    }

    public Integer getWheels_quantity() {
        return wheels_quantity;
    }

    public CarType getType() {
        return type;
    }
}

class Engine{
    String serialNumber;
    String enginePower;
    String quantityCilindrs;
    String consumptionFuel;

    public Engine(String serialNumber, String enginePower, String quantityCilindrs, String consumptionFuel){
        this.serialNumber = serialNumber;
        this.enginePower = enginePower;
        this.quantityCilindrs = quantityCilindrs;
        this.consumptionFuel = consumptionFuel;
    }

    public void printInfo() {
        System.out.println("Серийный номер: " + this.serialNumber);
        System.out.println("Мощность двигателя: " + this.enginePower);
        System.out.println("Количество циллиндров: " + this.quantityCilindrs);
        System.out.println("Расход топлива: " + this.consumptionFuel);
        System.out.println("---------------------------------------------");
    }

    public static Engine makeEngine(){
        Scanner scanner = new Scanner(System.in);

        System.out.println("Введите серийный номер двигателя:");
        String serialNumber = scanner.nextLine();

        System.out.println("Введите мощность двигателя:");
        String enginePower = scanner.nextLine();

        System.out.println("Введите количество цилиндров двигателя:");
        String quantityCilindrs = scanner.nextLine();

        System.out.println("Введите расход топлива двигателя:");
        String consumptionFuel = scanner.nextLine();

        return new Engine(serialNumber, enginePower, quantityCilindrs, consumptionFuel);
    }


    public String getSerialNumber() {
        return serialNumber;
    }

    public String getEnginePower() {
        return enginePower;
    }

    public String getQuantityCilindrs() {
        return quantityCilindrs;
    }

    public String getConsumptionFuel() {
        return consumptionFuel;
    }

    // Сеттеры
    public void setSerialNumber(String serialNumber) {
        this.serialNumber = serialNumber;
    }

    public void setEnginePower(String enginePower) {
        this.enginePower = enginePower;
    }

    public void setQuantityCilindrs(String quantityCilindrs) {
        this.quantityCilindrs = quantityCilindrs;
    }

    public void setConsumptionFuel(String consumptionFuel) {
        this.consumptionFuel = consumptionFuel;
    }
}

