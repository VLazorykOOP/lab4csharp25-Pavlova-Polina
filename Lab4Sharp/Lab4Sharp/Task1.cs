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
        set
        {
            if (value >= 1 && value <= 31)
                day = value;
            else
                day = -1;
        }
    }

    public int Month
    {
        get => month;
        set
        {
            if (value >= 1 && value <= 12)
                month = value;
            else
                month = -1;
        }
    }

    public int Year
    {
        get => year;
        set
        {
            if (value >= 1)
                year = value;
            else
                year = -1;
        }
    }

    public int Century => (year - 1) / 100 + 1;

    public bool IsValid()
    {
        if (day == -1 || month == -1 || year == -1)
            return false;

        int[] daysInMonth = { 31, IsLeapYear() ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        return day <= daysInMonth[month - 1];
    }

    private bool IsLeapYear()
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    public string PrintFull()
    {
        string[] months = { "січня", "лютого", "березня", "квітня", "травня", "червня", "липня", "серпня", "вересня", "жовтня", "листопада", "грудня" };
        return $"{day} {months[month - 1]} {year} року";
    }

    public string PrintShort()
    {
        return $"{day:D2}.{month:D2}.{year}";
    }

    public int DaysBetween(Date other)
    {
        DateTime d1 = new DateTime(this.year, this.month, this.day);
        DateTime d2 = new DateTime(other.year, other.month, other.day);
        return Math.Abs((d1 - d2).Days);
    }

    // Нові методи для лабораторної роботи 4

    // 1. Індексатор, що дозволяє визначити дату i-того дня щодо встановленої дати
    public Date this[int i]
    {
        get
        {
            if (!IsValid())
                return new Date(-1, -1, -1);

            DateTime current = new DateTime(year, month, day);
            DateTime result = current.AddDays(i);
            return new Date(result.Day, result.Month, result.Year);
        }
    }

    // 2. Перевантаження операції !: повертає true, якщо дата не є останнім днем місяця
    public static bool operator !(Date date)
    {
        if (!date.IsValid())
            return false;

        int[] daysInMonth = { 31, date.IsLeapYear() ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        return date.day < daysInMonth[date.month - 1];
    }

    // 3. Перевантаження сталих true і false: true, якщо дата є початком року
    public static bool operator true(Date date)
    {
        return date.IsValid() && date.day == 1 && date.month == 1;
    }

    public static bool operator false(Date date)
    {
        return !date.IsValid() || date.day != 1 || date.month != 1;
    }

    // 4. Перевантаження операції &: перевіряє рівність двох дат
    public static bool operator &(Date date1, Date date2)
    {
        return date1.day == date2.day && date1.month == date2.month && date1.year == date2.year;
    }

    // 5. Перетворення класу Date у тип string (і навпаки)
    public static implicit operator string(Date date)
    {
        if (!date.IsValid())
            return "Невалідна дата";
        return date.PrintShort();
    }

    public static implicit operator Date(string dateStr)
    {
        // Припускаємо формат "дд.мм.рррр"
        string[] parts = dateStr.Split('.');
        if (parts.Length != 3)
            return new Date(-1, -1, -1);

        int day, month, year;
        bool dayParsed = int.TryParse(parts[0], out day);
        bool monthParsed = int.TryParse(parts[1], out month);
        bool yearParsed = int.TryParse(parts[2], out year);

        if (!dayParsed || !monthParsed || !yearParsed)
            return new Date(-1, -1, -1);

        return new Date(day, month, year);
    }
}