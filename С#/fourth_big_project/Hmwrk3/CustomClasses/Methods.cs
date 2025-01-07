namespace CustomClasses;
/// <summary>
/// Класс, в котором хранятся полезные методы.
/// </summary>
public static class Methods
{
    /// <summary>
    /// Метод, читаемо выводящий таблицу на экран.
    /// </summary>
    /// <param name="table"></param>
    public static void CoolOutput(Zags[] table)
    {
        string[][] tableStrings = new string[table.Length][];
        //int index = table[0].GetIndex(fndEl);
        for (int i = 0; i < tableStrings.Length; i++)
        {
            tableStrings[i] = table[i].ToStringArray();
        }
        CutTable(ref tableStrings);
        Console.Clear();
        // Так как у нас 22 элемента, у нас два варианта вывода по страницам - по 2 и по 11. По 2 слишком мало, поэтому я вывожу 11 столбцов.
        for (int i = 0; i < tableStrings.GetLength(0); i++)
        {
            Console.WriteLine(
                $"{tableStrings[i][0],3} | {tableStrings[i][1],15} | {tableStrings[i][2],15} | {tableStrings[i][3],15} | {tableStrings[i][4],3} | " +
                $"{tableStrings[i][5],15} | {tableStrings[i][6],15} | {tableStrings[i][7],15} | {tableStrings[i][8],15} | {tableStrings[i][9],15} | " +
                $"{tableStrings[i][10],15}");
        }
        Console.WriteLine($"Страница 1. Нажмите на любую клавишу чтобы продолжить.");
        char tmp = Console.ReadKey().KeyChar;
        Console.Clear();
        for (int i = 0; i < tableStrings.GetLength(0); i++)
        {
            Console.WriteLine(
                $"{tableStrings[i][11],15} | {tableStrings[i][12],15} | {tableStrings[i][13],15} | {tableStrings[i][14],15} | {tableStrings[i][15],4} | " +
                $"{tableStrings[i][16],15} | {tableStrings[i][17],15} | {tableStrings[i][18],15} | {tableStrings[i][19],15} | {tableStrings[i][20],15} | " +
                $"{tableStrings[i][21],15}");
        }
        Console.WriteLine($"Страница 2. Нажмите на любую клавишу чтобы продолжить.");
        tmp = Console.ReadKey().KeyChar;
        Console.Clear();
    }
    /// <summary>
    /// Метод, обрезающий элементы таблицы до длины 15.
    /// </summary>
    /// <param name="tableStrings"></param>
    public static void CutTable(ref string[][] tableStrings)
    {
        for(int i = 0; i < tableStrings.Length; i++)
        {
            for(int j = 0; j < tableStrings[i].Length ; j++)
            {
                if (tableStrings[i][j].Length > 15) // Обрезаем строку, если она не влезает.
                {
                    tableStrings[i][j] = tableStrings[i][j][..12] + "...";
                }
            }
        }
    }
    /// <summary>
    /// Метод, проверяющий на абсолютность путь.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsAbsolut(string path)
    {
        if (Path.GetFullPath(path) != path)
        {
            return false;
        }
        return true;
    }
}