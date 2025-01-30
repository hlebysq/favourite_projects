using System.Text.Encodings.Web;
using System.Text.Json;

namespace ClassLibrary1
{
    /// <summary>
    /// Нестатический класс для работы с JSON файлами.
    /// </summary>
    public class JSONprocessing
    {
        /// <summary>
        /// Метод для записи в поток.
        /// </summary>
        /// <param name="hockeyList"></param>
        /// <returns></returns>
        public Stream JsonWrite(List<HockeyData> hockeyList)
        {
            var stream = new MemoryStream();
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            using (var writer = new StreamWriter(stream, leaveOpen: true))
            {
                writer.WriteLine(System.Text.Json.JsonSerializer.Serialize(hockeyList, options));
            }
            stream.Position = 0;
            return stream;
        }
        /// <summary>
        /// Метод для чтения из потока.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public List<HockeyData> JsonRead(Stream stream)
        {
            var allLines = new List<string>();
            var allAttractions = new List<HockeyData>();
            string line;

            stream.Position = 0;

            using (var reader = new StreamReader(stream))
            {
                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {

                    allLines.Add(line);
                }
            };

            JsonElement jsonElement = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(string.Join("", allLines));

            foreach (JsonElement element in jsonElement.EnumerateArray())
            {
                allAttractions.Add(new HockeyData(element));
            }

            return allAttractions;
        }
    }
}
