package laba4.car;

public final class Police extends Car {
    private Integer endurance;
    private String reg_znak;

    public Police(String reg_znak, CarType type, String marka, String color, Engine engine, Integer wheels_quantity, Integer endurance) {
        super(verifyRegZnak(reg_znak), marka, type, color, engine, wheels_quantity);
        this.endurance = endurance;
    }

    public static String verifyRegZnak(String reg_znak){
        String regex = "[АВЕКМНОРСТУХ]\\d{4}[АВЕКМНОРСТУХ]{2}";  //A1223KE
        if(reg_znak != null && reg_znak.matches(regex)){
            return reg_znak;
        }
        else {
            throw new IllegalArgumentException("Регистрационный знак не соответствует шаблону");
        }
    }


    public static Police createPoliceCar(CarType type, Integer endurance) {
        Car car = Car.createCar(type);
        Police police = new Police(car.getReg_znak(), car.getType(), car.getMarka(), car.getColor(), car.getEngine(), car.getWheels_quantity(), endurance);
        police.endurance = endurance;
        return police;
    }

    public void printInfo() {
        super.printInfo();
        System.out.println("Прочность " + this.endurance);
        System.out.println("Регистрационный номер " + this.reg_znak);
    }
}
