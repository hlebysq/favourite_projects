using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace ClassLibrary1
{
    /// <summary>
    /// Нестатический класс для работы с CSV файлами.
    /// </summary>
    public class CSVprocessing
    {
        /// <summary>
        /// Метод для записи в поток.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Stream CsvWrite(List<HockeyData> list)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            var csvWriter = new CsvWriter(writer, config);
            writer.WriteLine("\"global_id\";\"Название спортивного объекта\";\"" +
                "Название спортивной зоны в зимний период\";\"Фотография в зимн" +
                "ий период\";\"Административный округ\";\"Район\";\"Адрес\";\"А" +
                "дрес электронной почты\";\"Адрес сайта\";\"Справочный телефон\"" +
                ";\"Добавочный номер\";\"График работы в зимний период\";\"Уточне" +
                "ние графика работы в зимний период\";\"Вид собственности\";\"Тип" +
                " ведомственной принадлежности\";\"Возможность проката оборудован" +
                "ия\";\"Комментарии для проката оборудования\";\"Наличие сервиса " +
                "технического обслуживания\";\"Комментарии для сервиса техническо" +
                "го обслуживания\";\"Наличие раздевалки\";\"Наличие точки питани" +
                "я\";\"Наличие туалета\";\"Наличие точки Wi-Fi\";\"Наличие банкома" +
                "та\";\"Наличие медпункта\";\"Наличие звукового сопровождения\";\"Пе" +
                "риод эксплуатации в зимний период\";\"Статус функционирования\";\"Раз" +
                "меры в зимний период\";\"Освещение\";\"Покрытие в зимний период\";\"Коли" +
                "чество оборудованных посадочных мест\";\"Форма посещения (платность)\";\"" +
                "Комментарии к стоимости посещения\";\"Приспособленность для занятий инвали" +
                "дов\";\"Услуги предоставляемые в зимний период\";\"geoData\";\"geodata_c" +
                "enter\";\"geoarea\";");
            csvWriter.WriteRecords(list);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        /// <summary>
        /// Метод для чтения из потока.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public List<HockeyData> CsvRead(Stream stream)
        {
            var allLines = new List<string>();
            var hockeyList = new List<HockeyData>();
            string line;

            stream.Position = 0;

            using (var reader = new StreamReader(stream))
            {
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    allLines.Add(line);
                }
            };

            for (int i = 2; i < allLines.Count; i++)
            {
                hockeyList.Add(new HockeyData(allLines[i].Split(";")));
            }
            return hockeyList;
        }
    }
}
