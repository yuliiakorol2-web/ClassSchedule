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

            int[] ids = new int[Capacity];
            string[] subjects = new string[Capacity];
            string[] teachers = new string[Capacity];
            string[] groups = new string[Capacity];
            string[] days = new string[Capacity];
            int[] pairNumbers = new int[Capacity];
            string[] rooms = new string[Capacity];
            int[] typeIds = new int[Capacity];   //код типу заняття
            int count = 0;                       //скільки записів зайнято
            int nextId = 1;                      //наступний номер запису

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": PrintRecords(ids, subjects, teachers, groups, days, pairNumbers, rooms, typeIds, count); Pause(); break;
                    case "2":AddRecord(ids, subjects, teachers, groups, days, pairNumbers, rooms, typeIds, ref count, ref nextId, Capacity); Pause(); break;
                    case "3":case "4":case "5":case "6":case "7":case "8":case "9":
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
        // Читає рядок не коротший за minLength символів (після Trim)
        static string ReadText(string prompt, int minLength)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (input != null)
                {
                    input = input.Trim();
                    if (input.Length >= minLength)
                    {
                        return input;
                    }
                }
                if (minLength <= 1)
                {
                    Console.WriteLine("Поле не може бути порожнім.");
                }
                else
                {
                    Console.WriteLine($"Потрібно щонайменше {minLength} символи.");
                }
            }
        }

        // Читає день тижня зі списку допустимих значень
        static string ReadDay(string prompt)
        {
            string[] allowed = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб" };
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                input = input == null ? "" : input.Trim();
                for (int i = 0; i < allowed.Length; i++)
                {
                    if (string.Equals(input, allowed[i], StringComparison.OrdinalIgnoreCase))
                    {
                        return allowed[i];
                    }
                }
                Console.WriteLine("Потрібен день тижня: Пн, Вт, Ср, Чт, Пт, Сб.");
            }
        }

        // Додає пару: збирає всі поля в локальні змінні, потім записує в масиви під індексом count
        static void AddRecord(int[] ids, string[] subjects, string[] teachers, string[] groups,
            string[] days, int[] pairNumbers, string[] rooms, int[] typeIds,
            ref int count, ref int nextId, int capacity)
        {
            if (count == capacity)
            {
                Console.WriteLine("Сховище заповнене, видаліть щось зайве.");
                return;
            }

            string subject = ReadText("Дисципліна: ", 2);
            string teacher = ReadText("Викладач: ", 2);
            string group = ReadText("Група: ", 2);
            string day = ReadDay("День тижня (Пн–Сб): ");
            int pairNumber = ReadInt("Номер пари (1–7): ", 1, 7);
            string room = ReadText("Аудиторія: ", 1);
            int typeId = ReadInt("Код типу заняття (1–3): ", 1, 3);

            ids[count] = nextId;
            subjects[count] = subject;
            teachers[count] = teacher;
            groups[count] = group;
            days[count] = day;
            pairNumbers[count] = pairNumber;
            rooms[count] = room;
            typeIds[count] = typeId;
            count++;
            nextId++;

            Console.WriteLine($"Додано пару №{ids[count - 1]}.");
        }
        // Виводить усі записи таблицею
        static void PrintRecords(int[] ids, string[] subjects, string[] teachers, string[] groups,
            string[] days, int[] pairNumbers, string[] rooms, int[] typeIds, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Записів поки немає.");
                return;
            }

            Console.WriteLine($"{"№",4} {"Дисципліна",-22}{"Викладач",-18}{"Група",-9}{"День",-6}{"Пара",4} {"Ауд.",-8}{"Тип",4}");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{ids[i],4} {subjects[i],-22}{teachers[i],-18}{groups[i],-9}{days[i],-6}{pairNumbers[i],4} {rooms[i],-8}{typeIds[i],4}");
            }
            Console.WriteLine($"\nЗаписів: {count}.");
        }
    }
}