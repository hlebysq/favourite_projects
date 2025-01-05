using System.Diagnostics.CodeAnalysis;

namespace CustomClasses;

public class DataProcessing
{
    /// <summary>
    /// Перегрузка метода с выборкой по 1 элементу.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnName"></param>
    public static void Choise(string[][] table, string columnName) 
    {
        
        int index = Array.IndexOf(table[0], $"{columnName}");
        string[] allEls = new string[table.GetLength(0) - 2];
        for (int i = 2; i < table.GetLength(0); i++)
        {
            allEls[i - 2] = table[i][index]; // Заполняем allEls со сдвигом на 2.
        }

        string[] allValues = allEls.Distinct().ToArray(); // "Множество" неповторяющихся элементов.
        int num = 1;
        Console.Clear();
        foreach (string el in allValues) // Выводим их пользователю.
        {
            Console.WriteLine($"{num++}. {el}");
        }
        
        Methods.StringPut(out string? fndEl, allValues);
        Methods.CoolOutput(table, index, fndEl);
        Methods.ReturnToMenu(table);
    }
    /// <summary>
    /// Перегрузка метода с выборкой по 1 элементу.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column1"></param>
    /// <param name="column2"></param>
    public static void Choise(string[][] table, string column1, string column2)
    {
        int index1 = Array.IndexOf(table[0], $"{column1}");
        int index2 = Array.IndexOf(table[0], $"{column2}");
        string[] allEls1 = new string[table.GetLength(0) - 2];
        string[] allEls2 = new string[table.GetLength(0) - 2];
        for (int i = 2; i < table.GetLength(0); i++)
        {
            allEls1[i - 2] = table[i][index1];
        }
        for (int i = 2; i < table.GetLength(0); i++)
        {
            allEls2[i - 2] = table[i][index2];
        }


        string[] allValues1 = allEls1.Distinct().ToArray(); // "Множество" неповторяющихся элементов.
        string[] allValues2 = allEls2.Distinct().ToArray(); // "Множество" неповторяющихся элементов.
        
        Console.Clear();
        int num = 1;
        foreach (string el in allValues1) // Выводим их пользователю.
        {
            Console.WriteLine($"{num++}. {el}");
        }
        string? fndEl1, fndEl2;
        Methods.StringPut(out fndEl1, allValues1);
        
        num = 1;
        foreach (string el in allValues2) // Выводим их пользователю.
        {
            Console.WriteLine($"{num++}. {el}");
        }
        Methods.StringPut(out fndEl2, allValues2);
        Methods.CoolOutput(table, index1, fndEl1, index2, fndEl2);
        Methods.ReturnToMenu(table);
    }
    /// <summary>
    /// Метод, сортирующий по возрастанию.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="name"></param>
    public static void SortUp(string[][] table, string name)
    {
        string[][] starTable = new string[table.GetLength(0)][]; // Сохраняем стартовую таблицу.
        Methods.CopyToTo(table, starTable);
        int index = Array.IndexOf(table[0], name);
        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 2; j < table.GetLength(0) - 1; j++)
            {
                if (Convert.ToInt32(table[j][index]) > Convert.ToInt32(table[j + 1][index]))
                {
                    Methods.SwapArr(ref table, j, j + 1); // Классический пузырек. Можно было квиксортом)
                }
            }
        }
        
        Methods.DefaultOutput(table);
        bool ans = Methods.OneTwoInput("изменения в файле");
        if (ans)
        {
            CsvProcessing.Write(Methods.ArrArrToArr(table));
        }
        Methods.ReturnToMenu(starTable);
    }
    /// <summary>
    /// Метод сортировки по алфавиту.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="name"></param>
    public static void SortAlph(string[][] table, string name)
    {
        string[][] starTable = new string[table.GetLength(0)][];
        Methods.CopyToTo(table, starTable);
        int index = Array.IndexOf(table[0], name);
        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 2; j < table.GetLength(0) - 1; j++)
            {
                if (table[j][index].CompareTo(table[j+1][index]) > 0)
                {
                    Methods.SwapArr(ref table, j, j + 1); // Пузырек, но сравнение CompareTo.
                }
            }
        }
        
        Methods.DefaultOutput(table);
        bool ans = Methods.OneTwoInput("изменения в файле");
        if (ans)
        {
            CsvProcessing.Write(Methods.ArrArrToArr(table));
        }
        Methods.ReturnToMenu(starTable);
    }
}