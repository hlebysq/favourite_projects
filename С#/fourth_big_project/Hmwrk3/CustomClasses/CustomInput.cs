using static CustomClasses.TopOrBottomEls;

namespace CustomClasses
{
    /// <summary>
    /// Класс, в котором хранятся методы для получения данных с консоли от пользователя.
    /// </summary>
    public static class CustomInput
    {
        /// <summary>
        /// Метод StringPut, который формирует искомый элемент на основе данных от пользователя.
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
        /// Метод, получающий абсолютный путь от пользователя.
        /// </summary>
        /// <returns></returns>
        public static string ReadPath()
        {
            while (true)
            {
                try
                {
                    Console.Write("Введите абсолютный путь до файла: ");
                    string? path = Console.ReadLine();
                    if (path is null) // Проверка на null.
                    {
                        Console.WriteLine("Не строка, а null какой-то! Повторите попытку.");
                        continue;
                    }
                    if (!Methods.IsAbsolut(path)) // Проверка на абсолютность.
                    {
                        Console.WriteLine("Это не абсолютный путь! Повторите попытку.");
                        continue;
                    }
                    if (Path.GetInvalidPathChars().Any(path.Contains)) // Проверка на запрещенные символы.
                    {
                        Console.WriteLine("Запрещенные символы вижу я, падаван юный! Повторите попытку.");
                        continue;
                    }
                    return path;
                }
                catch (Exception e)
                {
                    Console.WriteLine("С путем что-то не так! Повторите попытку.");
                    continue;
                }
            }
        }
        /// <summary>
        /// Метод, получающий информацию, элементы сверху или снизу мы берем.
        /// </summary>
        /// <returns></returns>
        public static TopOrBottom TopOrBottomInput()
        {
            Console.Clear();
            Console.WriteLine("Выберите какие данные вывести.");
            Console.WriteLine("1. Первые n сверху.");
            Console.WriteLine("2. Первые n снизу.");
            char ans = Console.ReadKey().KeyChar;
            while (true)
            {
                switch (ans)
                {
                    case '1':
                        return TopOrBottom.Top;
                    case '2':
                        return TopOrBottom.Bottom;
                    default:
                        continue;
                }
            }
        }
        /// <summary>
        /// Метод, получающий от пользователя целое значение в диапазоне.
        /// </summary>
        /// <param name="MinValue"></param>
        /// <param name="MaxValue"></param>
        /// <returns></returns>
        public static int IntPut(int MinValue, int MaxValue)
        {
            while (true)
            {
                Console.Write($"Введите целое значение из диапазона [{MinValue}, {MaxValue}]: ");
                int ans;
                bool flag = int.TryParse(Console.ReadLine(), out ans);
                if (!flag || ans < MinValue || ans > MaxValue)
                {
                    Console.WriteLine("Некорректные данные. Повторите попытку!");
                    continue;
                }
                return ans;
            }
        }
    }
}
