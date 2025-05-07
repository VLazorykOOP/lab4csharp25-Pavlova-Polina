using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== МЕНЮ ===\n1. Date\n2. VectorByte\n3. MatrixByte\n0. Вихід");
            Console.Write("Вибір: ");

            switch (Console.ReadLine())
            {
                case "1": Task1(); break;
                case "2": Task2(); break;
                case "3": Task3(); break;
                case "0": exit = true; break;
                default: Console.WriteLine("Невірний вибір"); break;
            }

            if (!exit)
            {
                Console.WriteLine("\nНатисніть клавішу...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    static void Task1()
    {
        Console.WriteLine("=== Тестування класу Date ===");

        var testDate = new Date(15, 5, 2023);
        var lastDayOfMonth = new Date(31, 5, 2023);
        var firstDayOfYear = new Date(1, 1, 2023);

        Console.WriteLine($"\nДати:");
        Console.WriteLine($"testDate: {testDate.PrintShort()}, валідна: {testDate.IsValid()}");
        Console.WriteLine($"lastDayOfMonth: {lastDayOfMonth.PrintShort()}, валідна: {lastDayOfMonth.IsValid()}");
        Console.WriteLine($"firstDayOfYear: {firstDayOfYear.PrintShort()}, валідна: {firstDayOfYear.IsValid()}");

        Console.WriteLine($"\nІндексатор: {testDate[10].PrintShort()}, {testDate[-5].PrintShort()}");
        Console.WriteLine($"Оператор !: {!testDate}, {!lastDayOfMonth}");

        Console.WriteLine("\nОператори true/false:");
        Console.WriteLine(firstDayOfYear ? "firstDayOfYear - початок року" : "firstDayOfYear - не початок року");
        Console.WriteLine(testDate ? "testDate - початок року" : "testDate - не початок року");

        var sameAsTestDate = new Date(15, 5, 2023);
        Console.WriteLine($"\nОператор &: {testDate & sameAsTestDate}, {testDate & firstDayOfYear}");

        string dateStr = testDate;
        Date fromStr = "25.12.2023";
        Console.WriteLine($"\nКонвертація типів:\nDate→string: {dateStr}\nstring→Date: {fromStr.PrintShort()}");
    }

    static void Task2()
    {
        Console.WriteLine("=== Тестування класу VectorByte ===");

        var v1 = new VectorByte();
        var v2 = new VectorByte(3);
        var v3 = new VectorByte(4, 5);

        Console.WriteLine("\nКонструктори:");
        Console.Write("Без параметрів: "); v1.Print();
        Console.Write("Розмір 3: "); v2.Print();
        Console.Write("Розмір 4, значення 5: "); v3.Print();

        Console.WriteLine($"\nКількість векторів: {VectorByte.Count()}");
        v2.Set(7);
        Console.Write("Після v2.Set(7): "); v2.Print();

        // Короткі тести основних операцій
        Console.WriteLine("\nОперації:");
        v2[1] = 10;
        Console.WriteLine($"Індексатор v2[1] = 10: {v2[1]}");

        v2.Set(3);
        v2++;
        Console.Write("v2++ -> "); v2.Print();

        var vZero = new VectorByte(3, 0);
        var vNonZero = new VectorByte(3, 1);
        Console.WriteLine($"vZero оцінюється як: {(vZero ? "true" : "false")}");
        Console.WriteLine($"vNonZero оцінюється як: {(vNonZero ? "true" : "false")}");

        var va = new VectorByte(3, 10);
        var vb = new VectorByte(3, 3);

        Console.WriteLine("\nАрифметичні операції:");
        Console.Write("va: "); va.Print();
        Console.Write("vb: "); vb.Print();
        Console.Write("va + vb: "); (va + vb).Print();
        Console.Write("va - vb: "); (va - vb).Print();
        Console.Write("va * vb: "); (va * vb).Print();
    }

    static void Task3()
    {
        Console.WriteLine("=== Тестування класу MatrixByte ===");

        // Створення тестових матриць
        var m1 = new MatrixByte();
        var m2 = new MatrixByte(2, 3);
        var m3 = new MatrixByte(2, 2, 5);

        Console.WriteLine("\nКонструктори:");
        Console.WriteLine("Без параметрів (1x1):");
        m1.Print();

        Console.WriteLine("\nРозмір 2x3 (заповнена нулями):");
        m2.Print();

        Console.WriteLine("\nРозмір 2x2, значення 5:");
        m3.Print();

        Console.WriteLine($"\nКількість матриць: {MatrixByte.Count()}");

        // Тестування методу Set
        m2.Set(3);
        Console.WriteLine("\nПісля m2.Set(3):");
        m2.Print();

        // Тестування індексаторів
        Console.WriteLine("\nІндексатори:");
        m2[0, 1] = 7;
        Console.WriteLine($"m2[0,1] = 7: {m2[0, 1]}");

        Console.WriteLine($"m2[3] (одновимірний індексатор): {m2[3]}");
        m2[4] = 9;
        Console.WriteLine("Після m2[4] = 9:");
        m2.Print();

        // Тестування унарних операцій
        Console.WriteLine("\nУнарні операції:");
        m3++;
        Console.WriteLine("m3++ (було 5 у всіх елементах):");
        m3.Print();

        // Тестування операторів true/false
        var mZero = new MatrixByte(2, 2, 0);
        Console.WriteLine("\nОператори true/false:");
        Console.WriteLine($"mZero оцінюється як: {(mZero ? "true" : "false")}");
        Console.WriteLine($"m3 оцінюється як: {(m3 ? "true" : "false")}");

        // Тестування унарного оператора !
        Console.WriteLine($"\nОператор !: !m3 = {!m3}");

        // Тестування унарного оператора ~
        Console.WriteLine("\nОператор ~:");
        var m4 = new MatrixByte(2, 2, 5);
        var m4Not = ~m4;
        Console.WriteLine("m4 (значення 5):");
        m4.Print();
        Console.WriteLine("~m4:");
        m4Not.Print();

        // Тестування бінарних операцій
        Console.WriteLine("\nБінарні операції:");
        var mA = new MatrixByte(2, 2, 10);
        var mB = new MatrixByte(2, 2, 3);

        Console.WriteLine("mA (значення 10):");
        mA.Print();
        Console.WriteLine("mB (значення 3):");
        mB.Print();

        Console.WriteLine("mA + mB:");
        (mA + mB).Print();

        Console.WriteLine("mA - mB:");
        (mA - mB).Print();

        Console.WriteLine("mA * mB (множення матриць):");
        (mA * mB).Print();

        // Тестування множення матриці на вектор
        Console.WriteLine("\nМноження матриці на вектор:");
        var mC = new MatrixByte(2, 3, 2);
        var vC = new VectorByte(3, 3);

        Console.WriteLine("mC (2x3, значення 2):");
        mC.Print();
        Console.Write("vC (розмір 3, значення 3): ");
        vC.Print();

        Console.Write("mC * vC = ");
        (mC * vC).Print();

        // Тестування множення на скаляр
        Console.WriteLine("\nmC * 4 (множення на скаляр):");
        (mC * 4).Print();

        // Тестування побітових операцій
        Console.WriteLine("\nПобітові операції:");
        var mBit1 = new MatrixByte(2, 2, 5);  // 00000101
        var mBit2 = new MatrixByte(2, 2, 3);  // 00000011

        Console.WriteLine("mBit1 | mBit2:");
        (mBit1 | mBit2).Print();

        Console.WriteLine("mBit1 & mBit2:");
        (mBit1 & mBit2).Print();

        // Тестування побітового зсуву
        Console.WriteLine("\nmBit1 << 1:");
        (mBit1 << 1).Print();

        Console.WriteLine("mBit1 >> 1:");
        (mBit1 >> 1).Print();

        // Тестування операцій порівняння
        Console.WriteLine("\nОперації порівняння:");
        var mEqual1 = new MatrixByte(2, 2, 5);
        var mEqual2 = new MatrixByte(2, 2, 5);
        var mNotEqual = new MatrixByte(2, 2, 7);

        Console.WriteLine($"mEqual1 == mEqual2: {mEqual1 == mEqual2}");
        Console.WriteLine($"mEqual1 != mNotEqual: {mEqual1 != mNotEqual}");
        Console.WriteLine($"mEqual1 < mNotEqual: {mEqual1 < mNotEqual}");
        Console.WriteLine($"mNotEqual > mEqual1: {mNotEqual > mEqual1}");
    }
}