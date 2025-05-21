using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Лабораторна робота з C#");
        Console.WriteLine("Виберiть завдання (1-4):");

        string? choice = Console.ReadLine(); // Дозволяємо null
        if (choice == null)
        {
            Console.WriteLine("Вихiд через вiдсутнiсть введення.");
            return;
        }

        switch (choice)
        {
            case "1":
                Console.WriteLine("Виконується завдання 1:");
                Task1.Execute();
                break;
            case "2":
                Console.WriteLine("Виконується завдання 2:");
                Task2.Execute();
                break;
            case "3":
                Console.WriteLine("Виконується завдання 3:");
                Task3.Execute();
                break;
            case "4":
                Console.WriteLine("Виконується завдання 4:");
                Task4.Execute();
                break;
            default:
                Console.WriteLine("Невiрний вибiр. Виберiть число вiд 1 до 4.");
                break;
        }

        Console.WriteLine("\nНатиснiть будь-яку клавiшу для завершення...");
        Console.ReadKey();
    }
}