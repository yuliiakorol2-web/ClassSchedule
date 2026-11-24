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
       
        // Місткість масиву значень
        const int Capacity = 100;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool running = true;
            double[] values = new double[Capacity]; // навантаження викладачів, год
            int count = 0;                          // скільки значень зайнято

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":ShowAbout();Pause();break;
                    case "2":ShowLoadTabulation();Pause();break;
                    case "3":ShowWorkloadTable();Pause();break;
                    case "4":
                        {
                            double newValue = ReadDouble("Навантаження, год (0–100): ", 0, 100);
                            AddValue(values, ref count, newValue, Capacity);
                            Console.WriteLine($"Записів у масиві: {count} з {Capacity}");
                            Pause();
                            break;
                        }
                    case "5": PrintValues(values, count); Pause(); break;
                    case "6": ShowStatistics(values, count); Pause(); break;
                    case "7": ShowValueVsReferenceDemo(); Pause(); break;
                    case "8":
                        {
                            int number = ReadInt($"Номер значення для видалення (1–{Capacity}): ", 1, Capacity);
                            RemoveAt(values, ref count, number);
                            Pause();
                            break;
                        }
                    case "0":running = false;break;
                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
            Console.WriteLine("До побачення.");
        }
        // Виводить значення масиву до count таблицею з номерами
        static void PrintValues(double[] values, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Масив порожній.");
                return;
            }

            Console.WriteLine($"{"№",4}{"Навантаження, год",22}");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1,4}{values[i],22:F2}");
            }
            Console.WriteLine($"Усього значень: {count}");
        }
        // Виводить головне меню
        static void ShowMenu()
        {
            Console.WriteLine("===Розклад занять===");
            Console.WriteLine("1 - довідка про програму");
            Console.WriteLine("2 - табулювання: навантаження залежно від тривалості пари");
            Console.WriteLine("3 - таблиця навантаження викладача (групи × пари)");
            Console.WriteLine("4 - додати значення в масив");
            Console.WriteLine("5 - показати значення");
            Console.WriteLine("6 - показати статистику");
            Console.WriteLine("7 - дослід: значиме і посилальне");
            Console.WriteLine("8 - видалити значення за номером");
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

        // Додає значення в масив; лічильник іде через ref, щоб зміна була видна в Main
        static void AddValue(double[] values, ref int count, double newValue, int capacity)
        {
            if (count == capacity)
            {
                Console.WriteLine("Місця більше немає.");
                return;
            }
            values[count] = newValue;
            count++;
        }
        // Знаходить найбільше значення та його номер (з 1); для порожнього набору повертає 0 і 0
        static void FindMax(double[] values, int count, out double max, out int maxNumber)
        {
            if (count == 0)
            {
                max = 0;
                maxNumber = 0;
                return;
            }

            max = values[0];
            maxNumber = 1;
            for (int i = 1; i < count; i++)
            {
                if (values[i] > max)
                {
                    max = values[i];
                    maxNumber = i + 1;
                }
            }
        }

        // Рахує суму та середнє; для порожнього набору середнє 0, а не NaN
        static void Statistics(double[] values, int count, out double sum, out double average)
        {
            sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += values[i];
            }
            average = count > 0 ? sum / count : 0;
        }

        // Виводить статистику за масивом
        static void ShowStatistics(double[] values, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Масив порожній, статистики немає.");
                return;
            }

            Statistics(values, count, out double sum, out double average);
            FindMax(values, count, out double max, out int maxNumber);

            Console.WriteLine($"Кількість значень: {count}");
            Console.WriteLine($"Сума: {sum:F2} год");
            Console.WriteLine($"Середнє: {average:F2} год");
            Console.WriteLine($"Найбільше: {max:F2} год (номер {maxNumber})");
        }

        // Намагається змінити int-параметр без ref: змінюється лише копія
        static void TryChangeInt(int value)
        {
            value = value + 100;
            Console.WriteLine($"  Усередині методу value = {value}");
        }

        // Змінює елемент масиву: масив передається посиланням
        static void ChangeFirstElement(double[] items)
        {
            items[0] = items[0] + 100;
            Console.WriteLine($"  Усередині методу items[0] = {items[0]}");
        }

        // Показує різницю між значимим і посилальним типом
        static void ShowValueVsReferenceDemo()
        {
            int price = 50;
            double[] prices = { 50, 70, 90 };

            Console.WriteLine($"До виклику:    price = {price}, prices[0] = {prices[0]}");
            TryChangeInt(price);
            ChangeFirstElement(prices);
            Console.WriteLine($"Після виклику: price = {price}, prices[0] = {prices[0]}");
            Console.WriteLine();
            Console.WriteLine("price не змінився: метод отримав копію значення.");
            Console.WriteLine("prices[0] змінився: метод отримав адресу того самого масиву.");
        }

        // Видаляє значення за номером (з 1), зсуває решту вліво й зменшує лічильник
        static void RemoveAt(double[] values, ref int count, int index)
        {
            if (index < 1 || index > count)
            {
                Console.WriteLine("Немає значення з таким номером.");
                return;
            }

            for (int i = index - 1; i < count - 1; i++)
            {
                values[i] = values[i + 1];
            }
            count--;
            values[count] = 0;
            Console.WriteLine($"Значення №{index} видалено. Залишилось: {count}");
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
        static void PrintLoadTabulation(double from, double to, double step)
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

        // Запитує початок, кінець і крок, потім виводить табулювання
        static void ShowLoadTabulation()
        {
            double from = ReadDouble("Початок, год (0,5–5): ", 0.5, 5);
            double to = ReadDouble($"Кінець, год ({from}–5): ", from, 5);
            double step = ReadDouble("Крок, год (0,05–1): ", 0.05, 1);
            Console.WriteLine();
            PrintLoadTabulation(from, to, step);
        }
        //таблиця з двома входами
        static void ShowWorkloadTable()
        {
            int rows = ReadInt($"Кількість груп (1–{MaxSize}): ", 1, MaxSize);
            int cols = ReadInt($"Кількість пар на тиждень у групі (1–{MaxSize}): ", 1, MaxSize);
            Console.WriteLine();
            PrintWorkloadTable(rows, cols);
        }

        // Виводить таблицю з двома входами за переданими розмірами
        static void PrintWorkloadTable(int rows, int cols)
        {
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