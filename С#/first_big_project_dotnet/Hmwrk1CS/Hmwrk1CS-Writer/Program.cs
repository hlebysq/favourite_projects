// Золотухин 237-1 Вариант 13

using System.Text;

internal class Program
{
    static void Main()
    {
        do
        {
            int N, M;
            Methods.RndFill(out N, out M); // Присваиваем N и M случайные значения в диапазоне.
            double[,] Arr = new double[N, M]; // Массив размерности NxM.
            Methods.Change(N, M, out Arr); // Заполняем массив функцией.
                                           
            string[] strArr = new string[N]; // Создаем массив строк и заполняем его, чтобы в будущем записать в файл.
            for (int i = 0; i < N; i++)
            {
                StringBuilder str = new StringBuilder();
                for (int j = 0; j < M - 1; j++)
                {
                    str.Append($"{ Arr[i, j]:f3}" + ";"); // Формируем строку.
                }
                str.Append($"{ Arr[i, M - 1]:f3}"); // Чтобы ; был только между числами перебираем на 1 эл меньше
                                           // и добавляем его вручную в конце.
                strArr[i] = str.ToString(); // Заполняем массив со строками.
            }
            Methods.FillFile(N, M, ref strArr); // Создаем и заполняем файл, если такой уже есть перезаписываем.
            Console.WriteLine("Нажмите любую клавишу либо escape чтобы завершить работу.");
        } while (Console.ReadKey().Key != ConsoleKey.Escape); // Бесконечный ввод.
    }
}