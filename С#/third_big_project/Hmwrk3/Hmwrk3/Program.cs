/// Золотухин Глеб БПИ237-1 Вариант 4.

using System.Text;
using ClassLibrary;

internal class Program
{
    private static void Main()
    {
        do
        {
            try
            {
                Console.Clear();
                // Получаем название файла, сразу же считываем его и слепляем в одну большую строку.
                string allLines = FileProcessing.ReadFile();
                string output = FileProcessing.CreateFile(); // Создаем файл, название сохраняем для будущих изменений.
                string[] allLinesSplit = allLines.Split('.'); // Дробим нашу большую строку на много маленьких предложений.
                MyStrings[] Array = new MyStrings[allLinesSplit.Length]; // Массив объектов класса MyString.
                Console.WriteLine("Результат работы:");
                for (int i = 0; i < allLinesSplit.Length; i++)
                {
                    Array[i] = new MyStrings(allLinesSplit[i], ' '); // Конструируем элемент массива, деля по пробелу.
                    string[] abbrsArray = Array[i].ABBR; 
                    string[] wordsArray = Array[i].Sentences;
                    StringBuilder str = new StringBuilder(), abbr = new StringBuilder();
                    for (int j = 0; j < wordsArray.Length; j++)
                    {
                        // Этим условием мы отсекаем пустые слова, позволяя корректно обрабатывать множество файлов.
                        // Несколько простых примеров - многоточие, множественные пробелы, пробелы после точек.
                        if (wordsArray[j] == "")
                        {
                            continue;
                        }
                        // Можно было сделать наоборот - занести две строчки после if'а туда, но я сделал отрицание чтобы
                        // уменьшить вложенность кода.
                        str.Append(wordsArray[j] + ' ');
                        abbr.Append(abbrsArray[j]);
                    }
                    // Часть вышеупомянутого условия про пустые слова, но теперь отдельно отсекаем случай, когда все слова
                    // пустые и у нас пустое предложение.
                    if (str.Length > 0)
                    {
                        // Объединяем, выводим и записываем в файл объединенные исходное предложение и аббревиатуру.
                        string finalText = str.ToString() + ": " + abbr.ToString();
                        File.AppendAllText(output, finalText+'\n');
                        Console.WriteLine(finalText);
                    }
                }

            }
            /*Вообще - все ошибки обрабатываются в методах и до исключений в Main дело доходить не должно.
            Можно было пойти по другому пути - во всех методах бросать ошибки и вылавливать их в мейне с соответствующим
            комментарием, но в контексте этой достаточно простой программы мне показалось это менее уместным.
            Но даже если программу поломают каким-то условием, которое я не учел, она не упадет, а сообщит, что
            что-то пошло не так, выведет сообщение ошибки и продолжит работу. */
            catch (Exception ex)
            {
                Console.WriteLine($"Что-то пошло не так! Ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Нажмите любую клавишу чтобы продолжить либо Escape чтобы завершить работу.");
            }
        } while (Console.ReadKey().Key != ConsoleKey.Escape); // Бесконечный повтор решения.
    }
}