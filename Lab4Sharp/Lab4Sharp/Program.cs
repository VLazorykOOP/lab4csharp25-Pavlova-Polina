using System;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main()
    {
        // Встановлюємо кодування UTF-8 для консолі
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("Оберіть завдання:");
        Console.WriteLine("Завдання 1: Date (лабораторна робота 4)");
        // Тут можна додати інші завдання в майбутньому

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Викликаємо код для завдання 1 (клас Date)
                Task1();
                break;
            // Тут можна додати інші завдання в майбутньому
            default:
                Console.WriteLine("Невірний вибір");
                break;
        }
    }

    static void Task1()
    {
        // Тестовий код для класу Date
        Console.WriteLine("=== Тестування класу Date ===");

        // Створення тестових дат
        Date testDate = new Date(15, 5, 2023);
        Date lastDayOfMonth = new Date(31, 5, 2023);
        Date firstDayOfYear = new Date(1, 1, 2023);
        Date invalidDate = new Date(31, 2, 2023);

        Console.WriteLine("\nСтворені дати:");
        Console.WriteLine($"testDate: {testDate.PrintShort()}, валідна: {testDate.IsValid()}");
        Console.WriteLine($"lastDayOfMonth: {lastDayOfMonth.PrintShort()}, валідна: {lastDayOfMonth.IsValid()}");
        Console.WriteLine($"firstDayOfYear: {firstDayOfYear.PrintShort()}, валідна: {firstDayOfYear.IsValid()}");
        Console.WriteLine($"invalidDate: {invalidDate.PrintShort()}, валідна: {invalidDate.IsValid()}");

        // Тестування індексатора
        Console.WriteLine("\n--- Тестування індексатора ---");
        Console.WriteLine($"Дата через 10 днів після {testDate.PrintShort()}: {testDate[10].PrintShort()}");
        Console.WriteLine($"Дата за 5 днів до {testDate.PrintShort()}: {testDate[-5].PrintShort()}");

        // Тестування оператора !
        Console.WriteLine("\n--- Тестування оператора ! ---");
        Console.WriteLine($"!testDate (не останній день місяця): {!testDate}");
        Console.WriteLine($"!lastDayOfMonth (не останній день місяця): {!lastDayOfMonth}");

        // Тестування операторів true/false
        Console.WriteLine("\n--- Тестування операторів true/false ---");
        if (firstDayOfYear)
            Console.WriteLine("firstDayOfYear - це початок року");
        else
            Console.WriteLine("firstDayOfYear - це не початок року");

        if (testDate)
            Console.WriteLine("testDate - це початок року");
        else
            Console.WriteLine("testDate - це не початок року");

        // Тестування оператора &
        Console.WriteLine("\n--- Тестування оператора & ---");
        Date sameAsTestDate = new Date(15, 5, 2023);
        Console.WriteLine($"testDate & sameAsTestDate: {testDate & sameAsTestDate}");
        Console.WriteLine($"testDate & firstDayOfYear: {testDate & firstDayOfYear}");

        // Тестування перетворення типів
        Console.WriteLine("\n--- Тестування перетворення типів ---");
        string dateAsString = testDate;
        Console.WriteLine($"Date в string: {dateAsString}");

        Date stringAsDate = "25.12.2023";
        Console.WriteLine($"string в Date: {stringAsDate.PrintShort()}, валідна: {stringAsDate.IsValid()}");

        Date invalidStringDate = "неправильний-формат";
        Console.WriteLine($"Невалідний рядок в Date: {invalidStringDate.PrintShort()}, валідна: {invalidStringDate.IsValid()}");

        Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
        Console.ReadKey();
    }
}