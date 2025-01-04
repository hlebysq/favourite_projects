using Flex;

public class Methods
{
    /// <summary>
    /// Заполнение двумерного массива размера NxM согласно задаче.
    /// </summary>
    /// <param name="N"></param>
    /// <param name="M"></param>
    /// <param name="Arr"></param>
    public static void Change(int N, int M, out double[,] Arr)
    {
        double[,] NewArr = new double[N, M];
        int n = 1;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                NewArr[i, j] = Math.Sqrt(n * n * n + 1) - Math.Sqrt(n); // Формула.
                n++; // Увеличиваем n, получая то, что требуется в задаче.
            }
        }

        Arr = NewArr;
    }

    /// <summary>
    /// Создаем и заполняем файл.
    /// </summary>
    /// <param name="N"></param>
    /// <param name="M"></param>
    /// <param name="Arr"></param>
    public static void FillFile(int N, int M, ref string[] Arr)
    {
        Console.Write("Введите название файла: ");
        // Пока не получим корректное название файла работа не закончится.
        while (true)
        {
            try
            {
                var path = Console.ReadLine();
                File.WriteAllLines(path, Arr); // Создаем файл по пути path
                // и переносим все элементы массива строк в строки файла.
                FlexConsole.Hse(); // :)
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Файл с данными успешно создан!");
                Console.ForegroundColor = ConsoleColor.Gray;

                break;
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
                Console.WriteLine("Ошибка при открытии файла и записи структуры, повторите попытку.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Недопустимые символы! Повторите попытку.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    "Неизвестная ошибка, возьмите с полки пирожок, подайте заявку на тестировщика в Яндекс и повторите попытку.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
    /// <summary>
    /// Присваивание случайных значений N и M.
    /// </summary>
    /// <param name="N"></param>
    /// <param name="M"></param>
    public static void RndFill(out int N, out int M)
    {
        // Генерируем объект rnd и с его помощью присваиваем N и M случайные значения в заданном диапазоне.
        Random rnd = new Random();
        N = rnd.Next(1, 16);
        M = rnd.Next(1, 11);
    }
}