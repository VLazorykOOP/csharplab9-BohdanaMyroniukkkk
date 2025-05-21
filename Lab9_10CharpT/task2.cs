using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Task2
{
    // Множина гласних букв української абетки (у нижньому регістрі)
    private static readonly HashSet<char> vowels = new HashSet<char> { 'а', 'е', 'и', 'і', 'о', 'у', 'є', 'ї', 'ю', 'я' };

    public static void Execute()
    {
        try
        {
            // Шлях до текстового файлу
            string filePath = "C:\\Users\\myroniukkk\\OneDrive\\Робочий стіл\\c#\\lab9\\task2.txt";

            // Перевірка існування файлу
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл t.txt не знайдено!");
                return;
            }

            // Зчитування всього тексту файлу
            string text = File.ReadAllText(filePath);

            // Розбиття тексту на слова (враховуємо пробіли та розділові знаки)
            string[] words = text.Split(new[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            // Черги для слів
            Queue<string> vowelQueue = new Queue<string>();
            Queue<string> consonantQueue = new Queue<string>();

            // Розподіл слів по чергах
            foreach (string word in words)
            {
                if (string.IsNullOrEmpty(word)) continue;

                // Перша буква слова у нижньому регістрі
                char firstLetter = char.ToLower(word[0]);

                // Перевірка, чи починається слово на гласну
                if (vowels.Contains(firstLetter))
                {
                    vowelQueue.Enqueue(word);
                }
                else
                {
                    consonantQueue.Enqueue(word);
                }
            }

            // Виведення слів, що починаються на гласну
            Console.WriteLine("Слова, що починаються на голосну букву:");
            while (vowelQueue.Count > 0)
            {
                Console.WriteLine(vowelQueue.Dequeue());
            }

            // Виведення слів, що починаються на приголосну
            Console.WriteLine("\nСлова, що починаються на приголосну букву:");
            while (consonantQueue.Count > 0)
            {
                Console.WriteLine(consonantQueue.Dequeue());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Виникла помилка: {ex.Message}");
        }
    }
}