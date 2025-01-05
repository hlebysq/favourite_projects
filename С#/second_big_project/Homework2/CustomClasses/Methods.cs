using System.Text;

namespace CustomClasses;
public class Methods
{
    
    /// <summary>
    /// Метод, создающий на основе таблички элементов массив строк, который не стыдно засунуть в .csv
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static string[] ArrArrToArr(string[][] table)
    {
        string[] newArr = new string[table.GetLength(0)];
        for (int i = 0; i < table.GetLength(0); i++)
        {
            StringBuilder str = new StringBuilder(); // Стрингбилдер чтобы не есть лишнюю память. Можно просто через string)
            for (int j = 0; j < table[i].Length; j++)
            {
                str.Append('"' + table[i][j] + '"' + ';');
            }

            newArr[i] = str.ToString();
        }
        return newArr;
    }
    /// <summary>
    /// Возврат к меню. Несем table как олимпийский огонь.
    /// </summary>
    /// <param name="table"></param>
    public static void ReturnToMenu(string[][] table)
    {
        Console.Write(Environment.NewLine+Environment.NewLine);
        Console.WriteLine("Чтобы вернуться к меню нажмите любую клавишу. (кроме esc)");
        char temp = Console.ReadKey().KeyChar;
        CsvProcessing.CallMenu(table);
    }
    /// <summary>
    /// Перегрузка метода StringPut, которая формирует искомый элемент на основе данных от пользователя.
    /// </summary>
    /// <param name="fndEl"></param>
    /// <param name="allValues"></param>
    public static void StringPut(out string? fndEl, string[] allValues)
    {
        while (true)
        {
            Console.WriteLine("Введите одно из предложенных выше значений (название, не цифру :) ):");
            fndEl = Console.ReadLine();
            if (fndEl is null)
            {
                Console.WriteLine("null какой-то, повторите попытку.");
                continue;
            }
            if (Array.IndexOf(allValues, fndEl) == -1)
            {
                Console.WriteLine("Этого элемента нет в списке, повторите попытку.");
                continue;
            }
            break;
        }
    }
    /// <summary>
    /// Перегрузка метода StringPut, формирующая название файла.
    /// </summary>
    /// <returns></returns>
    private static string StringPut()
    {
        while (true)
        {

            Console.Write("Введите название нового файла: ");
            string? ans = Console.ReadLine();
            if (ans is null) // Обрабатываем null.
            {
                Console.WriteLine("null какой-то! Повторите попытку.");
                continue;
            }
            // Проверка на запрещенные символы.
            if (ans.Any(new char[]{'*','\"','<','>','|', '[', ']', '?', '\'', '.', '/'}.Contains)) 
            { 
                Console.WriteLine("В строке запрещенные символы!! Повторите попытку.");
                continue;
            }

            return ans;
        }
    }
    /// <summary>
    /// Метод, копирующий зубчатый массив.
    /// </summary>
    /// <param name="arr1"></param>
    /// <param name="arr2"></param>
    public static void CopyToTo(string[][] arr1, string[][] arr2)
    {
        for (int i = 0; i <  arr1.GetLength(0); i++ )
        {
            arr2[i] = new string[arr1[i].Length];
            arr1[i].CopyTo(arr2[i], 0);
        }
    }
    /// <summary>
    /// Метод, красиво выводящий огромную таблицу по страничкам.
    /// </summary>
    /// <param name="table"></param>
    public static void DefaultOutput(string[][] table)
    {
        Console.Clear();
        string[][] starTable = new string[table.GetLength(0)][];
        CopyToTo(table, starTable);
        int n = 1;
        for (int j = 0; j < 36; j += 5) // Фиксируем j.
        {
            Console.Clear();
            
            for (int i = 2; i < table.GetLength(0); i++)
            {
                    for (int J = j; J < j + 5; J++)
                    {
                        if (table[i][J].Length > 22) // Обрезаем строку, если она не влезает.
                        {
                            starTable[i][J] = table[i][J][..19] + "...";
                        }
                    }
                    Console.WriteLine($"{starTable[i][j],22} | {starTable[i][j + 1],22} | {starTable[i][j + 2],22} | {starTable[i][j + 3],22} | {starTable[i][j + 4],22}");
            }
            Console.WriteLine($"Страница {n++}. Нажмите на любую клавишу (кроме esc) чтобы продолжить.");
            char tmp = Console.ReadKey().KeyChar;
        }
    }
    /// <summary>
    /// Перегрузка метода CoolOutput для двух значений. Вывод+выборка.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="index"></param>
    /// <param name="fndEl"></param>
    public static void CoolOutput(string[][] table, int index, string fndEl) 
    {
        Console.Clear();
        string[] allLines = ArrArrToArr(table);;
        string fileName = ""; // Чтобы компилятор не ругался.
        bool ans = OneTwoInput("файл с выборкой");
        if (ans) // Заполняем файлик если нужно.
        {
            fileName = StringPut();
            CsvProcessing.Write(allLines[0], fileName+".csv"); // Заголовок.
            CsvProcessing.Write(allLines[1], fileName+".csv"); // Заголовок.
            for (int i = 0; i < table.GetLength(0); i++)
            {
                if (table[i][index] == fndEl)
                {
                    CsvProcessing.Write(allLines[i], fileName+".csv"); // Перегрузкой врайта заполняем по строчке.
                }
            }
        }
        string[][] starTable = new string[table.GetLength(0)][];
        CopyToTo(table, starTable);
        int n = 1;
        
        for (int j = 0; j < 36; j += 5) // Фиксируем j.
        {
            Console.Clear();
            for (int i = 2; i < table.GetLength(0); i++)
            {
                if (table[i][index] == fndEl)
                {
                    
                    for (int J = j; J < j + 5; J++)
                    {
                        if (table[i][J].Length > 22) // Обрезаем строку, если она не влезает.
                        {
                            starTable[i][J] = table[i][J][..19] + "...";
                        }
                    }

                    Console.WriteLine(
                        $"{starTable[i][j],22} | {starTable[i][j + 1],22} | {starTable[i][j + 2],22} | {starTable[i][j + 3],22} | {starTable[i][j + 4],22}");
                }
            }
            Console.WriteLine($"Страница {n++}. Нажмите на любую клавишу (кроме esc) чтобы продолжить.");
            char tmp = Console.ReadKey().KeyChar;
        }
        
    }
    /// <summary>
    /// Метод, предназначенный для выбора из двух вариантов с клавиатуры.
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    public static bool OneTwoInput(string arg)
    {
        Console.WriteLine($"Хотите сохранить {arg}?");
        Console.WriteLine("1. Да");
        Console.WriteLine("2. Нет");
        while (true)
        {
            char ans = Console.ReadKey().KeyChar;
            switch (ans)
            {
                case '1':
                    return true;
                case '2':
                    return false;
                default:
                    continue;
            }
        }

    }
    /// <summary>
    /// Перегрузка метода CoolOutput для двух значений.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="index1"></param>
    /// <param name="fndEl1"></param>
    /// <param name="index2"></param>
    /// <param name="fndEl2"></param>
    public static void CoolOutput(string[][] table, int index1, string fndEl1, int index2, string fndEl2)
    {
        string[] allLines = ArrArrToArr(table);;
        string fileName = ""; // Чтобы компилятор не ругался.
        bool ans = OneTwoInput("файл с выборкой");
        if (ans)
        {
            fileName = StringPut();
            CsvProcessing.Write(allLines[0], fileName+".csv");
            CsvProcessing.Write(allLines[1], fileName+".csv");
            for (int i = 0; i < table.GetLength(0); i++)
            {
                if (table[i][index1] == fndEl1 && table[i][index2]==fndEl2)
                {
                    CsvProcessing.Write(allLines[i], fileName+".csv");
                }
            }
        }
        bool flag = false;
        Console.Clear();
        string[][] starTable = new string[table.GetLength(0)][];
        CopyToTo(table, starTable);
        int n = 1;
        
        for (int j = 0; j < 36; j += 5)
        {
            Console.Clear();
            
            for (int i = 2; i < table.GetLength(0); i++)
            {
                if (table[i][index1] == fndEl1 && table[i][index2] == fndEl2)
                {
                    flag = true;
                    for (int J = j; J < j + 5; J++)
                    {
                        if (table[i][J].Length > 22) // Обрезаем строку, если она не влезает.
                        {
                            starTable[i][J] = table[i][J][..19] + "...";
                        }
                    }

                    Console.WriteLine(
                        $"{starTable[i][j],22} | {starTable[i][j + 1],22} | {starTable[i][j + 2],22} | {starTable[i][j + 3],22} | {starTable[i][j + 4],22}");
                }
            }
            Console.WriteLine($"Страница {n++}. Нажмите на любую клавишу (кроме esc) чтобы продолжить.");
            char tmp = Console.ReadKey().KeyChar;
        }

        if (!flag)
        {
            Console.WriteLine("Строк, удовлетворяющих требованиям, нет.");
        }
    }

    /// <summary>
    /// Метод, меняющий две строки массива друг с другом.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="ind1"></param>
    /// <param name="ind2"></param>
    public static void SwapArr(ref string[][] table, int ind1, int ind2)
    {
        
        string[] tmp = new string[table.GetLength(0)]; // Временный массив. По идее убирается сборщиком мусора.
        table[ind1].CopyTo(tmp, 0);
        table[ind1] = table[ind2];
        table[ind2] = tmp;
    }
}