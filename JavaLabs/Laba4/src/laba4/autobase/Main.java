package laba4.autobase;

import laba4.car.CarType;

import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner cin = new Scanner(System.in);
        System.out.println("Сколько машин в автопарке?");
        Autobase atp = new Autobase(cin.nextInt());
        atp.printBaseCarInfo();


        var bus = atp.getFromCarsByType(CarType.АВТОБУС, atp.cars_on_base);
        atp.appendToReys(bus);
        var car = atp.getFromCarsByType(CarType.ЛЕГКОВОЙ, atp.cars_on_base);
        atp.appendToRefund(car);
        var police = atp.getFromCarsByType(CarType.ПОЛИЦЕЙСКИЙ, atp.cars_on_base);
        atp.appendToRefund(police);

        atp.printBaseCarInfo();
        atp.printRefundCarInfo();
        atp.printReysCarInfo();

        atp.appendToReys(atp.getFromCarsByType(CarType.ПОЛИЦЕЙСКИЙ, atp.cars_on_refund));
        atp.appendToBase(atp.getFromCarsByType(CarType.АВТОБУС, atp.cars_on_reys));

        atp.printBaseCarInfo();
        atp.printRefundCarInfo();
        atp.printReysCarInfo();
    }
}
