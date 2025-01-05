using System.Text;

namespace ClassLibrary;

/// <summary>
/// Класс для работы с массивом слов предложения.
/// </summary>
public class MyStrings
{
    private string[] _sentences; // Объявляем приватное поле _sentences.
    /// <summary>
    /// Перегрузка конструктора с двумя аргументами.
    /// </summary>
    public MyStrings(string str, char ch)
    {
        try
        {
            _sentences = str.Split(ch);
        }
        // Если что-то пошло не так - выбрасываем ArgumentException.
        catch (Exception ex) {
            throw new ArgumentException("Со строкой/разделителем что-то не так.");
        }
    }
    /// <summary>
    /// Метод для взаимодействия с _sentences - сет приватный, можно только посмотреть.
    /// </summary>
    public string[] Sentences
    {
        get => _sentences;
        private set => _sentences = value;
    }
    /// <summary>
    /// Перегрузка конструктора без аргументов.
    /// </summary>
    public MyStrings() 
    {
        /* Если предложения для работы нет - создадим :)
        Чтобы не допускать ситуации, когда объект класса есть, а _sentences не инициализирован. */
        Sentences = new string[5] {"Never","gonna","give","your","up" };
    }

    /// <summary>
    /// Свойство, возвращающее массив аббревиатур. 
    /// </summary>
    /* Правила формирования аббревиатуры -
    1) Аббревиатура слова идет до первой гласной
    2) Аббревиатура слова начинается с заглавной буквы */
    public string[] ABBR
    {
        get
        {
            try
            {
                string[] ans = new string[_sentences.Length]; // Очевидно, количество аббревиатур = количеству слов.
                for (int i = 0; i < _sentences.Length; i++)
                {
                    /*Можно было использовать просто строку, но так как мы много будем прибавлять к строке элементы -
                    в плане памяти и скорости работы эффективнее использовать StringBuilder.*/
                    StringBuilder sb = new StringBuilder();
                    bool flag = false; /* Флаг, чтобы, если первая буква в слове строчная, сделать ее заглавной.
                    При помощи флага делим на 4 случая. Можно было поделить по другому, но тк работаем с локальной
                    неизменяемой переменной ch это было бы не очень удобно. */
                    foreach (char ch in _sentences[i])
                    {
                        if (HelpMethods.IsVowel(ch))
                        {
                            if (!flag)
                            {
                                sb.Append(char.ToUpper(ch));
                                flag = true;
                            }
                            else
                            {
                                sb.Append(ch);
                            }

                            break;
                        }

                        /* Важно, что тут не просто else - тк отрицание к IsVowel это в том числе любой другой символ,
                        а в аббревиатуре только буквы. */
                        if (HelpMethods.IsConsonant(ch))
                        {
                            if (!flag)
                            {
                                sb.Append(char.ToUpper(ch));
                                flag = true;
                            }
                            else
                            {
                                sb.Append(ch);
                            }
                        }
                    }

                    string abbr = sb.ToString();
                    ans[i] = abbr; // Добавляем в массив аббревиатуру, преобразованную в строку.
                }

                return ans;
            }
            // Если что-то пошло не так - выбрасываем ArgumentException.
            catch (Exception ex)
            {
                throw new ArgumentException("В _sentences некорректные для формирования аббревиатуры данные.");
            }
        }
    }
}