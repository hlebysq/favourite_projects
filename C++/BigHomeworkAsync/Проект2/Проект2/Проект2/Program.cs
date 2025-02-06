//Голубев Андрей Константинович БПИ246 Вариант 17.

using System;
using System.Security.Cryptography.X509Certificates;
using Проект2;
public class Program
{
    /// <summary>
    /// Бин поиск для 9 пункта
    /// </summary>
    /// <param name="doubles">Список с отсортированными числами</param>
    /// <param name="x">Число которое нужно вставить в список</param>
    /// <returns>Индекс, куда нужно вставить х, чтобы список остался отсортированным</returns>
    public static int BinSearch(List<double> doubles, double x) // Бин поиск для 9 пункта
    {
        if (doubles.Count == 0)
        {
            return 0;
        }
        int l = 0, r = doubles.Count;
        if (doubles[0] < x)
        {
            return 0;
        }
        else if (doubles[^1] > x)
        {
            return doubles.Count;
        }
        else
        {
            while (r - l > 1)
            {
                if (doubles[(r + l) / 2] >= x)
                {
                    l = (r + l) / 2;
                }
                else
                {
                    r = (r + l) / 2;
                }
            }
            return r;
        }

    }
    public static void Main()
    {
        try
        {
            bool end_of_while_first = false; // Меняю один раз. Влияет на конец все программы.
            bool end_of_while_second; // Меняю много раз. Влияет на конец второго цикла while.
            bool end_of_while_third; // Меняю много раз. Влияет на конец третьего цикла while.
            CSV csv1; // Объект типа CSV
            WeatherRec[] weatherRecs; // Массив объектов типа WeatherRec.
            while (!end_of_while_first) // Проверяет на конец всей программы.
            {
                end_of_while_second = false;
                while (!end_of_while_second) // Проверяет на запрос повторного ввода файла.
                {
                    Console.WriteLine("Введите адрес файла с данными");
                    string pathin = Console.ReadLine(); // Путь к файлу с входными данными.
                    string[][] str; // Массив для хранения данных, полученных из файла.
                    try
                    {
                        csv1 = new CSV(pathin);
                        str = csv1.Get_Data(); // Получаем данные.
                    }
                    catch (System.IO.FileNotFoundException)
                    {
                        Console.WriteLine("Файл не существует");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Некорректный путь");
                        break;
                    }
                    try
                    {
                        weatherRecs = new WeatherRec[str.Length - 1]; // Выделяю память под объекты типа WeatherRec.
                        for (int i = 1; i < str.Length; i++) // Заполняю массив объектами типа WeatherRec.
                        {
                            weatherRecs[i - 1] = new WeatherRec(str[i]);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Неверный формат данных");
                        break;
                    }
                    string offers;
                    {
                        offers = "Введите соответствующую цифру:\n" +
                        "1. Поменять файл с данными\n" +
                        "2. Вывести данные о погоде в Сиднее в 2009 - 2010 годах\n" +
                        "3. Вывести количество дней пригодных для рыбалки\n" +
                        "4. Вывести информацию про количество групп записей по локации и кол-во записей в каждой группе\n" +
                        "5. Вывести количество теплых дождливых дней\n" +
                        "6. Вывести количество дней с нормальным атмосферным давлением с утра.\n" +
                        "7. Закончить выполнение программы\n" +
                        "8. Вывести выборку записей дней, когда солнечная погода держалась хотя бы 4 часа за день\n" +
                        "9. Вывести переупорядоченный набор исходных данных о записях, в котором выделены группы по месту расположения станции сбора метеоданных";
                    }
                    end_of_while_third = false; // Меняю много раз.
                    while (!end_of_while_third)
                    {
                        Console.WriteLine(offers);
                        switch (Console.ReadLine())
                        {
                            case "1":
                                {
                                    end_of_while_third = true;
                                    break;
                                }
                            case "2":
                                {
                                    List<string> strings = []; // Список для хранения строк для вывода
                                    Console.WriteLine("Вывести данные в файл или в консоль? Файл - 1, Консоль - 2");
                                    switch (Console.ReadLine())
                                    {
                                        case "1":
                                            {
                                                try
                                                {
                                                    foreach (WeatherRec i in weatherRecs)
                                                    {
                                                        if (i.Location == "Sydney" && (i.Date.Year == 2009 || i.Date.Year == 2010))
                                                        {
                                                            strings.Add(i.ToString_with_comma());
                                                        }
                                                    }
                                                    File.WriteAllLines("../../../Sydney_2009_2010_weatherAUS.csv", strings); // Вывод итоговый
                                                    Console.WriteLine("Успешно!");
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Не удается записать данные в файл");
                                                }
                                                break;
                                            }
                                        case "2":
                                            {
                                                foreach (WeatherRec i in weatherRecs)
                                                {
                                                    if (i.Location == "Sydney" && (i.Date.Year == 2009 || i.Date.Year == 2010))
                                                    {
                                                        strings.Add(i.ToString());
                                                    }
                                                }
                                                Console.WriteLine(string.Join("\n", strings)); // Вывод итоговый
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Неправильная цифра");
                                                break;
                                            }


                                    }
                                    break;
                                }
                            case "3":
                                {
                                    int count = 0; // Счетчик
                                    foreach (WeatherRec i in weatherRecs)
                                    {
                                        try
                                        {
                                            if (i.WindSpeed3pm == null)
                                            {
                                                continue;
                                            }

                                            if (i.WindSpeed3pm < 13)
                                            {
                                                count++;
                                            }
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                    Console.WriteLine(count.ToString());
                                    break;
                                }
                            case "4":
                                {
                                    List<string> s = []; // Список локаций
                                    List<int> quantities = []; // Список кол-ва записей по локациям
                                    foreach (WeatherRec i in weatherRecs)
                                    {
                                        if (!s.Contains(i.Location))
                                        {
                                            s.Add(i.Location);
                                            quantities.Add(1);
                                        }
                                        else
                                        {
                                            quantities[s.IndexOf(i.Location)]++;
                                        }
                                    }
                                    Console.WriteLine($"Всего локаций - {s.Count}"); // Итоговый вывод
                                    for (int i = 0; i < s.Count; i++)
                                    {
                                        Console.WriteLine($"{s[i]}: {quantities[i]}"); //Итоговый вывод
                                    }
                                    break;
                                }
                            case "5":
                                {
                                    int count = 0; // Счетчик
                                    foreach (WeatherRec i in weatherRecs)
                                    {
                                        try
                                        {
                                            if (i.MaxTemp == null)
                                            {
                                                continue;
                                            }
                                            if (i.RainToday == "Yes" && i.MaxTemp >= 20)
                                            {
                                                count++;
                                            }
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                    Console.WriteLine(count); // Итоговый вывод
                                    break;
                                }
                            case "6":
                                {
                                    int count = 0; // Счетчик
                                    foreach (WeatherRec i in weatherRecs)
                                    {
                                        try
                                        {
                                            if (i.Pressure9am == null)
                                            {
                                                continue;
                                            }
                                            if (i.Pressure9am is >= 1000 and <= 1007)
                                            {
                                                count++;
                                            }
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                    }
                                    Console.WriteLine(count); // Итоговый вывод
                                    break;
                                }
                            case "7":
                                {
                                    //Завершаем сразу три цикла для выхода из программы
                                    end_of_while_first = true;
                                    end_of_while_second = true;
                                    end_of_while_third = true;
                                    break;
                                }
                            case "8":
                                {
                                    bool is_elwithmaxSunshine_full = false;  // True - если el_with_maxSunshine заполнен объектом, у которого определено поле Sunshine, и false иначе.
                                    WeatherRec el_with_maxSunshine;
                                    try
                                    {
                                        el_with_maxSunshine = weatherRecs[0];
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                    List<string> list = [];
                                    Console.WriteLine("Вывести данные в файл или в консоль? 1 - В файл, 2 - В Консоль");
                                    string x = Console.ReadLine();
                                    if (x is not ("1" or "2"))
                                    {
                                        Console.WriteLine("Некорректная цифра");
                                        break;
                                    }
                                    foreach (WeatherRec i in weatherRecs)
                                    {
                                        if (i.Sunshine == null)
                                        {
                                            continue;
                                        }
                                        if (!is_elwithmaxSunshine_full)
                                        {
                                            el_with_maxSunshine = i;
                                            is_elwithmaxSunshine_full = true;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (el_with_maxSunshine.Sunshine < i.Sunshine && el_with_maxSunshine.Sunshine != null && i.Sunshine != null)
                                                {
                                                    el_with_maxSunshine = i;
                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                        try
                                        {
                                            if (i.Sunshine == null)
                                            {
                                                continue;
                                            }
                                            if (i.Sunshine is >= 4)
                                            {
                                                switch (x)
                                                {
                                                    case "1":
                                                        {
                                                            list.Add(i.ToString_with_comma());
                                                            break;
                                                        }
                                                    case "2":
                                                        {
                                                            list.Add(i.ToString());
                                                            break;
                                                        }
                                                }
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }
                                    if (is_elwithmaxSunshine_full)
                                    {
                                        Console.WriteLine($"{el_with_maxSunshine.Date.Year}-{el_with_maxSunshine.Date.Month}-{el_with_maxSunshine.Date.Day}\t {el_with_maxSunshine.MaxTemp.ToString().Replace(',', '.')}");

                                        if (x == "1")
                                        {
                                            Console.WriteLine("Введите путь к файлу");
                                            string path = Console.ReadLine();
                                            try
                                            {
                                                File.WriteAllLines(path, list);
                                                Console.WriteLine("Успешно!");
                                            }
                                            catch
                                            {
                                                Console.WriteLine("Не удается записать в файл");
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine(string.Join("\n", list));
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Нет данных о солнечной погоде.");
                                    }
                                    break;
                                }
                            case "9":
                                {
                                    List<string> list_of_locations = [];
                                    List<List<WeatherRec>> list_of_weather_recs = [];
                                    List<List<double>> list_of_Rainfalls = [];
                                    foreach (WeatherRec i in weatherRecs)
                                    {
                                        if (!list_of_locations.Contains(i.Location))
                                        {
                                            list_of_locations.Add(i.Location);
                                            list_of_weather_recs.Add([]);
                                            list_of_Rainfalls.Add([]);
                                        }
                                        if (i.Rainfall == null)
                                        {
                                            list_of_weather_recs[list_of_locations.IndexOf(i.Location)].Add(i);
                                            list_of_Rainfalls[list_of_locations.IndexOf(i.Location)].Add(-1);
                                        }
                                        else
                                        {
                                            int j = BinSearch(list_of_Rainfalls[list_of_locations.IndexOf(i.Location)], (double)i.Rainfall);
                                            list_of_weather_recs[list_of_locations.IndexOf(i.Location)].Insert(j, i);
                                            list_of_Rainfalls[list_of_locations.IndexOf(i.Location)].Insert(j, (double)i.Rainfall);
                                        }
                                    }
                                    Console.WriteLine("Вывести в файл или в консоль. Файл - 1, Консоль - 2");
                                    switch (Console.ReadLine())
                                    {
                                        case "1":
                                            {
                                                List<string> string9_output = [];
                                                foreach (List<WeatherRec> list_cursor in list_of_weather_recs)
                                                {
                                                    foreach (WeatherRec item in list_cursor)
                                                    {
                                                        string9_output.Add(item.ToString_with_comma());
                                                    }
                                                }
                                                string path9 = "../../../average_rain_weatherAUS.csv";
                                                try
                                                {
                                                    File.WriteAllLines(path9, string9_output);
                                                    Console.WriteLine("Успешно!");
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("Не удается вывести в файл");
                                                }
                                                break;
                                            }
                                        case "2":
                                            {
                                                foreach (List<WeatherRec> list_cursor in list_of_weather_recs)
                                                {
                                                    double sum = 0;
                                                    int len = 0;
                                                    foreach (WeatherRec weatherRec_cursor in list_cursor)
                                                    {
                                                        if (weatherRec_cursor.Rainfall != null)
                                                        {
                                                            sum += (double)weatherRec_cursor.Rainfall;
                                                            len++;
                                                        }
                                                    }
                                                    if (len != 0)
                                                    {
                                                        Console.WriteLine($"Средние осадки в группе {list_cursor[0].Location} по имеющимся данным равны " + (sum / len));
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Нет информации про осадки");
                                                    }
                                                    Console.WriteLine(string.Join("\n", list_cursor));
                                                }
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Введена неверная цифра");
                                                break;
                                            }
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Некорректная цифра");
                                    break;
                                }
                        }
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine("Программа выдала ошибку (-_-)");
        }
    }
}