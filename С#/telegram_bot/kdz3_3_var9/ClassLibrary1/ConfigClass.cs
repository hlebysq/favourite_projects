namespace ClassLibrary1
{
    /// <summary>
    /// Нестатический класс для хранения данных за пределами Updater'а.
    /// </summary>
    public class ConfigClass
    {
        private static List<HockeyData> _hockeyList;
        private static string _pathF;
        private InterestingFields _field;
        private static DateTime _lastActivity;
        private static long _chatId = -1;
        public DateTime LastActivity
        {
            get { return _lastActivity; }
            set
            {
                _lastActivity = value;
            }
        }
        public long ChatId
        {
            get { return _chatId; }
            set
            {
                _chatId = value;
            }
        }
        public List<HockeyData> HockeyList
        {
            get { return _hockeyList; }
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("null kakoyta");
                }
                _hockeyList = value;
            }
        }
        public InterestingFields Field
        {
            get { return _field; }
            set
            {
                _field = value;
            }
        }
        public static void ActivityCheck(ref ConfigClass config)
        {
            Console.WriteLine("Updated.");
            while (true)
            {
                if (config.ChatId != -1 && (DateTime.Now - config.LastActivity).TotalSeconds > 300)
                {
                    config.ChatId = -1;
                    Console.WriteLine("Session finished because user did nothing 5 mins");
                }
                Thread.Sleep(30000);
            }
        }
    }
}
