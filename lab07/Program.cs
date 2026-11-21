using System;
using System.Text;

namespace lab07
{
    internal class Program
    {
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
                Console.WriteLine(" Розклад занять ");
                Console.WriteLine("1 - довідка про програму");
                Console.WriteLine("2 - табулювання: навантаження залежно від тривалості пари");
                Console.WriteLine("3 - таблиця навантаження викладача (групи × пари)");
                Console.WriteLine("0 - вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
                        ShowLoadTabulation();
                        Pause();
                        break;
                    case "3":
                        ShowWorkloadTable();
                        Pause();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("До побачення.");
                        break;
                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        // Пауза перед поверненням у меню
        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
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
        static void ShowLoadTabulation()
        {
            double from = 1.0;   // початок проміжку, год
            double to = 3.0;     // кінець проміжку, год
            double step = 0.25;  // дробовий крок

            Console.WriteLine($"Навантаження викладача при {PairsPerWeek} парах на тиждень");
            Console.WriteLine();
            Console.WriteLine($"{"Пара, год",12}{"Год/тиждень",16}{"Год/семестр",16}");

            for (double x = from; x <= to + 1e-9; x += step)
            {
                double perWeek = x * PairsPerWeek;
                double perSemester = perWeek * WeeksInSemester;
                Console.WriteLine($"{x,12:F2}{perWeek,16:F2}{perSemester,16:F2}");
            }
        }
        //таблиця з двома входами
        static void ShowWorkloadTable()
        {
            int rows = 5;
            int cols = 6;

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

            for (int row = 1; row <= rows; row++)
            {
                int rowTotal = 0; // підсумок рядка обнуляється на кожному рядку
                Console.Write($"{row,8}");

                for (int col = 1; col <= cols; col++)
                {
                    int hours = row * col * HoursPerPair;
                    rowTotal += hours;
                    Console.Write($"{hours,8}");
                }

                Console.WriteLine($"{rowTotal,8}");
            }
        }
    }

}
