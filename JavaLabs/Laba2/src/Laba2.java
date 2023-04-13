
import java.util.Scanner;

public class Laba2 {
    public static void ex1() {
        System.out.println("Задача 1: ");
        Number[][] type_array = new Number[][]{
                {Byte.MIN_VALUE, Byte.MAX_VALUE},
                {Short.MIN_VALUE, Short.MAX_VALUE},
                {Integer.MIN_VALUE, Integer.MAX_VALUE},
                {Long.MIN_VALUE, Long.MAX_VALUE},
                {Float.MIN_VALUE, Float.MAX_VALUE},
                {Double.MIN_VALUE, Double.MAX_VALUE}
        };
        String[] string_array = new String[]{"Byte", "Short", "Integer", "Long", "Float", "Double"};
        System.out.format("%10s %15s %30s \n", "Type:", "Min Value:", "Max Value:");
        System.out.println("_".repeat(100));
        for (int i = 0; i < type_array.length; i++) {
            System.out.format("%10s: %10s %40s \n", string_array[i], type_array[i][0].toString(), type_array[i][1].toString());
        }
    }

    public static  void ex2(){
        System.out.println("Задача 2: ");
        int[] arr = {-3, -3};
        int result = 1;
        int counter = 0;
        for(int num : arr){
            if(num < 0){
                result *= Math.abs(num);
                counter += 1;
            }
        }
        System.out.println("Среднее геометрическое отрицательных чисел: " + Math.pow(result, 1.0/counter) + " Кол-во отрицательных чисел: " + counter);
    }

    public static  void  ex3(double r, double R, double x, double y){
        System.out.println("Задача 3: ");
        double distance = Math.sqrt(Math.pow(x, 2) + Math.pow(y, 2));
        if(distance <= r){
            System.out.println("Тревога!");
        }
        else if((r < distance) && (distance <= R)){
            System.out.println("Обнаружен!");
        }
        else {
            System.out.println("Не обнаружен");
        }
    }
    public static void ex4(){
        System.out.println("Задача 4: ");
        Scanner scanner = new Scanner(System.in);
        System.out.println("Введите r: ");
        double r = scanner.nextDouble();
        System.out.println("Введите R: ");
        double R = scanner.nextDouble();
        System.out.println("Введите x: ");
        double x = scanner.nextDouble();
        System.out.println("Введите y: ");
        double y = scanner.nextDouble();
        ex3(r,R,x,y);
    }

    public static  void ex5(int num){
        System.out.println("Задача 5: ");
        System.out.println("Введите целое десятичное число для преобразования: ");
        //Scanner scanner = new Scanner(System.in);
        //int num = scanner.nextInt();
        System.out.println("В двоичной системе: " + Integer.toBinaryString(num));
        System.out.println("В восьмеричной системе: " + Integer.toOctalString(num));
        System.out.println("В шестнадцатеричной системе: " + Integer.toHexString(num));
    }

    public  static void ex6(){
        System.out.println("Задача 6: ");
        String code = "4";
        String[] codes = new String[18];
        //String[] money_codes = new String[]{"20a0", "20b0"};
        codes[16] = "20a0";
        codes[17] = "20b0";
        String[] numbers = new String[16];
        String[] hex = new String[]{"a", "b", "c", "d","e","f"};
        int tmp;
        for(int i = 0; i < 16; i++){
            if(i < 10){
                code += (String.valueOf(i) + "0");
                numbers[i] = String.valueOf(i);
            }
            else{
                code += (hex[i % 10] + "0");
                numbers[i] = String.valueOf(hex[i % 10]);
            }
            codes[i] = code;
            code = "4";
        }


        for(int i = 0; i < 18; i++){
            if(i == 0){
                System.out.println("Кириллица: ");
            }
            else if(i == 16){
                System.out.println("\nДенежные единицы: ");
            }
            for(int j = 0; j < 16; j++){
                tmp = ((Integer.parseInt(codes[i],16)) + (Integer.parseInt(numbers[j],16)));
                System.out.print((char)tmp + " ");
            }
            System.out.println();
        }
    }
    public static void  ex7(String input){
        System.out.println("Задача 7: ");
        int string_size = input.length();
        int digits = string_size - input.replaceAll("[0-9]", "").length();
        int lower = string_size - input.replaceAll("[A-ZА-Я]", "").length();
        int higher = string_size - input.replaceAll("[a-zа-я]", "").length();
        System.out.println("Кол-во символов: " + string_size + "\nКол-во букв: " + (string_size - digits) +
                "\nКол-во цифр: " + digits + "\nКол-во заглавных букв: " + higher + "\nКол-во прописных букв: " + lower
        );
    }

    public static void  ex9(String input, String substr){
        System.out.println("Задача 9: ");
        int sdvig = 0;
        int counter = 0;
        int tmp = 0;
        while (true) {
            tmp = input.indexOf(substr, sdvig);
            if (tmp != -1) {
                sdvig += (tmp + substr.length());
                counter++;
            }
            else{
                System.out.println("Подстрока '"+substr+"' встречается " + counter + " раз.");
                break;
            }
            System.out.println("Подстрока '"+substr+"' встречается " + counter + " раз.");
        }
    }
    public  static void ex10(String str){
        System.out.println("Задача 10: ");
        char tmp;
        char tmp2;
        String[] result = new String[str.length()];
        for(int i = 0; i < result.length; i++){
            tmp = str.charAt(0);
            str = str.substring(1, str.length());
            str += tmp;
            result[i] = str;
        }
        result[str.length()-1] = str;
        for (String obj: result){
            System.out.println(obj);
        }
    }
    public static void main(String args[]){
        //ex1();
        //ex2();
        //ex3(3, 6, 4,4);
        //ex4();
        ex5(Integer.parseInt(args[0]));
        ex6();
        ex7("Abc834452lll39");
        ex9("aaaaddaaff", "aa");
        ex10("normal");
    }
}
