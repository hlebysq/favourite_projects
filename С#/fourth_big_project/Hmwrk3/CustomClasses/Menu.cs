namespace CustomClasses
{
    /// <summary>
    /// Класс, в котором хранятся интерфейсы взаимодействия с пользователем.
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// Метод, вызывающий основное меню, через которое мы общаемся с пользователем.
        /// </summary>
        /// <param name="table"></param>
        public static void CallMenu(Zags[] table)
        {
            Console.Clear();
            Console.WriteLine("Укажите номер пункта меню для запуска действия:");
            Console.WriteLine("1. Произвести фильтрацию по значению AdmArea");
            Console.WriteLine("2. Произвести фильтрацию по значению AdmAreaCode");
            Console.WriteLine("3. Произвести фильтрацию по значениям AdmArea и AdmAreaCode");
            Console.WriteLine("4. Отсортировать таблицу (в порядке возрастания)");
            Console.WriteLine("5. Отсортировать таблицу (в порядке убывания)");
            Console.WriteLine("6. Вывести первые или последние n элементов.");
            Console.WriteLine("7. Завершить работу программы.");
            while (true)
            {
                // Получаем от пользователя символ, пока не получим цифру [1,7].
                char num = Console.ReadKey().KeyChar;
                bool flag = false;
                switch (num) // Свитчкейсом полученную цифру обработатываем, вызвав соответствущий метод/завершив работу.
                {
                    case '1':
                        DataProcessing.Choise(table, "AdmArea");
                        flag = true;
                        break;
                    case '2':
                        DataProcessing.Choise(table, "AdmAreaCode");
                        flag = true;
                        break;
                    case '3':
                        DataProcessing.Choise(table, "AdmArea", "AdmAreaCode");
                        flag = true;
                        break;
                    case '4':
                        DataProcessing.SortAlph(table, "AdmArea", true);
                        flag = true;
                        break;
                    case '5':
                        DataProcessing.SortAlph(table, "AdmArea", false);
                        flag = true;
                        break;
                    case '6':
                        TopOrBottomEls.Select(table);
                        break;
                    case '7':
                        return;
                }
                if (flag)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Метод, возвращающий в меню.
        /// </summary>
        /// <param name="table"></param>
        public static void ReturnToMenu(Zags[] table)
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("Чтобы вернуться к меню нажмите любую клавишу.");
            char temp = Console.ReadKey().KeyChar;
            Menu.CallMenu(table);
        }
        /// <summary>
        /// Метод, вызывающий интерфейс для общения с пользователем о файле.
        /// </summary>
        /// <param name="table"></param>
        public static void TalkAboutFile(Zags[] table)
        {
            while (true)
            {
                bool flag = false;
                try
                {
                    Console.Clear();
                    Console.WriteLine("Как вы хотите сохранить файл?");
                    Console.WriteLine("1. Создать новый файл.");
                    Console.WriteLine("2. Заменить содержимое уже существующего файла.");
                    Console.WriteLine("3. Добавить сохраняемые данные к содержимому существующего файла (если этот файл соответствует формату).");
                    Console.WriteLine("4. Не хочу сохранять результаты обработки.");
                    char num = Console.ReadKey().KeyChar;
                    switch (num)
                    {
                        case '1':
                            CsvProcessing.CreateFile(table);
                            flag = true;
                            break;
                        case '2':
                            CsvProcessing.ReplaceFile(table);
                            flag = true;
                            break;
                        case '3':
                            CsvProcessing.AddToFile(table);
                            flag = true;
                            break;
                        case '4':
                            flag = true;
                            break;
                        default:
                            continue;
                    }
                    if (flag)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                    Console.WriteLine("Нажмите любую клавишу чтобы продолжить.");
                    char tmp = Console.ReadKey().KeyChar;
                }
            }
        }
    }
}
