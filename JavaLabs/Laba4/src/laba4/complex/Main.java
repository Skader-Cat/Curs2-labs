package laba4.complex;


public class Main {
    public static void main(String[] args) {
        Complex z1 = new Complex(2, 3);
        Complex z2 = new Complex(1, -2);

        Complex sum = z1.add(z2);
        Complex difference = z1.subtract(z2);
        Complex product = z1.multiply(z2);
        Complex quotient = z1.divide(z2);
        Complex conjugate = z1.conjugate();

        System.out.println("z1 = " + z1);
        System.out.println("z2 = " + z2);
        System.out.println("z1 + z2 = " + sum);
        System.out.println("z1 - z2 = " + difference);
        System.out.println("z1 * z2 = " + product);
        System.out.println("z1 / z2 = " + quotient);
        System.out.println("Conjugate of z1 = " + conjugate);
    }
}
