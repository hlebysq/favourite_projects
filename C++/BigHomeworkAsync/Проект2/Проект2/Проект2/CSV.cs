using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Проект2;
namespace Проект2
{
    /// <summary>
    /// Класс для создания объектов позволяющих получать доступ к csv файлам.
    /// </summary>
    /// <param name="pathin">Путь к csv файлу</param>
    public class CSV(string pathin)
    {
        private readonly string pathin = File.Exists(pathin) ? pathin : throw new System.IO.FileNotFoundException();

        /// <summary>
        /// Обрабатывает данные из csv файла.
        /// </summary>
        /// <returns>Массив массивов строк, где каждый массив представляет одну строку в исходном файле.</returns>
        public string[][] Get_Data()
        {
            string[] array_of_str_in = File.ReadAllLines(pathin);
            string[][] array_of_str_out = new string[array_of_str_in.Length][];
            for (int i = 0; i < array_of_str_in.Length; i++)
            {
                array_of_str_out[i] = array_of_str_in[i].Split(',');
            }
            return array_of_str_out;
        }
    }
}
