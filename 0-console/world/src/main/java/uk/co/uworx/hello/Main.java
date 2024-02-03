package uk.co.uworx.hello;

import java.text.SimpleDateFormat;
import java.time.LocalTime;
import java.util.Date;
import java.util.List;
import java.util.Random;
import java.util.function.Consumer;
import java.util.function.Predicate;
import java.util.function.Supplier;

interface SomeInterface
{
    void method1();

    // default method
    default void method2()
    {
        ;
    }
}

@FunctionalInterface
interface Writeable {
    void WriteLine(String message);
}

public class Main {
   public List<Integer> filter(List<Integer> source, Predicate<Integer> predicate)
    {
        return source.stream()
            .filter(predicate)
            .toList();
    }

    static String log(String message) {
        Date now = new Date();
        SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String formattedDateTime = dateFormat.format(now);
        return "[" + formattedDateTime + "] " + message;
    }

    static void writeLine(String message){
        System.out.println(log(message));
    }

    public void greet(String message) {
        int hour = LocalTime.now().getHour();
        if (hour < 10)
            System.out.println(String.format("Good morning %s", message));
        else if (hour < 18)
            System.out.println(String.format("Good day. %s", message));
        else
            System.out.println(String.format("Good evening. %s", message));
    }

    public static void main(String[] args) {
        Main m = new Main();
        m.greet("Khurram");

        Writeable writeable = Main::writeLine; // or lambda
        writeable.WriteLine("Hello World");

        //Consumer & Supplier
        Consumer<String> writeConsumer = Main::writeLine;   // Action<T>
        writeConsumer.accept("Hello World");

        Supplier<Integer> nextRandomNumber = () -> new Random().nextInt(100) + 1;   // Func<T1, T2>
        Consumer<String> writeConsumer2 = writeConsumer.andThen((s) -> System.out.println(s));  // Multicasting
        writeConsumer2.accept("Should be cool, Random Number: " + nextRandomNumber.get());  // Wiring up
    }
}