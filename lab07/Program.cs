using System;
using System.Text;

namespace lab07
{
    internal class Program
    {
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
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                    case "3":
                        Console.WriteLine("Поки не зроблено.");
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
    }
}
