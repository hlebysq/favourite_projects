public static class Methods
{
    
    /// <summary>
    /// Вывод всех элементов double[,] массива произвольного размера.
    /// </summary>
    /// <param name="arr"></param>
    public static void WriteAllEl(double[,] arr)
    {
        if (arr is null) { Console.WriteLine(); return; } // Проверка на null, тк иногда возвращаем null в FullArr.
        // GetLength() в многомерных массивах возвращает все размерности в зависимости от порядкогого номера меры.
        for(int i = 0; i< arr.GetLength(0); i++)
        {
            for(int j=0; j< arr.GetLength(1); j++)
            {
                Console.Write($"{arr[i,j]:f3} "); 
            }
            Console.Write('\n'); 
            // Вывод вснх элементов массива по индексам.
        }
    }
    /// <summary>
    /// Заполняем массив соответственно заданию 2.
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="finalArr"></param>
    public static void FullArr(double[,] arr, out double[,] finalArr)
    {
        // Проверяем, есть ли элементы на четных позициях, если нет - возвращаем null.
        if (arr.GetLength(1) / 2 == 0)
        {
            finalArr = null;
            return;
        }
        int N = arr.GetLength(0), M = arr.GetLength(1); // Получаем размерность полученного массива.
        double[,] tmpArr = new double[N,M/2]; // Создаем новый массив, в котором при четном колве элементов в 2 раза
                                              // короче строки, при нечетном - в 2 раза и на 1 меньше короче. 
        for(int i = 0; i < N; i++)
        {
            for(int j = 1; j < M; j+=2) // Берем только нечетные индексы.
            {
                tmpArr[i, j / 2] = arr[i, j]; // Заполняем массив элементами с нечет индексами.
            }
        }
        finalArr = tmpArr;
        
    }

    /// <summary>
    /// Переносим элементы из массива со строками из файла в двумерный массив.
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="finalArr"></param>
    /// <returns></returns>
    public static bool ReadArr(string[] arr, out double[,] finalArr)
    {
        if(arr.Length==0) { Console.ForegroundColor = ConsoleColor.Red;  Console.WriteLine("В файле ничего нет!"); 
            Console.ForegroundColor = ConsoleColor.Gray; finalArr = null; return false; } // Если массив пустой возвращаем false.
        string[][] tmpMass = new string[arr.Length][]; // 
        for (int i = 0; i < arr.Length; i++) { tmpMass[i] = arr[i].Split(';'); }
        int len = tmpMass[0].Length;
        // Проверяем, реально ли в каждой строке одинаковое колво элементов.
        foreach (string[] str in tmpMass)
        {
            if (str.Length != len)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Нарушен формат ввода! Повторите попытку.");
                Console.ForegroundColor = ConsoleColor.Gray;
                finalArr = null;
                return false;
            }
            len = str.Length;
        }
        int N = arr.Length, M = tmpMass[0].Length; // Раз строк одинаковое колво можно взять за M любую, N колво строк.
        double[,] ansArr = new double[N, M];
        // Заполняем наш созданный двумерный массив.
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                double el;
                bool flag = double.TryParse(tmpMass[i][j], out el); // Трайпарсим элемент. Можно было просто через Parse :)

                // Если не получилось - формат ввода неверный. Сообщим пользователю и прервем работу. Также красим в красный.
                if (!flag)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Нарушен формат ввода! Повторите попытку.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    finalArr = null;
                    return false;
                }
                ansArr[i, j] = el; 
            }
        }
        finalArr = ansArr; 
        return true;

    }
    /// <summary>
    /// Читаем файл и возвращаем массив с его строками. Тексты ошибок говорят сами за себя.
    /// </summary>
    /// <param name="isRelease"></param>
    /// <returns></returns>
    public static string[] ReadFile(bool isRelease)
    {
        while (true)
        {
            Console.Write("Введите название файла(с расширением): ");
            try
            {
                var path = Console.ReadLine();
                string[] Arr;
                if (isRelease) { Arr = File.ReadAllLines($@"..\..\..\..\Hmwrk1CS-Writer\bin\release\net6.0\{path}"); } // Файл из директории Writer'а.
                else { Arr = File.ReadAllLines($@"..\..\..\..\Hmwrk1CS-Writer\bin\debug\net6.0\{path}"); }
                return Arr;
            }
            // Обрабатываем ошибки. Также красим консоль в красный.
            catch (ArgumentException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Некорректное название файла, повторите попытку.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (IOException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого файла нет либо он не открывается, повторите попытку.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неизвестная ошибка, возьмите с полки пирожок, подайте заявку на тестировщика в Яндекс и повторите попытку.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
