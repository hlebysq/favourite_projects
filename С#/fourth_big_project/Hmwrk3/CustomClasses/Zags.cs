using System.Text;
using System.Collections.Specialized;

namespace CustomClasses
{
    /// <summary>
    /// Класс, в котором хранится информация о ЗАГСе.
    /// </summary>
    public class Zags : IComparable<Zags>
    {
        private OrderedDictionary _dict; /* Все данные хранятся в упорядоченном словаре. Можно было хранить в виде 22 переменных,
                                          * но это мазохизм. Можно было в виде массива или листа, но это костыли. Поэтому так. */
        private Address _address;
        /// <summary>
        /// Реализация метода сравнения двух ЗАГСов из интерфейса IComparable.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo(Zags other)
        {
            try
            {
                return this["AdmArea"].CompareTo(other["AdmArea"]);
            }
            catch(Exception ex)
            {
                throw new ArgumentException("Эти два ЗАГСа сравнить не получится.");
            }
        }
        /// <summary>
        /// Пустой конструктор.
        /// </summary>
        public Zags() 
        {
            _address = new Address();
            _dict = new OrderedDictionary
                {
                    { "ROWNUM", "" },
                    { "CommonName", "" },
                    { "FullName", "" },
                    { "ShortName", "" },
                    { "AdmAreaCode", 0 },
                    { "AdmArea", "" },
                    { "District", "" },
                    { "PostalCode", 0 },
                    { "Address", "" },
                    { "NearestMetroStations", "" },
                    { "ChiefName", "" },
                    { "ChiefPosition", "" },
                    { "ChiefPhone", "" },
                    { "ContactPhone", "" },
                    { "ArchivePhone", "" },
                    { "SignPGU", "" },
                    { "WorkingHours", "" },
                    { "ClarificationOfWorkingHours", "" },
                    { "WebSite", "" },
                    { "X_WGS", "" },
                    { "Y_WGS", "" },
                    { "GLOBALID", "" }
            };

        }
        /// <summary>
        /// Конструктор, задающий значения всем полям.
        /// </summary>
        /// <param name="elements"></param>
        /// <exception cref="ArgumentException"></exception>
        public Zags(string[] elements)
        {
            try
            {
                if (elements.Length > 22)
                {
                    throw new ArgumentException("С форматом что-то не так!" );
                }
                _dict = new OrderedDictionary
                {
                    { "ROWNUM", elements[0] },
                    { "CommonName", elements[1] },
                    { "FullName", elements[2] },
                    { "ShortName", elements[3] },
                    { "AdmAreaCode", int.Parse(elements[4]) },
                    { "AdmArea", elements[5] },
                    { "District", elements[6] },
                    { "PostalCode", int.Parse(elements[7]) },
                    { "Address", elements[8] },
                    { "NearestMetroStations", elements[9] },
                    { "ChiefName", elements[10] },
                    { "ChiefPosition", elements[11] },
                    { "ChiefPhone", elements[12] },
                    { "ContactPhone", elements[13] },
                    { "ArchivePhone", elements[14] },
                    { "SignPGU", elements[15] },
                    { "WorkingHours", elements[16] },
                    { "ClarificationOfWorkingHours", elements[17] },
                    { "WebSite", elements[18] },
                    { "X_WGS", elements[19] },
                    { "Y_WGS", elements[20] },
                    { "GLOBALID", elements[21] },

                };
                _address = new Address(elements[5], elements[6], elements[8], elements[9], int.Parse(elements[4]), int.Parse(elements[7]));

            }
            /* По сути - проверка формата строки файла проходит здесь. Хоть одна ошибка - значит, в строке файла ошибка 
            и такое мы не храним, выдаем ошибку, ловим исключение в мейне и отправляем в начало программы. */
            catch(Exception ex)
            {
                throw new ArgumentException("С форматом что-то не так!"+ex.Message.ToString());
            }
        }
        /// <summary>
        /// Индексатор, возвращающий соответствующее аргументу-ключю значение.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string this[string name]
        {
            get
            {
                try
                {
                    return _dict[name].ToString();
                }
                catch (Exception e)
                {
                    throw new ArgumentException("Такой информацией о ЗАГСе не располагаем...");
                }
            }
        }
        public Address GetAddress
        {
            get => _address;
        }
        /// <summary>
        /// Метод, возвращающий подходящую к записыванию в .csv файл строку.
        /// </summary>
        /// <returns></returns>
        public string ToCSV()
        {

            StringBuilder sb = new StringBuilder();
            foreach(var el in _dict.Values)
            {
                sb.Append('"'+el.ToString()+'"'+';');
            }
            return sb.ToString();
        }
        /// <summary>
        /// Метод, возвращающий массив строк, содержащий все поля класса.
        /// </summary>
        /// <returns></returns>
        public string[] ToStringArray()
        {
            string[] res = new string[22];
            int count = 0;
            foreach (var el in _dict.Values)
            {
                res[count++] = el.ToString();
            }
            return res;
        }
        /// <summary>
        /// Метод, возвращающий заголовок, - склеенные ключи словаря, - пригодный для записи в .csv файл.
        /// </summary>
        /// <returns></returns>
        public string GetZagolovokCSV()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var el in _dict.Keys)
            {
                sb.Append('"' + el.ToString() + '"' + ';');
            }
            return sb.ToString();
        }
    }
}
