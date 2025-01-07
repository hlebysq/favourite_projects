namespace CustomClasses;
/// <summary>
/// Класс, предназначенный для работы с .csv файлами.
/// </summary>
public static class CsvProcessing
{
    public static string? FPath;

    /// <summary>
    /// Метод, проверяющий на абсолютность путь.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private static bool IsAbsolut()
    {
        try
        {
            if (Path.GetFullPath(FPath) != FPath)
            {
                throw new ArgumentNullException("Путь не абсолютный!");
            }
        }
        catch (ArgumentException e)
        {
            throw new ArgumentNullException("Это не путь...");
        }

        return true;
    }
    /// <summary>
    /// Метод, переводящий массив строк в табличку.
    /// </summary>
    /// <param name="allLines"></param>
    /// <returns></returns>
    public static string[][] ArrToArrArr(string[] allLines)
    {
        string[][] newArray = new string[allLines.Length][];
        newArray[0] = allLines[0].Split(";");
        for (int i = 1; i < allLines.Length; i++)
        {
            newArray[i] = allLines[i].Split(";\"");

        }

        for (int i = 0; i < newArray.GetLength(0); i++)
        {
            for (int j = 0; j < newArray[i].Length; j++)
            {
                // Меняем кринж ковычки на нормальные(первые некорректно читаются консолью), убираем кавычки по краям которые присущи .csv файлам.
                newArray[i][j] = newArray[i][j].Trim('"').Replace('»', '"').Replace('«', '"');
            }
        }

        return newArray;
    }
    /// <summary>
    /// Метод, создающий новый файл.
    /// </summary>
    /// <param name="table"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void CreateFile(Zags[] table)
    {
        try
        {
            Console.Clear();
            string path = CustomInput.ReadPath();
            Random rnd = new Random();
            while (File.Exists(path))
            {
                path = path.Insert(path.Length - 4, $"({rnd.Next(0, 10)})");
            }
            string[] arr = new string[table.Length + 1];
            arr[0] = table[0].GetZagolovokCSV();
            for (int i = 0; i < table.Length; i++)
            {
                arr[i + 1] = table[i].ToCSV()[0..^2];
            }
            File.WriteAllLines(path, arr);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Что-то пошло не так! Повторите попытку.");
        }
    }
    /// <summary>
    /// Метод, заменяющий данные в уже существующем файле.
    /// </summary>
    /// <param name="table"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void ReplaceFile(Zags[] table)
    {
        try
        {
            Console.Clear();
            string path = CustomInput.ReadPath();
            string[] arr = new string[table.Length + 1];
            arr[0] = table[0].GetZagolovokCSV();
            for (int i = 0; i < table.Length; i++)
            {
                arr[i + 1] = table[i].ToCSV()[..^2];
            }
            File.WriteAllLines(path, arr);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Что-то пошло не так! Повторите попытку.");
        }
    }
    /// <summary>
    /// Метод, добавляющий данные в уже существующий файл.
    /// </summary>
    /// <param name="table"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void AddToFile(Zags[] table)
    {
        try
        {
            Console.Clear();
            string path = CustomInput.ReadPath();
            if (!File.Exists(path))
            {
                throw new ArgumentException("Такого файла нет, куда записывать-то?");
            }
            File.AppendAllText(path, "\n");
            foreach (Zags el in table)
            {
                File.AppendAllText(path, el.ToCSV()[0..^2] + "\n");
            }
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("С путем все же что-то не так! Повторите попытку.");
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Что-то пошло не так! Повторите попытку.");
        }
    }
    /// <summary>
    ///  Метод, считывающий файл в массив строк. 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string[] Read()
    {
        //Обрабатываем исключения.
        try
        {
            Console.Write("Введите абсолютный путь до файла: ");
            FPath = Console.ReadLine();
            if (FPath is null)
            {
                throw new ArgumentNullException("Такого файла нет.");
            }

            IsAbsolut();
            string[] allLines = File.ReadAllLines(FPath);
            return allLines;
        }
        catch (IndexOutOfRangeException e)
        {
            throw new ArgumentNullException("Структура не соответствует варианту!");
        }
        catch (System.IO.FileNotFoundException e)
        {
            throw new ArgumentNullException("Файла нет в директории!");
        }
    }
}