package laba4.complex;

public class Complex {
    private double real; // действительная часть
    private double imaginary; // мнимая часть

    public Complex(double real, double imaginary) {
        this.real = real;
        this.imaginary = imaginary;
    }

    public double getReal() {
        return real;
    }

    public double getImaginary() {
        return imaginary;
    }

    public Complex add(Complex other) {
        double real = this.real + other.real;
        double imaginary = this.imaginary + other.imaginary;
        return new Complex(real, imaginary);
    }

    public Complex subtract(Complex other) {
        double real = this.real - other.real;
        double imaginary = this.imaginary - other.imaginary;
        return new Complex(real, imaginary);
    }

    public Complex multiply(Complex other) {
        double real = this.real * other.real - this.imaginary * other.imaginary;
        double imaginary = this.real * other.imaginary + this.imaginary * other.real;
        return new Complex(real, imaginary);
    }

    public Complex divide(Complex other) {
        double denominator = Math.pow(other.real, 2) + Math.pow(other.imaginary, 2);
        double real = (this.real * other.real + this.imaginary * other.imaginary) / denominator;
        double imaginary = (this.imaginary * other.real - this.real * other.imaginary) / denominator;
        return new Complex(real, imaginary);
    }

    public Complex conjugate() {
        return new Complex(this.real, -this.imaginary);
    }

    public boolean equals(Complex other) {
        return this.real == other.real && this.imaginary == other.imaginary;
    }

    public double modulus() {
        return Math.sqrt(Math.pow(this.real, 2) + Math.pow(this.imaginary, 2));
    }

    public double argument() {
        return Math.atan2(this.imaginary, this.real);
    }

    @Override
    public String toString() {
        if (imaginary >= 0) {
            return String.format("%.2f + %.2fi", real, imaginary);
        } else {
            return String.format("%.2f - %.2fi", real, -imaginary);
        }
    }

    public static Complex exp(Complex z) {
        double realExp = Math.exp(z.real);
        double cosImaginary = Math.cos(z.imaginary);
        double sinImaginary = Math.sin(z.imaginary);

        return new Complex(realExp * cosImaginary, realExp * sinImaginary);
    }

    public static Complex sin(Complex z) {
        double expReal = Math.exp(z.real);
        double expNegReal = Math.exp(-z.real);
        double cosImaginary = Math.cos(z.imaginary);
        double sinImaginary = Math.sin(z.imaginary);

        double real = sinImaginary * (expReal - expNegReal) / 2;
        double imag = cosImaginary * (expReal + expNegReal) / 2;

        return new Complex(real, imag);
    }

    public Complex negate() {
        return new Complex(-real, -imaginary);
    }

    // Метод log
    public Complex log() {
        double modulus = Math.sqrt(real * real + imaginary * imaginary);
        double argument = Math.atan2(imaginary, real);

        if (modulus == 0) {
            // log(0) is undefined
            return new Complex(Double.NaN, Double.NaN);
        } else {
            return new Complex(Math.log(modulus), argument);
        }
    }

    public static Complex cos(Complex z) {
        double expReal = Math.exp(z.real);
        double expNegReal = Math.exp(-z.real);
        double cosImaginary = Math.cos(z.imaginary);
        double sinImaginary = Math.sin(z.imaginary);

        double real = cosImaginary * (expReal + expNegReal) / 2;
        double imag = -sinImaginary * (expReal - expNegReal) / 2;

        return new Complex(real, imag);
    }

    public static Complex tan(Complex z) {
        return sin(z).divide(cos(z));
    }

    public Complex arctan() {
        double real = Math.atan2(2 * this.real, 1 - Math.pow(this.real, 2) - Math.pow(this.imaginary, 2)) / 2;
        double imaginary = Math.log((Math.pow(this.real - 1, 2) + Math.pow(this.imaginary, 2)) /
                (Math.pow(this.real + 1, 2) + Math.pow(this.imaginary, 2))) / 4;
        return new Complex(real, imaginary);
    }

    public static Complex sinh(Complex z) {
        return exp(z).subtract(exp(z.negate())).divide(new Complex(2, 0));
    }

    public static Complex cosh(Complex z) {
        return exp(z).add(exp(z.negate())).divide(new Complex(2, 0));
    }

    public static Complex tanh(Complex z) {
        return sinh(z).divide(cosh(z));
    }

    public static Complex coth(Complex z) {
        return cosh(z).divide(sinh(z));
    }
}