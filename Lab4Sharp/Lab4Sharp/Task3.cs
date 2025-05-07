using System;

class MatrixByte
{
    protected byte[,] ByteArray;
    protected uint n, m;
    protected int codeError;
    protected static int num_vec;

    // Конструктори
    public MatrixByte()
    {
        n = m = 1;
        ByteArray = new byte[n, m];
        codeError = 0;
        num_vec++;
    }

    public MatrixByte(uint rows, uint cols)
    {
        n = rows; m = cols;
        ByteArray = new byte[n, m];
        codeError = 0;
        num_vec++;
    }

    public MatrixByte(uint rows, uint cols, byte value) : this(rows, cols)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                ByteArray[i, j] = value;
    }

    ~MatrixByte() => Console.WriteLine("Деструктор MatrixByte викликано");

    // Методи
    public void Input()
    {
        Console.WriteLine($"Введіть елементи матриці {n}x{m}:");
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                Console.Write($"[{i},{j}]: ");
                byte.TryParse(Console.ReadLine(), out ByteArray[i, j]);
            }
    }

    public void Print()
    {
        if (n == 0 || m == 0) { Console.WriteLine("(Порожня матриця)"); return; }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
                Console.Write($"{ByteArray[i, j],3} ");
            Console.WriteLine();
        }
    }

    public void Set(byte value)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                ByteArray[i, j] = value;
    }

    public static int Count() => num_vec;

    // Властивості
    public uint Rows => n;
    public uint Cols => m;

    public int CodeError
    {
        get => codeError;
        set => codeError = value;
    }

    // Індексатори
    public byte this[int i, int j]
    {
        get
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                return ByteArray[i, j];
            codeError = -1;
            return 0;
        }
        set
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                ByteArray[i, j] = value;
            else
                codeError = -1;
        }
    }

    public byte this[int k]
    {
        get
        {
            if (m == 0) { codeError = -1; return 0; }
            int i = k / (int)m;
            int j = k % (int)m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                return ByteArray[i, j];
            codeError = -1;
            return 0;
        }
        set
        {
            if (m == 0) { codeError = -1; return; }
            int i = k / (int)m;
            int j = k % (int)m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                ByteArray[i, j] = value;
            else
                codeError = -1;
        }
    }

    // Унарні операції
    public static MatrixByte operator ++(MatrixByte matrix)
    {
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                if (matrix.ByteArray[i, j] < 255)
                    matrix.ByteArray[i, j]++;
        return matrix;
    }

    public static MatrixByte operator --(MatrixByte matrix)
    {
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                if (matrix.ByteArray[i, j] > 0)
                    matrix.ByteArray[i, j]--;
        return matrix;
    }

    // Оператори true і false
    public static bool operator true(MatrixByte matrix)
    {
        if (matrix.n == 0 || matrix.m == 0)
            return false;

        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                if (matrix.ByteArray[i, j] != 0)
                    return true;
        return false;
    }

    public static bool operator false(MatrixByte matrix)
    {
        if (matrix.n == 0 || matrix.m == 0)
            return true;

        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                if (matrix.ByteArray[i, j] != 0)
                    return false;
        return true;
    }

    // Логічний оператор !
    public static bool operator !(MatrixByte matrix) =>
        matrix.n == 0 || matrix.m == 0;

    // Побітове заперечення ~
    public static MatrixByte operator ~(MatrixByte matrix)
    {
        MatrixByte result = new MatrixByte(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.ByteArray[i, j] = (byte)(~matrix.ByteArray[i, j]);
        return result;
    }

    // Допоміжні методи для бінарних операцій
    private static MatrixByte BinaryOp(MatrixByte m1, MatrixByte m2, Func<byte, byte, byte> operation)
    {
        // Якщо розміри різні, повертаємо першу матрицю (за умовою)
        if (m1.n != m2.n || m1.m != m2.m)
            return new MatrixByte(m1.n, m1.m) { ByteArray = (byte[,])m1.ByteArray.Clone() };

        MatrixByte result = new MatrixByte(m1.n, m1.m);
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                result.ByteArray[i, j] = operation(m1.ByteArray[i, j], m2.ByteArray[i, j]);

        return result;
    }

    private static MatrixByte ScalarOp(MatrixByte matrix, byte scalar, Func<byte, byte, byte> operation)
    {
        MatrixByte result = new MatrixByte(matrix.n, matrix.m);
        for (int i = 0; i < matrix.n; i++)
            for (int j = 0; j < matrix.m; j++)
                result.ByteArray[i, j] = operation(matrix.ByteArray[i, j], scalar);
        return result;
    }

    // Додавання (+)
    public static MatrixByte operator +(MatrixByte m1, MatrixByte m2) =>
        BinaryOp(m1, m2, (a, b) => (byte)(a + b));

    public static MatrixByte operator +(MatrixByte m, byte scalar) =>
        ScalarOp(m, scalar, (a, b) => (byte)(a + b));

    // Віднімання (-)
    public static MatrixByte operator -(MatrixByte m1, MatrixByte m2) =>
        BinaryOp(m1, m2, (a, b) => a >= b ? (byte)(a - b) : (byte)0);

    public static MatrixByte operator -(MatrixByte m, byte scalar) =>
        ScalarOp(m, scalar, (a, b) => a >= b ? (byte)(a - b) : (byte)0);

    // Множення (*) - для матриць має особливу реалізацію
    public static MatrixByte operator *(MatrixByte m1, MatrixByte m2)
    {
        if (m1.m != m2.n) // Перевірка можливості множення
            return new MatrixByte(m1.n, m1.m) { ByteArray = (byte[,])m1.ByteArray.Clone() };

        MatrixByte result = new MatrixByte(m1.n, m2.m);
        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m2.m; j++)
            {
                byte sum = 0;
                for (int k = 0; k < m1.m; k++)
                    sum += (byte)(m1.ByteArray[i, k] * m2.ByteArray[k, j]);
                result.ByteArray[i, j] = sum;
            }
        return result;
    }

    // Множення матриці на вектор
    public static VectorByte operator *(MatrixByte m, VectorByte v)
    {
        if (m.m != v.Size) // Перевірка можливості множення
            return new VectorByte(m.n);

        VectorByte result = new VectorByte(m.n);
        for (int i = 0; i < m.n; i++)
        {
            byte sum = 0;
            for (int j = 0; j < m.m; j++)
                sum += (byte)(m.ByteArray[i, j] * v[j]);
            result[i] = sum;
        }
        return result;
    }

    public static MatrixByte operator *(MatrixByte m, byte scalar) =>
        ScalarOp(m, scalar, (a, b) => (byte)(a * b));

    // Ділення (/)
    public static MatrixByte operator /(MatrixByte m1, MatrixByte m2) =>
        BinaryOp(m1, m2, (a, b) => b != 0 ? (byte)(a / b) : (byte)0);

    public static MatrixByte operator /(MatrixByte m, byte scalar)
    {
        if (scalar == 0)
        {
            MatrixByte result = new MatrixByte(m.n, m.m);
            result.codeError = -2; // Код помилки для ділення на нуль
            return result;
        }
        return ScalarOp(m, scalar, (a, b) => (byte)(a / b));
    }

    // Остача (%)
    public static MatrixByte operator %(MatrixByte m1, MatrixByte m2) =>
        BinaryOp(m1, m2, (a, b) => b != 0 ? (byte)(a % b) : (byte)0);

    public static MatrixByte operator %(MatrixByte m, byte scalar)
    {
        if (scalar == 0)
        {
            MatrixByte result = new MatrixByte(m.n, m.m);
            result.codeError = -2; // Код помилки для ділення на нуль
            return result;
        }
        return ScalarOp(m, scalar, (a, b) => (byte)(a % b));
    }

    // Побітове ИЛИ (|)
    public static MatrixByte operator |(MatrixByte m1, MatrixByte m2) =>
        BinaryOp(m1, m2, (a, b) => (byte)(a | b));

    public static MatrixByte operator |(MatrixByte m, byte scalar) =>
        ScalarOp(m, scalar, (a, b) => (byte)(a | b));

    // Побітове XOR (^)
    public static MatrixByte operator ^(MatrixByte m1, MatrixByte m2) =>
        BinaryOp(m1, m2, (a, b) => (byte)(a ^ b));

    public static MatrixByte operator ^(MatrixByte m, byte scalar) =>
        ScalarOp(m, scalar, (a, b) => (byte)(a ^ b));

    // Побітове И (&)
    public static MatrixByte operator &(MatrixByte m1, MatrixByte m2) =>
        BinaryOp(m1, m2, (a, b) => (byte)(a & b));

    public static MatrixByte operator &(MatrixByte m, byte scalar) =>
        ScalarOp(m, scalar, (a, b) => (byte)(a & b));

    // Побітовий зсув
    public static MatrixByte operator >>(MatrixByte m, sbyte shift)
    {
        MatrixByte result = new MatrixByte(m.n, m.m);
        for (int i = 0; i < m.n; i++)
            for (int j = 0; j < m.m; j++)
                result.ByteArray[i, j] = (byte)(m.ByteArray[i, j] >> shift);
        return result;
    }

    public static MatrixByte operator <<(MatrixByte m, sbyte shift)
    {
        MatrixByte result = new MatrixByte(m.n, m.m);
        for (int i = 0; i < m.n; i++)
            for (int j = 0; j < m.m; j++)
                result.ByteArray[i, j] = (byte)(m.ByteArray[i, j] << shift);
        return result;
    }

    // Операції порівняння
    public static bool operator ==(MatrixByte m1, MatrixByte m2)
    {
        if (m1.n != m2.n || m1.m != m2.m) return false;

        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                if (m1.ByteArray[i, j] != m2.ByteArray[i, j])
                    return false;
        return true;
    }

    public static bool operator !=(MatrixByte m1, MatrixByte m2) => !(m1 == m2);

    public static bool operator >(MatrixByte m1, MatrixByte m2)
    {
        if (m1.n != m2.n || m1.m != m2.m) return false;

        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                if (m1.ByteArray[i, j] <= m2.ByteArray[i, j])
                    return false;
        return true;
    }

    public static bool operator >=(MatrixByte m1, MatrixByte m2)
    {
        if (m1.n != m2.n || m1.m != m2.m) return false;

        for (int i = 0; i < m1.n; i++)
            for (int j = 0; j < m1.m; j++)
                if (m1.ByteArray[i, j] < m2.ByteArray[i, j])
                    return false;
        return true;
    }

    public static bool operator <(MatrixByte m1, MatrixByte m2) => !(m1 >= m2);
    public static bool operator <=(MatrixByte m1, MatrixByte m2) => !(m1 > m2);

    // Обов'язкове перевизначення для коректної роботи == та !=
    public override bool Equals(object obj) =>
        obj is MatrixByte m && this == m;

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + n.GetHashCode();
        hash = hash * 23 + m.GetHashCode();

        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                hash = hash * 23 + ByteArray[i, j].GetHashCode();

        return hash;
    }
}