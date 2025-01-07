namespace CustomClasses
{
    /// <summary>
    /// Структура, в которой хранится адрес ЗАГСа.
    /// </summary>
    public struct Address
    {
        private string _district, _address, _nearestMetroStation;
        private int _postalCode;
        /// <summary>
        /// Перегрузка конструктора, задающая каждому полю значения.
        /// </summary>
        /// <param name="aa"></param>
        /// <param name="d"></param>
        /// <param name="add"></param>
        /// <param name="nms"></param>
        /// <param name="Acode"></param>
        /// <param name="Pcode"></param>
        public Address(string aa, string d, string add, string nms, int Acode, int Pcode)
        {
            AdmArea = aa;
            _district = d;
            _address = add;
            _nearestMetroStation = nms;
            AdmAreaCode = Acode;
            _postalCode = Pcode;
        }
        /// <summary>
        /// Пустой конструктор с дефолтными значениями.
        /// </summary>
        public Address() 
        {
            AdmArea = "Северо-Юго-Западо-Восточный округ";
            _district = "[ДАННЫЕ УДАЛЕНЫ] район";
            _address = "Улица Пушкина, дом Колотушкина";
            _nearestMetroStation = "Люблино(работаем)";
            AdmAreaCode = 52;
            _postalCode = 1337;
        }
        public string AdmArea { get; private set; }
        public int AdmAreaCode { get; private set; }
    }
}
