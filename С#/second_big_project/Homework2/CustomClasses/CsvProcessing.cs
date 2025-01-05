using System.Net;

namespace CustomClasses;

public class CsvProcessing
{
    private static string? FPath;
    
    /// <summary>
    /// Метод, проверяющий на абсолютность путь.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    private static void IsAbsolut()
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
        return;
    }
    /// <summary>
    /// Метод, переводящий массив строк в табличку.
    /// </summary>
    /// <param name="allLines"></param>
    /// <returns></returns>
    public static string[][] ArrToArrArr(string[] allLines)
    {
        string[][] newArray = new string[allLines.Length][]; 
        for (int i = 0; i < allLines.Length; i++)
        {
            newArray[i] = allLines[i].Split(';');
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
    /// Метод, проверяющий формат и сравнивающий с допустимым форматом.
    /// </summary>
    /// <param name="allLines"></param>
    /// <returns></returns>
    public static bool CheckFormat(string[] allLines)
    {
        try
        { 
            string[][] allEls = ArrToArrArr(allLines);
            for (int i = 2; i < allEls.GetLength(0); i++)
            { 
                bool flag = int.TryParse(allEls[i][Array.IndexOf(allEls[0], "Seats")], out int tmp);
                if (!flag)
                { 
                    return false;
                }
            }
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    /// <summary>
    /// Перегрузка метода Write с массивом строк.
    /// </summary>
    /// <param name="allLines"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Write(string[] allLines)
    {
        try
        {
            File.WriteAllLines(FPath, allLines);
        }
        catch (ArgumentException e)
        {
            throw new ArgumentNullException("Указан некорректный путь.");
        } // В случае некорректно указанного пути выбрасываем ошибку, которую выловим в Main.
    }
    /// <summary>
    /// Метод, считывающий файл.
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
            if (!CheckFormat(allLines))
            {
                throw new ArgumentNullException("Структура не соответствует варианту!");
            }

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
    /// <summary>
    /// Перегрузка метода Write.
    /// </summary>
    /// <param name="newLine"></param>
    /// <param name="nPath"></param>
    public static void Write(string newLine, string nPath)
    {
        File.AppendAllText(nPath, newLine+'\n');
    }
    /// <summary>
    /// Метод, вызывающий меню.
    /// </summary>
    /// <param name="table"></param>
    public static void CallMenu(string[][] table)
    {
        Console.Clear();
        Console.WriteLine("Укажите номер пункта меню для запуска действия:");
        Console.WriteLine("1. Произвести выборку по значению ObjectName");
        Console.WriteLine("2. Произвести выборку по значению NameWinter");
        Console.WriteLine("3. Произвести выборку по значениям ObjectName и NameWinter ");
        Console.WriteLine("4. Отсортировать таблицу по значению Lighting (по алфавиту в прямом порядке)");
        Console.WriteLine("5. Отсортировать таблицу по значению Seats (в порядке возрастания)");
        Console.WriteLine("6. Завершить работу программы.");
        
        while (true)
        {
            // Получаем от пользователя символ, пока не получим цифру [1,6].
            char num = Console.ReadKey().KeyChar;
            bool flag = false;
            switch (num) // Свитчкейсом полученную цифру обработатываем, вызвав соответствущий метод/завершив работу.
            {
                case '1':
                    DataProcessing.Choise(table, "ObjectName");
                    flag = true;
                    break;
                case '2':
                    DataProcessing.Choise(table, "NameWinter");
                    flag = true;
                    break;
                case '3':
                    DataProcessing.Choise(table, "ObjectName", "NameWinter"); 
                    flag = true;
                    break;
                case '4':
                    DataProcessing.SortAlph(table, "Lighting");
                    flag = true;
                    break;
                case '5':
                    DataProcessing.SortUp(table, "Seats");
                    flag = true;
                    break;
                case '6':
                    return;
                
            }
            if (flag)
            {
                break;
            }
        }
    }
}