using System.Diagnostics;
using System.Runtime.CompilerServices;
using Проект2;

/// <summary>
/// Позволяет создавать объекты связанные с записями о погоде в файле.
/// </summary>
internal class WeatherRec
{
    public DateTime Date { get; private set; } //1
    public string Location { get; private set; } //2
    public double? MinTemp { get; private set; } //3
    public double? MaxTemp { get; private set; } //4
    public double? Rainfall { get; private set; } //5
    public double? Evaporation { get; private set; } //6
    public double? Sunshine { get; private set; } //7
    public string WindGustDir { get; private set; } //8
    public double? WindGustSpeed { get; private set; } //9
    public string WindDir9am { get; private set; } //10
    public string WindDir3pm { get; private set; } //11
    public double? WindSpeed9am { get; private set; } //12
    public double? WindSpeed3pm { get; private set; } //13
    public double? Humidity9am { get; private set; } //14
    public double? Humidity3pm { get; private set; } //15
    public double? Pressure9am { get; private set; } //16
    public double? Pressure3pm { get; private set; } //17
    public double? Cloud9am { get; private set; } //18
    public double? Cloud3pm { get; private set; } //19
    public double? Temp9am { get; private set; } // 20
    public double? Temp3pm { get; private set; } // 21
    public string RainToday { get; private set; } //22
    public string RainTomorrow { get; private set; } //23
    /// <summary>
    /// Создает строку для вывода в файл формата csv.
    /// </summary>
    /// <returns>Итоговую строку.</returns>
    public string ToString_with_comma()
    {
        return Date.Year.ToString()
            + "-"
            + $"{(Date.Month > 9 ? Date.Month : "0" + Date.Month)}"
            + "-"
            + $"{(Date.Day > 9 ? Date.Day : "0" + Date.Day)}"
            + ","
            + (Location == null ? "NA" : Location.ToString())
            + ","
            + (MinTemp == null ? "NA" : MinTemp.ToString().Replace(',', '.'))
            + ","
            + (MaxTemp == null ? "NA" : MaxTemp.ToString().Replace(',', '.'))
            + ","
            + (Rainfall == null ? "NA" : Rainfall.ToString().Replace(',', '.'))
            + ","
            + (Evaporation == null ? "NA" : Evaporation.ToString().Replace(',', '.'))
            + ","
            + (Sunshine == null ? "NA" : Sunshine.ToString().Replace(',', '.'))
            + ","
            + (WindGustDir == null ? "NA" : WindGustDir.ToString().Replace(',', '.'))
            + ","
            + (WindGustSpeed == null ? "NA" : WindGustSpeed.ToString().Replace(',', '.'))
            + ","
            + (WindDir9am == null ? "NA" : WindDir9am.ToString().Replace(',', '.'))
            + ","
            + (WindDir3pm == null ? "NA" : WindDir3pm.ToString().Replace(',', '.'))
            + ","
            + (WindSpeed9am == null ? "NA" : WindSpeed9am.ToString().Replace(',', '.'))
            + ","
            + (WindSpeed3pm == null ? "NA" : WindSpeed3pm.ToString().Replace(',', '.'))
            + ","
            + (Humidity9am == null ? "NA" : Humidity9am.ToString().Replace(',', '.'))
            + ","
            + (Humidity3pm == null ? "NA" : Humidity3pm.ToString().Replace(',', '.'))
            + ","
            + (Pressure9am == null ? "NA" : Pressure9am.ToString().Replace(',', '.'))
            + ","
            + (Pressure3pm == null ? "NA" : Pressure3pm.ToString().Replace(',', '.'))
            + ","
            + (Cloud9am == null ? "NA" : Cloud9am.ToString().Replace(',', '.'))
            + ","
            + (Cloud3pm == null ? "NA" : Cloud3pm.ToString().Replace(',', '.'))
            + ","
            + (Temp9am == null ? "NA" : Temp9am.ToString().Replace(',', '.'))
            + ","
            + (Temp3pm == null ? "NA" : Temp3pm.ToString().Replace(',', '.'))
            + ","
            + (RainToday == null ? "NA" : RainToday.ToString().Replace(',', '.'))
            + ","
            + (RainTomorrow == null ? "NA" : RainTomorrow.ToString().Replace(',', '.'));
    }
    /// <summary>
    /// Создает строку для вывода в консоль.
    /// </summary>
    /// <returns>Итоговую строку.</returns>
    public override string ToString()
    {
        return Date.Year.ToString()
            + "-"
            + $"{(Date.Month > 9 ? Date.Month : "0" + Date.Month)}"
            + "-"
            + $"{(Date.Day > 9 ? Date.Day : "0" + Date.Day)}"
            + " "
            + (Location == null ? "NA" : Location.ToString())
            + " "
            + (MinTemp == null ? "NA" : MinTemp.ToString().Replace(',', '.'))
            + " "
            + (MaxTemp == null ? "NA" : MaxTemp.ToString().Replace(',', '.'))
            + " "
            + (Rainfall == null ? "NA" : Rainfall.ToString().Replace(',', '.'))
            + " "
            + (Evaporation == null ? "NA" : Evaporation.ToString().Replace(',', '.'))
            + " "
            + (Sunshine == null ? "NA" : Sunshine.ToString().Replace(',', '.'))
            + " "
            + (WindGustDir == null ? "NA" : WindGustDir.ToString().Replace(',', '.'))
            + " "
            + (WindGustSpeed == null ? "NA" : WindGustSpeed.ToString().Replace(',', '.'))
            + " "
            + (WindDir9am == null ? "NA" : WindDir9am.ToString().Replace(',', '.'))
            + " "
            + (WindDir3pm == null ? "NA" : WindDir3pm.ToString().Replace(',', '.'))
            + " "
            + (WindSpeed9am == null ? "NA" : WindSpeed9am.ToString().Replace(',', '.'))
            + " "
            + (WindSpeed3pm == null ? "NA" : WindSpeed3pm.ToString().Replace(',', '.'))
            + " "
            + (Humidity9am == null ? "NA" : Humidity9am.ToString().Replace(',', '.'))
            + " "
            + (Humidity3pm == null ? "NA" : Humidity3pm.ToString().Replace(',', '.'))
            + " "
            + (Pressure9am == null ? "NA" : Pressure9am.ToString().Replace(',', '.'))
            + " "
            + (Pressure3pm == null ? "NA" : Pressure3pm.ToString().Replace(',', '.'))
            + " "
            + (Cloud9am == null ? "NA" : Cloud9am.ToString().Replace(',', '.'))
            + " "
            + (Cloud3pm == null ? "NA" : Cloud3pm.ToString().Replace(',', '.'))
            + " "
            + (Temp9am == null ? "NA" : Temp9am.ToString().Replace(',', '.'))
            + " "
            + (Temp3pm == null ? "NA" : Temp3pm.ToString().Replace(',', '.'))
            + " "
            + (RainToday == null ? "NA" : RainToday.ToString().Replace(',', '.'))
            + " "
            + (RainTomorrow == null ? "NA" : RainTomorrow.ToString().Replace(',', '.'));
    }

    public WeatherRec(string[] str)
    {
        try
        {
            string[] date_array = str[0].Split('-');
            Date = new DateTime(
                int.Parse(date_array[0]),
                int.Parse(date_array[1]),
                int.Parse(date_array[2])
            );
        }
        catch
        {
            throw new ArgumentException();
        }
        Location = str[1];
        try
        {
            MinTemp = str[2] == "NA" ? null : double.Parse(str[2].Replace('.', ','));
            MaxTemp = str[3] == "NA" ? null : double.Parse(str[3].Replace('.', ','));
            Rainfall = str[4] == "NA" ? null : double.Parse(str[4].Replace('.', ','));
            Evaporation = str[5] == "NA" ? null : double.Parse(str[5].Replace('.', ','));
            Sunshine = str[6] == "NA" ? null : double.Parse(str[6].Replace('.', ','));
        }
        catch
        {
            throw new ArgumentException();
        }
        WindGustDir = str[7];
        try
        {
            WindGustSpeed = str[8] == "NA" ? null : double.Parse(str[8].Replace('.', ','));
        }
        catch
        {
            throw new ArgumentException();
        }
        WindDir9am = str[9];
        WindDir3pm = str[10];
        try
        {
            WindSpeed9am = str[11] == "NA" ? null : double.Parse(str[11].Replace('.', ','));
            WindSpeed3pm = str[12] == "NA" ? null : double.Parse(str[12].Replace('.', ','));
            Humidity9am = str[13] == "NA" ? null : double.Parse(str[13].Replace('.', ','));
            Humidity3pm = str[14] == "NA" ? null : double.Parse(str[14].Replace('.', ','));
            Pressure9am = str[15] == "NA" ? null : double.Parse(str[15].Replace('.', ','));
            Pressure3pm = str[16] == "NA" ? null : double.Parse(str[16].Replace('.', ','));
            Cloud9am = str[17] == "NA" ? null : double.Parse(str[17].Replace('.', ','));
            Cloud3pm = str[18] == "NA" ? null : double.Parse(str[18].Replace('.', ','));
            Temp9am = str[19] == "NA" ? null : double.Parse(str[19].Replace('.', ','));
            Temp3pm = str[20] == "NA" ? null : double.Parse(str[20].Replace('.', ','));
        }
        catch
        {
            throw new ArgumentException("Некорректные данные");
        }
        RainToday = str[21];
        RainTomorrow = str[22];
    }
}
