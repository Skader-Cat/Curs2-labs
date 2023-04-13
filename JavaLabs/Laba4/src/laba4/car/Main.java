package laba4.car;

import java.util.Scanner;

public class Main {
    static Engine makeEngine(){
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
    public static void main(String[] args) {

    }
}