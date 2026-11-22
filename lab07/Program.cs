using System;
using System.Text;

namespace lab07
{
    internal class Program
    {
        // Найбільший розмір таблиці з двома входами
        const int MaxSize = 10;
        //триває одна пара
        const int HoursPerPair = 2;
        //пар на тиждень для таблиці табулювання
        const int PairsPerWeek = 10;
        //тижнів у семестрі
        const int WeeksInSemester = 16;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":ShowAbout();Pause();break;
                    case "2":ShowLoadTabulation(1.0, 3.0, 0.25);Pause();break;
                    case "3":ShowWorkloadTable();Pause();break;
                    case "0":running = false;break;
                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
            Console.WriteLine("До побачення.");
        }
        // Виводить головне меню
        static void ShowMenu()
        {
            Console.WriteLine("===Розклад занять===");
            Console.WriteLine("1 - довідка про програму");
            Console.WriteLine("2 - табулювання: навантаження залежно від тривалості пари");
            Console.WriteLine("3 - таблиця навантаження викладача (групи × пари)");
            Console.WriteLine("0 - вихід");
            Console.Write("Ваш вибір: ");
        }

        // Пауза перед поверненням у меню
        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // Читає дробове число з перевіркою меж. питає знову, доки не введено прийнятне
        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();

                if (!double.TryParse(text, out double value))
                {
                    Console.WriteLine("Помилка: потрібно ввести число.");
                    continue;
                }
                if (!(value >= min && value <= max))
                {
                    Console.WriteLine($"Помилка: число має бути від {min} до {max}.");
                    continue;
                }
                return value;
            }
        }
        
        // Читає ціле число з перевіркою меж. питає знову, доки не введено прийнятне
        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = Console.ReadLine();

                if (!int.TryParse(text, out int value))
                {
                    Console.WriteLine("Помилка: потрібно ввести ціле число.");
                    continue;
                }
                if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: число має бути від {min} до {max}.");
                    continue;
                }
                return value;
            }
        }

        // Навантаження за тиждень, год: тривалість однієї пари × кількість пар
        static double CalculateWeeklyLoad(double pairHours, int pairsPerWeek)
        {
            return pairHours * pairsPerWeek;
        }

        // Навантаження за семестр, год
        static double CalculateSemesterLoad(double weeklyLoad, int weeks)
        {
            return weeklyLoad * weeks;
        }

        // Навантаження викладача, год: груп × пар на тиждень у групі × годин на пару
        static int CalculateGroupsLoad(int groups, int pairsPerGroup)
        {
            return groups * pairsPerGroup * HoursPerPair;
        }
        //тема,сутність,поля з типами
        static void ShowAbout()
        {
            Console.WriteLine("Тема: Розклад занять.");
            Console.WriteLine("Головна сутність: Пара.");
            Console.WriteLine("Довідник: Тип заняття.");
            Console.WriteLine("Полів у сутності: 8.");
            Console.WriteLine();
            Console.WriteLine("Поля сутності «Пара»:");
            Console.WriteLine("int    — код");
            Console.WriteLine("string — дисципліна");
            Console.WriteLine("string — викладач");
            Console.WriteLine("string — група");
            Console.WriteLine("string — день тижня");
            Console.WriteLine("int    — номер пари");
            Console.WriteLine("string — аудиторія");
            Console.WriteLine("int    — код типу заняття (з довідника)");
            Console.WriteLine();
            Console.WriteLine("Підсумкова величина: навантаження кожного викладача за тиждень у годинах.");
        }
        
        // Табулювання функції: навантаження залежно від тривалості однієї пари
        static void ShowLoadTabulation(double from, double to, double step)
        {
            Console.WriteLine($"Навантаження викладача при {PairsPerWeek} парах на тиждень");
            Console.WriteLine();
            Console.WriteLine($"{"Пара, год",12}{"Год/тиждень",16}{"Год/семестр",16}");

            for (double x = from; x <= to + 1e-9; x += step)
            {
                double perWeek = CalculateWeeklyLoad(x, PairsPerWeek);
                double perSemester = CalculateSemesterLoad(perWeek, WeeksInSemester);
                Console.WriteLine($"{x,12:F2}{perWeek,16:F2}{perSemester,16:F2}");
            }
        }
        
        // Читає розмір таблиці; при помилці повідомляє про неї й повертає -1
        static int ReadSize(string prompt)
        {
            Console.Write(prompt);
            string text = Console.ReadLine();

            if (!int.TryParse(text, out int value))
            {
                Console.WriteLine("Помилка: потрібно ввести ціле число.");
                return -1;
            }
            if (value <= 0)
            {
                Console.WriteLine("Помилка: число має бути більшим за нуль.");
                return -1;
            }
            if (value > MaxSize)
            {
                Console.WriteLine($"Помилка: число не може бути більшим за {MaxSize}.");
                return -1;
            }
            return value;
        }

        //таблиця з двома входами
        static void ShowWorkloadTable()
        {
            int rows = ReadSize($"Кількість груп (1–{MaxSize}): ");
            if (rows == -1) return;

            int cols = ReadSize($"Кількість пар на тиждень у групі (1–{MaxSize}): ");
            if (cols == -1) return;
            Console.WriteLine();
            Console.WriteLine("Навантаження викладача за тиждень, год");
            Console.WriteLine("(рядки — груп, стовпці — пар на тиждень у кожній групі)");
            Console.WriteLine();

            //порожнє місце під стовпець з номерами рядків
            Console.Write($"{"Груп",8}");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{col,8}");
            }
            Console.WriteLine($"{"Разом",8}");
           
            int[] colTotals = new int[cols]; // підсумки стовпців
            int grandTotal = 0;              // загальний підсумок

            for (int row = 1; row <= rows; row++)
            {
                int rowTotal = 0; // підсумок рядка обнуляється на кожному рядку
                Console.Write($"{row,8}");

                for (int col = 1; col <= cols; col++)
                {
                    int hours = CalculateGroupsLoad(row, col);
                    rowTotal += hours;
                    colTotals[col - 1] += hours;
                    Console.Write($"{hours,8}");
                }

                Console.WriteLine($"{rowTotal,8}");
                grandTotal += rowTotal;
            }
            // Роздільник і рядок підсумків
            Console.WriteLine(new string('-', 8 * (cols + 2)));
            Console.Write($"{"Разом",8}");
            for (int col = 0; col < cols; col++)
            {
                Console.Write($"{colTotals[col],8}");
            }
            Console.WriteLine($"{grandTotal,8}");
        }
    }
}
