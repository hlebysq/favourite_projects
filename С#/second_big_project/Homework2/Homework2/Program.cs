// Золотухин Глеб Андреевич БПИ237-1 Вариант 9.


using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using CustomClasses;
internal class Program
{
    static void Main()
    {
        while(true)
        {
            try
            {
                string[] allLines = CsvProcessing.Read();
                string[][] allEl = CsvProcessing.ArrToArrArr(allLines);
                CsvProcessing.CallMenu(allEl);
                break; // Заканчиваем, если в CallMenu нажали 6.
            }
            catch (ArgumentNullException e) // Сюда нас бросают Write и Read.
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("У меня нет прав.");
            }
            catch (Exception e) // Если все совсем грустно.
            {
                Console.WriteLine("Что-то пошло не так! сорян)");
            }
        } 

    }
}
