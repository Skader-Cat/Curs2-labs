import java.util.Arrays;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class laba3 {

    public static void ex1(){
        double x = Math.PI / 15;
        System.out.format("%10s %15s %15s \n", "Аргумент", "Функция 1", "Функция 2");
        while (x < Math.PI){
            System.out.format("%10f %15.5e %15.7e \n", x, Math.sin(x), (Math.pow(Math.E, x) / x * Math.log(x)));
            x += Math.PI / 15;
        }
    }

    public static void ex2(){
        Scanner scan = new Scanner(System.in);


        Double[][] arr = new Double[][]{
                {1.9, 2.0, 2.1,},
                {2.3, 5.3, -2.6, 3.1},
                {3.6, -2.6, 7.3, -5.4, 5.5}
        };

        Double max = (double) Integer.MIN_VALUE;
        for (int i = 0; i < arr.length; i++) {
            for (int j = 0; j < arr[i].length; j++) {
                if(arr[i][j] < 0){
                    if(arr[i][j] > max)
                        max = arr[i][j];
                }
            }
        }
        System.out.format("Наибольшее отрицательное число: %.2f",max);
    }
    public static void ex3(){
        Double[][] arr = new Double[3][3];

        for (int i = 0; i < arr.length; i++) {
            for (int j = 0; j < arr[i].length; j++) {
                arr[i][j] = (double) Math.round(Math.random() * (200.0 - 1.0) + 1.0);
            }
        }
        System.out.println("До: ");
        for(Double[] subArr: arr){
            System.out.println(Arrays.toString(subArr));
        }

        for(Double[] subArr: arr){
            Arrays.sort(subArr);
        }

        System.out.println("После: ");
        for(Double[] subArr: arr){
            System.out.println(Arrays.toString(subArr));
        }
    }

    static class Circle{
        float x, y, r;

        public Circle(float x, float y, float r){
            this.x = x;
            this.y = y;
            this.r = r;
        }

        enum CircleState {
            TOUCHING, // окружности касаются в одной точке
            INTERSECTING, // окружности пересекаются в двух точках
            COINCIDENT, // окружности совпадают
            DISJOINT, // окружности не пересекаются
            EMBEDDED_SECOND, // вторая окружность вложена в первую
            EMBEDDED_FIRST, // первая окружность вложена во вторую
            UNKNOWN // неизвестная ситуация
        }

        public static int check_circle_states(Circle first_c, Circle second_c){
            double distance = Math.sqrt(Math.pow((second_c.x - first_c.x), 2)  + Math.pow((second_c.y - first_c.y), 2));
            if (distance == 0 && first_c.r == second_c.r) {
                System.out.println("Окружности совпадают.");
                return CircleState.COINCIDENT.ordinal();
            }
            else if (distance == first_c.r + second_c.r || distance == Math.abs(first_c.r - second_c.r)) {
                System.out.println("Окружности касаются в одной точке.");
                return CircleState.TOUCHING.ordinal();
            }
            else if (distance < first_c.r + second_c.r && distance > Math.abs(first_c.r - second_c.r)) {
                System.out.println("Окружности пересекаются в двух точках.");
                return CircleState.INTERSECTING.ordinal();
            }
            else if ((distance > first_c.r + second_c.r || distance < Math.abs(first_c.r - second_c.r)) && distance > 0.0) {

                System.out.println("Окружности не пересекаются.");
                return CircleState.DISJOINT.ordinal();
            }
            else if (second_c.r < first_c.r) {
                System.out.println("Вторая окружность вложена в первую.");
                return CircleState.EMBEDDED_SECOND.ordinal();
            }
            else if (first_c.r < second_c.r) {
                System.out.println("Первая окружность вложена во вторую.");
                return CircleState.EMBEDDED_FIRST.ordinal();
            }
            else {
                System.out.println("Неизвестная ситуация.");
                return CircleState.UNKNOWN.ordinal();
            }
        }
    }
    public static void ex5(){
        Scanner cin = new Scanner(System.in);
        System.out.format("Введите %2s %1s %1s: \n %8s", "x", "y", "r", ">>");
        Circle first_c = new Circle(cin.nextFloat(), cin.nextFloat(), cin.nextFloat());
        System.out.format("Введите %2s %1s %1s: \n %8s", "x", "y", "r", ">>");
        Circle second_c = new Circle(cin.nextFloat(), cin.nextFloat(), cin.nextFloat());

        System.out.println(Circle.check_circle_states(first_c, second_c));
    }

    public static double leftRectangleIntegration(double[] x, double[] y, double h) {
        double integral = 0.0;
        for (int i = 0; i < x.length - 1; i++) {
            integral += y[i] * h;
        }
        return integral;
    }

    public static void ex6(float range_a, float range_b) {
        double a = range_a;
        double b = range_b;
        int n = 10000;
        double h = (b - a) / n;
        double[] x = new double[n + 1];
        double[] y = new double[n + 1];


        for (int i = 0; i <= n; i++) {
            x[i] = a + i * h;
            y[i] = Math.exp(x[i]) - Math.pow(x[i], 3);
        }


        double integral = leftRectangleIntegration(x, y, h);


        System.out.println("Значение интеграла: " + integral);
    }

    public static void ex7(){
        Scanner cin = new Scanner(System.in);
        System.out.println("Введите десятичное число: ");
        int input = cin.nextInt();
        System.out.println("Введите основание системы, в которую хотите перевести: ");
        int base = cin.nextInt();
        StringBuilder ans = new StringBuilder();
        if(base < 2 || base > 8){
            System.out.println("Неверное основание системы счисления. Разрешены только основания от 2 до 8");
        }
        else {
            String etalon = Integer.toString(input, base);
            while (input > 0){
                ans.append(input % base);
                input = Math.round(input / base);
            }
            System.out.format("Ответ, полученный алгоритмом: %s\nОтвет, полученный встроенным методом: %s",
                    ans.reverse().toString(),
                    etalon
            );
        }
    }

    public static void ex8(double[] coeffs, double x) {
        int n = coeffs.length;
        double result = coeffs[n-1];
        for (int i = n-2; i >= 0; i--) {
            result = result * x + coeffs[i];
        }
        System.out.println(result);
    }

    public static void ex9(){
        Scanner cin = new Scanner(System.in);
        String regex_federal = "^(\\+7|8)?(\\s|-)?(\\(\\d{3}\\)|\\d{3})(\\s|-)?\\d{3}(\\s|-)?\\d{2}(\\s|-)?\\d{2}$";
        String regex_municipal = "^(2|3)?(\\s|-|)?(\\(\\d{2}\\)|\\d{2})(\\s|-)?\\d{2}(\\s|-)?\\d{2}$";
        System.out.println("Введите номер: ");
        String input_string = cin.nextLine();
        if(input_string.matches(regex_federal)){
            System.out.format("Федеральный номер %s записан верно!\n", input_string);
        }
        else if(input_string.matches(regex_municipal)){
            System.out.format("Муниципальный номер %s записан верно!\n", input_string);
        }
        else{
            System.out.println("Ввёденный номер не соответсвует ни одному из форматов");
        }
    }

    public static void ex10(){
        Scanner cin = new Scanner(System.in);
        String regex_federal = "(\\+7|8)?(\\s|-)?(\\(\\d{3}\\)|\\d{3})(\\s|-)?\\d{3}(\\s|-)?\\d{2}(\\s|-)?\\d{3}";
        String regex_municipal = "(2|3)?(\\s|-|)?(\\(\\d{2}\\)|\\d{2})(\\s|-)?\\d{2}(\\s|-)?\\d{3}(\\s|)";
        System.out.println("Введите строку: ");

        StringBuilder sb = new StringBuilder();

        while (cin.hasNextLine()) {
            String line = cin.nextLine();
            if (line.isEmpty()) {
                break;
            }
            sb.append(line);
        }

        String input_string = sb.toString();

        Pattern pattern = Pattern.compile(regex_federal);
        Matcher matcher = pattern.matcher(input_string);
        while (matcher.find()){
            System.out.println("В строке найден федеральный номер: " + matcher.group());
            input_string = input_string.replaceAll(matcher.group(), "");
        }
        pattern = Pattern.compile(regex_municipal);
        matcher = pattern.matcher(input_string);

        while (matcher.find()){
            System.out.println("В строке найден муниципальный номер: " + matcher.group());
        }


    }

    public static void main(String[] args){
        System.out.println("Введите номер задания: ");
        Scanner cin = new Scanner(System.in);
        int ex = cin.nextInt();
        switch (ex) {
            case 1 -> ex1();
            case 2 -> ex2();
            case 3 -> ex3();
            case 5 -> ex5();
            case 6 -> ex6(0, 4);
            case 7 -> ex7();
            case 8 -> {
                System.out.println("Введите количество элементов, которые хотите сгенерировать: ");
                Scanner scan = new Scanner(System.in);
                int n = scan.nextInt();
                double[] input = new double[n];
                for (int i = 0; i < input.length; i++) {
                    input[i] = (double) Math.round(Math.random() * (200.0 + 200.0) - 200.0);
                }
                System.out.print("Матрица сформирована: \n[");

                for(int j = 0; j < input.length-1; j++){
                    System.out.format("%f; ", input[j]);
                }
                System.out.println(input[input.length-1] + "]");
                ex8(input, 6);
            }
            case 9 -> ex9();
            case 10 -> ex10();
        }
    }
}

