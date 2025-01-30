namespace ClassLibrary1
{
    /// <summary>
    /// Статический класс для работы с данными.
    /// </summary>
    public static class DataProcessing
    {
        
        public static List<string> AllValues(List<HockeyData> data, InterestingFields field) 
        { 
            if(data is null)
            {
                throw new ArgumentException("Данных нет!");
            }
            List<string> allValues = new List<string>();
            switch (field)
            {
                case InterestingFields.ObjectName:
                    foreach(var el in data)
                    {
                        allValues.Add(el.ObjectName);
                    }
                    break;
                case InterestingFields.NameWinter:
                    foreach (var el in data)
                    {
                        allValues.Add(el.NameWinter);
                    }
                    break;
                case InterestingFields.District:
                    foreach (var el in data)
                    {
                        allValues.Add(el.District);
                    }
                    break;
                case InterestingFields.HasDressingRoom:
                    foreach (var el in data)
                    {
                        allValues.Add(el.ObjectName);
                    }
                    break;
                default:
                    throw new ArgumentException("По таким полям не сортируем!");
            }
            List<string> uniqueValues = allValues.Distinct().ToList();
            return uniqueValues;
        }
    }
}
