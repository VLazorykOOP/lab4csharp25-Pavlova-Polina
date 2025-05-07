using System;

class VectorByte
{
    protected byte[] BArray;
    protected uint n;
    protected int codeError;
    protected static uint num_vec;

    // ������������
    public VectorByte() : this(1) { }

    public VectorByte(uint size)
    {
        n = size;
        BArray = new byte[n];
        codeError = 0;
        num_vec++;
    }

    public VectorByte(uint size, byte value) : this(size)
    {
        for (int i = 0; i < n; i++) BArray[i] = value;
    }

    ~VectorByte() => Console.WriteLine("���������� VectorByte ���������");

    // ������
    public void Input()
    {
        Console.WriteLine($"������ {n} ��������:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"[{i}]: ");
            byte.TryParse(Console.ReadLine(), out BArray[i]);
        }
    }

    public void Print()
    {
        if (n == 0) { Console.WriteLine("(�������)"); return; }

        for (int i = 0; i < n; i++) Console.Write(BArray[i] + " ");
        Console.WriteLine();
    }

    public void Set(byte value)
    {
        for (int i = 0; i < n; i++) BArray[i] = value;
    }

    public static uint Count() => num_vec;

    // ����������
    public uint Size => n;

    public int CodeError
    {
        get => codeError;
        set => codeError = value;
    }

    // ����������
    public byte this[int index]
    {
        get
        {
            if (index >= 0 && index < n) return BArray[index];
            codeError = -1;
            return 0;
        }
        set
        {
            if (index >= 0 && index < n) BArray[index] = value;
            else codeError = -1;
        }
    }

    // ����� ��������
    public static VectorByte operator ++(VectorByte v)
    {
        for (int i = 0; i < v.n; i++)
            if (v.BArray[i] < 255) v.BArray[i]++;
        return v;
    }

    public static VectorByte operator --(VectorByte v)
    {
        for (int i = 0; i < v.n; i++)
            if (v.BArray[i] > 0) v.BArray[i]--;
        return v;
    }

    // ��������� true � false
    public static bool operator true(VectorByte v)
    {
        if (v.n == 0) return false;
        foreach (byte item in v.BArray)
            if (item != 0) return true;
        return false;
    }

    public static bool operator false(VectorByte v)
    {
        if (v.n == 0) return true;
        foreach (byte item in v.BArray)
            if (item != 0) return false;
        return true;
    }

    // ������� �������� !
    public static bool operator !(VectorByte v) => v.n == 0;

    // ������� ����������� ~
    public static VectorByte operator ~(VectorByte v)
    {
        VectorByte result = new VectorByte(v.n);
        for (int i = 0; i < v.n; i++)
            result.BArray[i] = (byte)(~v.BArray[i]);
        return result;
    }

    // ���������� �� ������ ��������
    // ��������� ����� ��� ������� ��������
    private static VectorByte BinaryOp(VectorByte v1, VectorByte v2, Func<byte, byte, byte> operation)
    {
        uint maxSize = Math.Max(v1.n, v2.n);
        VectorByte result = new VectorByte(maxSize);

        // ������� �������� � ������� �������
        for (int i = 0; i < v1.n; i++)
            result.BArray[i] = v1.BArray[i];

        // �������� ��������
        for (int i = 0; i < v2.n && i < maxSize; i++)
            if (i < v1.n)
                result.BArray[i] = operation(result.BArray[i], v2.BArray[i]);
            else
                result.BArray[i] = v2.BArray[i];

        return result;
    }

    private static VectorByte ScalarOp(VectorByte v, byte scalar, Func<byte, byte, byte> operation)
    {
        VectorByte result = new VectorByte(v.n);
        for (int i = 0; i < v.n; i++)
            result.BArray[i] = operation(v.BArray[i], scalar);
        return result;
    }

    // ��������� (+)
    public static VectorByte operator +(VectorByte v1, VectorByte v2) =>
        BinaryOp(v1, v2, (a, b) => (byte)(a + b));

    public static VectorByte operator +(VectorByte v, byte scalar) =>
        ScalarOp(v, scalar, (a, b) => (byte)(a + b));

    // ³������� (-)
    public static VectorByte operator -(VectorByte v1, VectorByte v2) =>
        BinaryOp(v1, v2, (a, b) => a >= b ? (byte)(a - b) : (byte)0);

    public static VectorByte operator -(VectorByte v, byte scalar) =>
        ScalarOp(v, scalar, (a, b) => a >= b ? (byte)(a - b) : (byte)0);

    // �������� (*)
    public static VectorByte operator *(VectorByte v1, VectorByte v2) =>
        BinaryOp(v1, v2, (a, b) => (byte)(a * b));

    public static VectorByte operator *(VectorByte v, byte scalar) =>
        ScalarOp(v, scalar, (a, b) => (byte)(a * b));

    // ĳ����� (/)
    public static VectorByte operator /(VectorByte v1, VectorByte v2) =>
        BinaryOp(v1, v2, (a, b) => b != 0 ? (byte)(a / b) : (byte)0);

    public static VectorByte operator /(VectorByte v, byte scalar)
    {
        if (scalar == 0)
        {
            VectorByte result = new VectorByte(v.n);
            result.codeError = -2;
            return result;
        }
        return ScalarOp(v, scalar, (a, b) => (byte)(a / b));
    }

    // ������ (%)
    public static VectorByte operator %(VectorByte v1, VectorByte v2) =>
        BinaryOp(v1, v2, (a, b) => b != 0 ? (byte)(a % b) : (byte)0);

    public static VectorByte operator %(VectorByte v, byte scalar)
    {
        if (scalar == 0)
        {
            VectorByte result = new VectorByte(v.n);
            result.codeError = -2;
            return result;
        }
        return ScalarOp(v, scalar, (a, b) => (byte)(a % b));
    }

    // ������� ��� (|)
    public static VectorByte operator |(VectorByte v1, VectorByte v2) =>
        BinaryOp(v1, v2, (a, b) => (byte)(a | b));

    public static VectorByte operator |(VectorByte v, byte scalar) =>
        ScalarOp(v, scalar, (a, b) => (byte)(a | b));

    // ������� XOR (^)
    public static VectorByte operator ^(VectorByte v1, VectorByte v2) =>
        BinaryOp(v1, v2, (a, b) => (byte)(a ^ b));

    public static VectorByte operator ^(VectorByte v, byte scalar) =>
        ScalarOp(v, scalar, (a, b) => (byte)(a ^ b));

    // ������� � (&)
    public static VectorByte operator &(VectorByte v1, VectorByte v2) =>
        BinaryOp(v1, v2, (a, b) => (byte)(a & b));

    public static VectorByte operator &(VectorByte v, byte scalar) =>
        ScalarOp(v, scalar, (a, b) => (byte)(a & b));

    // �������� ����
    public static VectorByte operator >>(VectorByte v, int shift)
    {
        VectorByte result = new VectorByte(v.n);
        for (int i = 0; i < v.n; i++)
            result.BArray[i] = (byte)(v.BArray[i] >> shift);
        return result;
    }

    public static VectorByte operator <<(VectorByte v, int shift)
    {
        VectorByte result = new VectorByte(v.n);
        for (int i = 0; i < v.n; i++)
            result.BArray[i] = (byte)(v.BArray[i] << shift);
        return result;
    }

    // �������� ���������
    public static bool operator ==(VectorByte v1, VectorByte v2)
    {
        if (v1.n != v2.n) return false;
        for (int i = 0; i < v1.n; i++)
            if (v1.BArray[i] != v2.BArray[i]) return false;
        return true;
    }

    public static bool operator !=(VectorByte v1, VectorByte v2) => !(v1 == v2);

    public static bool operator >(VectorByte v1, VectorByte v2)
    {
        uint minSize = Math.Min(v1.n, v2.n);
        for (int i = 0; i < minSize; i++)
            if (v1.BArray[i] <= v2.BArray[i]) return false;
        return v1.n >= v2.n;
    }

    public static bool operator >=(VectorByte v1, VectorByte v2)
    {
        uint minSize = Math.Min(v1.n, v2.n);
        for (int i = 0; i < minSize; i++)
            if (v1.BArray[i] < v2.BArray[i]) return false;
        return v1.n >= v2.n;
    }

    public static bool operator <(VectorByte v1, VectorByte v2) => !(v1 >= v2);
    public static bool operator <=(VectorByte v1, VectorByte v2) => !(v1 > v2);

    // ����'������ �������������� ��� �������� ������ == �� !=
    public override bool Equals(object obj) =>
        obj is VectorByte v && this == v;

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + n.GetHashCode();
        foreach (byte item in BArray)
            hash = hash * 23 + item.GetHashCode();
        return hash;
    }
}