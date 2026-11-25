using System;
using System.Text;

namespace lab07
{
    internal class Program
    {
        //триває одна пара
        const int HoursPerPair = 2;
       
        // Місткість масиву значень
        const int Capacity = 100;
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
                    case "1":case "2":case "3":case "4":case "5":case "6":case "7":case "8":case "9":
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
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
            Console.WriteLine("=== Розклад занять ===");
            Console.WriteLine("1 - показати всі записи");
            Console.WriteLine("2 - додати запис");
            Console.WriteLine("3 - знайти за умовою");
            Console.WriteLine("4 - змінити запис");
            Console.WriteLine("5 - видалити запис");
            Console.WriteLine("6 - зведена таблиця");
            Console.WriteLine("7 - зберегти у файл");
            Console.WriteLine("8 - завантажити з файлу");
            Console.WriteLine("9 - експорт у CSV");
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

    }
}