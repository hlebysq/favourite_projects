// Золотухин Глеб Андреевич БПИ237-1 Вариант 1.
//  C:\Users\Gleb\Downloads\ЗАГС-инфо.csv

using CustomClasses;
internal class Program
{
    static void Main()
    {
        while (true)
        {
            try
            {
                string[] allLines = CsvProcessing.Read();
                string[][] allEl = CsvProcessing.ArrToArrArr(allLines);
                List<Zags> zags = new List<Zags>();
                for (int i = 1; i < allEl.Length; i++)
                {
                    if (allEl[i].Length > 4)
                    {
                        zags.Add(new Zags(allEl[i]));
                    }
                }
                Menu.CallMenu(zags.ToArray());
                break; // Заканчиваем, если в CallMenu нажали 7.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
