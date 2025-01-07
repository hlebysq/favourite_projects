namespace CustomClasses;

/// <summary>
/// Класс, в котором хранятся методы для работы с данными.
/// </summary>
public static class DataProcessing
{
    /// <summary>
    /// Перегрузка метода с фильтрацией по 1 элементу.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnName"></param>
    public static void Choise(Zags[] table, string columnName)
    {
        string[] allEls = new string[table.GetLength(0)];
        for (int i = 0; i < table.GetLength(0); i++)
        {
            allEls[i] = table[i][columnName]; // Заполняем allEls.
        }

        string[] allValues = allEls.Distinct().ToArray(); // "Множество" неповторяющихся элементов.
        int num = 1;
        Console.Clear();
        foreach (string el in allValues) // Выводим их пользователю.
        {
            Console.WriteLine($"{num++}. {el}");
        }

        string? fndEl;
        CustomInput.StringPut(out fndEl, allValues);
        Console.Clear();
        List<Zags> list = new List<Zags>();
        foreach (Zags zags in table)
        {
            if (zags[columnName] == fndEl)
            {
                list.Add(zags);
            }
        }
        Methods.CoolOutput(list.ToArray());
        Menu.TalkAboutFile(list.ToArray());
        Menu.ReturnToMenu(table);
    }
    /// <summary>
    /// Перегрузка метода с фильтрацией по 2 элементам.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="column1"></param>
    /// <param name="column2"></param>
    public static void Choise(Zags[] table, string column1, string column2)
    {
        string[] allEls1 = new string[table.GetLength(0)];
        string[] allEls2 = new string[table.GetLength(0)];
        for (int i = 0; i < table.GetLength(0); i++)
        {
            allEls1[i] = table[i][column1];
            allEls2[i] = table[i][column2];
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
        CustomInput.StringPut(out fndEl1, allValues1);
        
        num = 1;
        foreach (string el in allValues2) // Выводим их пользователю.
        {
            Console.WriteLine($"{num++}. {el}");
        }
        CustomInput.StringPut(out fndEl2, allValues2);
        Console.Clear();
        List<Zags> list = new List<Zags>();
        foreach (Zags zags in table)
        {
            if (zags[column1] == fndEl1 && zags[column2]==fndEl2)
            {
                list.Add(zags);
            }
        }
        Methods.CoolOutput(list.ToArray());
        Menu.TalkAboutFile(list.ToArray());
        Menu.ReturnToMenu(table);
    }
    /// <summary>
    /// Метод, меняющий два ЗАГСа друг с другом.
    /// </summary>
    /// <param name="zg1"></param>
    /// <param name="zg2"></param>
    public static void SwapZags(ref Zags zg1, ref Zags zg2)
    {
        (zg1, zg2) = (zg2, zg1);
    }
    /// <summary>
    /// Метод, сортирующий массив ЗАГСов.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="name"></param>
    /// <param name="ans"></param>
    public static void SortAlph(Zags[] table, string name, bool ans)
    {
        List<Zags> startTable = table.ToList<Zags>(); // Исходная таблица.
        for (int i = 0; i < table.GetLength(0); i++)
        {
            for (int j = 0; j < table.GetLength(0)-1; j++)
            {
                if (table[j].CompareTo(table[j + 1]) > 0) // Пузырек, но сравнение CompareTo.
                {
                    SwapZags(ref table[j], ref table[j+1]); 
                }
            }
        }
        if (!ans)
        {
            table.Reverse();
        }
        Methods.CoolOutput(table);
        Menu.TalkAboutFile(table);
        Menu.ReturnToMenu(startTable.ToArray());
    }
}