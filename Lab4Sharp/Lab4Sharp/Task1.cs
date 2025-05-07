using System;

class Date
{
    protected int day;
    protected int month;
    protected int year;

    public Date(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }

    public int Day
    {
        get => day;
        set => day = (value >= 1 && value <= 31) ? value : -1;
    }

    public int Month
    {
        get => month;
        set => month = (value >= 1 && value <= 12) ? value : -1;
    }

    public int Year
    {
        get => year;
        set => year = (value >= 1) ? value : -1;
    }

    public int Century => (year - 1) / 100 + 1;

    public bool IsValid()
    {
        if (day == -1 || month == -1 || year == -1) return false;
        int[] daysInMonth = { 31, IsLeapYear() ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        return day <= daysInMonth[month - 1];
    }

    private bool IsLeapYear() => (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);

    public string PrintFull()
    {
        string[] months = { "січня", "лютого", "березня", "квітня", "травня", "червня",
                           "липня", "серпня", "вересня", "жовтня", "листопада", "грудня" };
        return $"{day} {months[month - 1]} {year} року";
    }

    public string PrintShort() => $"{day:D2}.{month:D2}.{year}";

    public int DaysBetween(Date other)
    {
        DateTime d1 = new DateTime(year, month, day);
        DateTime d2 = new DateTime(other.year, other.month, other.day);
        return Math.Abs((d1 - d2).Days);
    }

    // Індексатор
    public Date this[int i]
    {
        get
        {
            if (!IsValid()) return new Date(-1, -1, -1);
            DateTime current = new DateTime(year, month, day);
            DateTime result = current.AddDays(i);
            return new Date(result.Day, result.Month, result.Year);
        }
    }

    // Оператор !
    public static bool operator !(Date date)
    {
        if (!date.IsValid()) return false;
        int[] daysInMonth = { 31, date.IsLeapYear() ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        return date.day < daysInMonth[date.month - 1];
    }

    // Оператори true і false
    public static bool operator true(Date date) => date.IsValid() && date.day == 1 && date.month == 1;
    public static bool operator false(Date date) => !date.IsValid() || date.day != 1 || date.month != 1;

    // Оператор &
    public static bool operator &(Date date1, Date date2) =>
        date1.day == date2.day && date1.month == date2.month && date1.year == date2.year;

    // Перетворення типів
    public static implicit operator string(Date date) =>
        date.IsValid() ? date.PrintShort() : "Невалідна дата";

    public static implicit operator Date(string dateStr)
    {
        string[] parts = dateStr.Split('.');
        if (parts.Length != 3) return new Date(-1, -1, -1);

        int.TryParse(parts[0], out int day);
        int.TryParse(parts[1], out int month);
        int.TryParse(parts[2], out int year);

        return new Date(day, month, year);
    }
}