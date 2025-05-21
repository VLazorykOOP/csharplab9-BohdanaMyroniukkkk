using System;
using System.Collections;

class Song : ICloneable
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public int Duration { get; set; }

    public Song(string title, string artist, int duration)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Artist = artist ?? throw new ArgumentNullException(nameof(artist));
        Duration = duration > 0 ? duration : throw new ArgumentException("Тривалiсть має бути бiльше 0");
    }

    public object Clone()
    {
        return new Song(Title, Artist, Duration);
    }

    public override string ToString()
    {
        return $"Пiсня: {Title}, Виконавець: {Artist}, Тривалiсть: {Duration / 60}:{Duration % 60:D2}";
    }
}

class MusicDisc : IEnumerable
{
    public string DiscName { get; set; }
    private readonly Hashtable songs;

    public MusicDisc(string discName)
    {
        DiscName = discName ?? throw new ArgumentNullException(nameof(discName));
        songs = new Hashtable();
    }

    public void AddSong(Song song)
    {
        if (song == null) throw new ArgumentNullException(nameof(song));
        if (song.Title == null) throw new ArgumentNullException(nameof(song.Title));
        if (!songs.ContainsKey(song.Title))
        {
            songs.Add(song.Title, song);
        }
        else
        {
            Console.WriteLine($"Пiсня '{song.Title}' уже iснує на диску '{DiscName}'");
        }
    }

    public void RemoveSong(string title)
    {
        if (title == null) throw new ArgumentNullException(nameof(title));
        if (songs.ContainsKey(title))
        {
            songs.Remove(title);
        }
        else
        {
            Console.WriteLine($"Пiсня '{title}' не знайдена на диску '{DiscName}'");
        }
    }

    public IEnumerator GetEnumerator()
    {
        return songs.Values.GetEnumerator();
    }

    public Hashtable GetSongs()
    {
        return songs;
    }
}

class MusicCatalog
{
    private readonly Hashtable discs;

    public MusicCatalog()
    {
        discs = new Hashtable();
    }

    public void AddDisc(MusicDisc disc)
    {
        if (disc == null) throw new ArgumentNullException(nameof(disc));
        if (disc.DiscName == null) throw new ArgumentNullException(nameof(disc.DiscName));
        if (!discs.ContainsKey(disc.DiscName))
        {
            discs.Add(disc.DiscName, disc);
        }
        else
        {
            Console.WriteLine($"Диск '{disc.DiscName}' уже iснує в каталозi");
        }
    }

    public void RemoveDisc(string discName)
    {
        if (discName == null) throw new ArgumentNullException(nameof(discName));
        if (discs.ContainsKey(discName))
        {
            discs.Remove(discName);
        }
        else
        {
            Console.WriteLine($"Диск '{discName}' не знайдений у каталозi");
        }
    }

    public void DisplayCatalog()
    {
        Console.WriteLine("\nВмiст каталогу:");
        if (discs.Count == 0)
        {
            Console.WriteLine("Каталог порожнiй");
            return;
        }

        foreach (DictionaryEntry entry in discs)
        {
            if (entry.Value is MusicDisc disc)
            {
                Console.WriteLine($"\nДиск: {disc.DiscName}");
                foreach (Song song in disc)
                {
                    Console.WriteLine($"  {song}");
                }
            }
        }
    }

    public void DisplayDisc(string discName)
    {
        if (discName == null) throw new ArgumentNullException(nameof(discName));
        if (discs[discName] is MusicDisc disc)
        {
            Console.WriteLine($"\nВмiст диску '{discName}':");
            foreach (Song song in disc)
            {
                Console.WriteLine($"  {song}");
            }
        }
        else
        {
            Console.WriteLine($"Диск '{discName}' не знайдений");
        }
    }

    public void SearchByArtist(string artist)
    {
        if (artist == null) throw new ArgumentNullException(nameof(artist));
        Console.WriteLine($"\nПошук пiсень виконавця '{artist}':");
        bool found = false;
        foreach (DictionaryEntry entry in discs)
        {
            if (entry.Value is MusicDisc disc)
            {
                foreach (Song song in disc)
                {
                    if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Диск: {disc.DiscName}, {song}");
                        found = true;
                    }
                }
            }
        }
        if (!found)
        {
            Console.WriteLine("Пiсень цього виконавця не знайдено");
        }
    }

    public bool Contains(string discName)
    {
        return discs.ContainsKey(discName);
    }

    public MusicDisc? this[string discName]
    {
        get => discs[discName] as MusicDisc;
        set => discs[discName] = value ?? throw new ArgumentNullException(nameof(value));
    }
}

class Task4
{
    public static void Execute()
    {
        MusicCatalog catalog = new MusicCatalog();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nКаталог музичних дискiв");
            Console.WriteLine("1. Додати диск");
            Console.WriteLine("2. Видалити диск");
            Console.WriteLine("3. Додати пiсню до диску");
            Console.WriteLine("4. Видалити пiсню з диску");
            Console.WriteLine("5. Переглянути весь каталог");
            Console.WriteLine("6. Переглянути вмiст диску");
            Console.WriteLine("7. Пошук за виконавцем");
            Console.WriteLine("8. Вихiд");
            Console.Write("Виберiть опцiю (1-8): ");

            string? choice = Console.ReadLine();
            if (string.IsNullOrEmpty(choice)) continue;

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Введiть назву диску: ");
                        string discName = Console.ReadLine()!;
                        catalog.AddDisc(new MusicDisc(discName));
                        break;

                    case "2":
                        Console.Write("Введiть назву диску для видалення: ");
                        string discNameToRemove = Console.ReadLine()!;
                        catalog.RemoveDisc(discNameToRemove);
                        break;

                    case "3":
                        Console.Write("Введiть назву диску: ");
                        string discNameAdd = Console.ReadLine()!;
                        if (!catalog.Contains(discNameAdd))
                        {
                            Console.WriteLine($"Диск '{discNameAdd}' не знайдений");
                            break;
                        }
                        MusicDisc? discAdd = catalog[discNameAdd];
                        if (discAdd == null)
                        {
                            Console.WriteLine($"Диск '{discNameAdd}' не знайдений");
                            break;
                        }
                        Console.Write("Введiть назву пiснi: ");
                        string songTitle = Console.ReadLine()!;
                        Console.Write("Введiть виконавця: ");
                        string artist = Console.ReadLine()!;
                        Console.Write("Введiть тривалiсть (у секундах): ");
                        string? durationInput = Console.ReadLine();
                        if (!int.TryParse(durationInput, out int duration))
                        {
                            Console.WriteLine("Невiрна тривалiсть!");
                            break;
                        }
                        discAdd.AddSong(new Song(songTitle, artist, duration));
                        break;

                    case "4":
                        Console.Write("Введiть назву диску: ");
                        string discNameRemove = Console.ReadLine()!;
                        if (!catalog.Contains(discNameRemove))
                        {
                            Console.WriteLine($"Диск '{discNameRemove}' не знайдений");
                            break;
                        }
                        MusicDisc? discRemove = catalog[discNameRemove];
                        if (discRemove == null)
                        {
                            Console.WriteLine($"Диск '{discNameRemove}' не знайдений");
                            break;
                        }
                        Console.Write("Введiть назву пiснi для видалення: ");
                        string songTitleRemove = Console.ReadLine()!;
                        discRemove.RemoveSong(songTitleRemove);
                        break;

                    case "5":
                        catalog.DisplayCatalog();
                        break;

                    case "6":
                        Console.Write("Введiть назву диску: ");
                        string discNameDisplay = Console.ReadLine()!;
                        catalog.DisplayDisc(discNameDisplay);
                        break;

                    case "7":
                        Console.Write("Введiть iм'я виконавця: ");
                        string artistSearch = Console.ReadLine()!;
                        catalog.SearchByArtist(artistSearch);
                        break;

                    case "8":
                        running = false;
                        Console.WriteLine("Вихiд...");
                        break;

                    default:
                        Console.WriteLine("Невiрний вибiр. Виберiть число вiд 1 до 8.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Виникла помилка: {ex.Message}");
            }
        }
    }
}