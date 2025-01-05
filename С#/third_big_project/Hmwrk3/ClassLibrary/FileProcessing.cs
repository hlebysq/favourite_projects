using System.Security;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Класс, содержащий методы, предназначенные для работы с файлами.
    /// </summary>
    public static class FileProcessing
    {
        /// <summary>
        /// Метод, считывающий входные данные из файла. Возвращает все строки файла, слепленные в одну.
        /// </summary>
        public static string ReadFile()
        {
            while (true)
            {
                string name = NameInput("входными"); 
                
                try
                {
                    StringBuilder sb = new StringBuilder(); // Можно было использовать строку, но SB оптимальнее.
                    string[] allLinesList = File.ReadAllLines(name);
                    foreach (string el in allLinesList) // Прибавляем все строки к одной большой.
                    {
                        sb.Append(el);
                    }
                    string ans = sb.ToString();
                    bool flag = HelpMethods.TestFormat(ans); // Проверка на формат.
                    if (!flag)
                    {
                        Console.WriteLine("Формат данных в файле нарушен. Повторите попытку.");
                        continue;
                    }
                    Console.WriteLine("Данные успешно считаны.");
                    return ans;
                }
                // Много исключений(в основном у метода ReadAllLines), в соответствующих writeline'ах все расписано.
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Файла с таким названием нет. Повторите попытку.");
                }
                catch (PathTooLongException ex)
                {
                    Console.WriteLine("Слишком длинное название. Повторите попытку.");
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine("У меня нет прав. Повторите попытку.");
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Файла с таким названием нет. Повторите попытку.");
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine("Файла с таким названием нет. Повторите попытку.");
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine("Неверный формат файла! Повторите попытку.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Файла с таким названием нет. Повторите попытку.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.Write("Скорее всего, вы пытаетесь меня сломать этим файлом. Плохая идея! Повторите попытку.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Что-то с данным файлом не так! Повторите попытку.");
                }
            }
        }
        /// <summary>
        /// Метод, создающий новый файл с выходными данными.
        /// </summary>
        public static string CreateFile()
        {
            while (true)
            {
                string name = NameInput("выходными");
                try
                {
                    File.Create(name).Close();
                    Console.WriteLine($"Файл с названием {name} успешно создан.");
                    return name;
                }
                // Если создать файл с таким именем не получается оповещаем об этом пользователя и 
                // возвращаемся в начало. По идее мы отклоняем всю запрещенку на этапе NameInput, но
                // если вдруг нет - программа не упадет.
                catch (Exception ex)
                {
                    Console.WriteLine("Что-то с данным названием не так! Повторите попытку.");
                }
            }
        }
        /// <summary>
        /// Метод, реализующий ввод корректного названия файла.
        /// </summary>
        public static string NameInput(string arg)
        {
            while (true)
            {
                Console.Write($"Введите название файла с {arg} данными: ");
                string? name = Console.ReadLine(); 
                if (name is null) // Проверка на null - это важно, т.к. файл без названия создать нельзя.
                {
                    Console.WriteLine("Не название, а null какой-то! Повторите попытку.");
                    continue;
                }
                if (name == "") // Проверка на пустую строку - это важно, т.к. файл без названия создать нельзя.
                {
                    Console.WriteLine("Не null конечно, но тоже непорядок. Повторите попытку.");
                    continue;
                }
                char[] notAllowed = Path.GetInvalidFileNameChars();
                if (notAllowed.Any(name.Contains)) // Проверка на запрещенные символы
                {
                    Console.WriteLine("Запрещенные символы вижу я, падаван юный! Повторите попытку.");
                    continue;
                }
                if (HelpMethods.IsBanWord(name)) // Проверка на запрещенные названия.
                {
                    Console.WriteLine("Хорошая попытка положить код! Но идея плохая. " +
                        "Это название запрещено в ОС Windows. Повторите попытку.");
                    continue;
                }
                return name;
            }
        }
    }
}
