//Золотухин 237-1 Вариант 13
using Flex;

internal class Program
{
    static void Main()
    {
        do
        {
            while (true)
            {
                bool flag = false, isRelease;
                do
                {
                    Console.WriteLine("Файл в релизной версии Writer'а? (true если да false если нет)"); // Данное решение вызвано тем, что debug и release
                                                                                                         // версии writer'а сохраняют файл в разные папки.
                    flag = bool.TryParse(Console.ReadLine(), out isRelease);
                    if (!flag) { 
                        Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Это не true или false. Повторите попытку.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    } // Пока не узнаем релизная это версия или нет не продолжим работу.
                } while (!flag);
                string[] strArr = Methods.ReadFile(isRelease); // Читаем файл в массив строк.
                double[,] finalArr, firstArr; 
                // Если не вышло - повторяем ввод.
                if (!Methods.ReadArr(strArr, out firstArr))
                {
                    continue;
                }
                Methods.FullArr(firstArr, out finalArr); // Заполняем итоговый массив.
                Console.WriteLine("Before:");
                Methods.WriteAllEl(firstArr);
                Console.WriteLine("After");
                Methods.WriteAllEl(finalArr);
                break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Структура с данными успешно считана!");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Нажмите любую клавишу либо escape чтобы завершить работу.");
        } while (Console.ReadKey().Key != ConsoleKey.Escape); // Бесконечный ввод.
        FlexConsole.Amogus(); // Когда завершается работа рисуем [ДАННЫЕ УДАЛЕНЫ].
    }
}