using System;
using System.IO;
using System.Collections.Generic;

class Task1
{
    public static void Execute()
    {
        try
        {
            // Шлях до текстового файлу
            string filePath = "C:\\Users\\myroniukkk\\OneDrive\\Робочий стіл\\c#\\lab9\\t.txt";

            // Перевірка існування файлу
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл t.txt не знайдено!");
                return;
            }

            // Зчитування всіх рядків файлу
            string[] lines = File.ReadAllLines(filePath);

            // Обробка кожного рядка
            foreach (string line in lines)
            {
                // Створюємо стек для символів рядка
                Stack<char> charStack = new Stack<char>();

                // Заповнюємо стек символами рядка
                foreach (char c in line)
                {
                    charStack.Push(c);
                }

                // Виводимо символи у зворотному порядку
                Console.Write("Рядок у зворотному порядку: ");
                while (charStack.Count > 0)
                {
                    Console.Write(charStack.Pop());
                }
                Console.WriteLine(); // Новий рядок після виведення
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Виникла помилка: {ex.Message}");
        }
    }
}