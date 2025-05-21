using System;
using System.IO;
using System.Collections;
using System.Linq;

class TextElement : IEnumerable, IComparable, ICloneable
{
    private string value; // Зберігає символ або слово
    public static readonly HashSet<char> vowels = new HashSet<char> { 'а', 'е', 'и', 'і', 'о', 'у', 'є', 'ї', 'ю', 'я' };

    public TextElement(string? val)
    {
        value = val ?? throw new ArgumentNullException(nameof(val));
    }

    public string Value => value;

    // Реалізація IComparable
    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        if (obj is not TextElement other)
            throw new ArgumentException("Об'єкт не є TextElement");

        bool thisIsVowel = value.Length > 0 && vowels.Contains(char.ToLower(value[0]));
        bool otherIsVowel = other.value.Length > 0 && vowels.Contains(char.ToLower(other.value[0]));

        if (thisIsVowel && !otherIsVowel) return -1;
        if (!thisIsVowel && otherIsVowel) return 1;
        return string.Compare(value, other.value, StringComparison.OrdinalIgnoreCase);
    }

    // Реалізація ICloneable
    public object Clone()
    {
        return new TextElement(value);
    }

    // Реалізація IEnumerable
    public IEnumerator GetEnumerator()
    {
        foreach (char c in value ?? "")
        {
            yield return c;
        }
    }

    public override string ToString() => value ?? "";
}

class Task3
{
    public static void Execute()
    {
        try
        {
            string? filePath = "C:\\Users\\myroniukkk\\OneDrive\\Робочий стіл\\c#\\lab9\\t.txt";
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("Файл t.txt не знайдено!");
                return;
            }

            // Завдання 1: Виведення рядків у зворотному порядку
            Console.WriteLine("Завдання 1: Рядки у зворотному порядку:");
            string[]? lines = File.ReadAllLines(filePath);
            foreach (string? line in lines ?? Array.Empty<string>())
            {
                if (string.IsNullOrEmpty(line)) continue;
                ArrayList charList = new ArrayList();
                TextElement textElement = new TextElement(line);

                foreach (char c in textElement)
                {
                    charList.Add(c);
                }

                Console.Write("Рядок у зворотному порядку: ");
                for (int i = charList.Count - 1; i >= 0; i--)
                {
                    Console.Write(charList[i]);
                }
                Console.WriteLine();
            }

            // Завдання 2: Слова за гласними/приголосними
            Console.WriteLine("\nЗавдання 2: Слова за голосними/приголосними:");
            string? text = File.ReadAllText(filePath);
            string[]? words = (text ?? "").Split(new[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            ArrayList vowelList = new ArrayList();
            ArrayList consonantList = new ArrayList();

            foreach (string? word in words ?? Array.Empty<string>())
            {
                if (string.IsNullOrEmpty(word)) continue;
                TextElement wordElement = new TextElement(word);
                char firstLetter = char.ToLower(word[0]);
                if (TextElement.vowels.Contains(firstLetter))
                {
                    vowelList.Add(wordElement);
                }
                else
                {
                    consonantList.Add(wordElement);
                }
            }

            ArrayList vowelListClone = (ArrayList)(vowelList.Clone() ?? new ArrayList());
            ArrayList consonantListClone = (ArrayList)(consonantList.Clone() ?? new ArrayList());

            Console.WriteLine("Слова, що починаються на голосну букву:");
            foreach (TextElement? word in vowelListClone)
            {
                if (word != null) Console.WriteLine(word);
            }

            Console.WriteLine("\nСлова, що починаються на приголосну букву:");
            foreach (TextElement? word in consonantListClone)
            {
                if (word != null) Console.WriteLine(word);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Виникла помилка: {ex.Message}");
        }
    }
}