# Немного контекста...
Это мое четвертое большое домашнее задание на курсе по C#. Оно похоже на второе - тоже обработка  файлов, тоже база данных в .csv, только в этот раз с исполнением всех принципов ООП, индексаторов для доступа к элементам класса, enum вместо "магических чисел", интерфейсов(в частности IComparable). В сумме с комментариями вышло около 1000 строк, в общем - большая работа

# Сама задача
## Общее задание

Разрабатываемая в рамках контрольного домашнего задания программа предназначена для
работы с данными csv-файла и должна быть реализована с использованием объектноориентированной парадигмы. Соблюдение принципов единственной ответственности,
принципа подстановки Лисков и инверсии зависимостей обязательно.
В одном решении разместить проект библиотеки классов и проект консольного
приложения. Дополнительные требования к библиотеке и приложению см. в
индивидуальном варианте.

## Требования к библиотеке классов

1. Обязательно присутствуют типы данных, заданные индивидуальным вариантом
(таблица 2).

2. Реализации классов не должны нарушать инкапсуляцию данных и принцип
единственной ответственности (Single Responsibility Principle).

3. Иерархии не должны нарушать принципа подстановки Лисков (Liskov Substitution
Principle) и проектируются, исходя из соблюдения принципа инверсии зависимостей
(Dependency Inversion Principle).

- Архитектурные принципы (https://learn.microsoft.com/ruru/dotnet/architecture/modern-web-apps-azure/architectural-principles)

4. Реализации классов должны содержать регламентированный доступ к данным.

5. Классы библиотеки должны быть доступны за пределами сборки.

6. Каждый нестатический класс обязательно должен содержать, в числе прочих,
конструктор без параметров или эквивалентные описания, допускающие его прямой
вызов или неявный вызов.

7. Запрещено изменять набор данных (удалять / дополнять), хранящихся в классах или
не использовать указанные в задании открытые варианты поведения.

8. Допускается расширение открытого поведения или добавление закрытых
функциональных членов класса.

9. Допускается использование собственных (самописных) иерархий классов в
дополнение к предложенным в индивидуальном варианте, но требования по ООП
принципам к ним сохраняется.

10. Допускается использование абстрактных типов данных, таких как List, ArrayList, Set,
Stack, их обобщённых реализаций и проч., но не в качестве замены определённых
вариантом структур данных.

11. Поскольку в описаниях классов присутствует «простор» для принятия решений, то
каждое такое решение должно быть описано в комментариях к коду программы.
Например, если выбран тип исключения, то должно быть письменно обоснованно,
почему вы считаете его наиболее подходящим в рамках данной задачи.

## Требования к консольному приложению

1. Консольное приложение должно использовать типы данных, заданные
индивидуальным вариантом (таблица 2) и позволять (в консольном меню):

- Открыть *.csv файл с исходными данными. Структура файла определена
файлом индивидуального варианта (таблица 1). В случае ошибок открывания
файла или некорректных данных программа должна выводить пользователю
сообщение. Пути к файлам получать от пользователя.

- Предоставить для просмотра N первых (top) или последних записей (bottom)
из файла (N > 1 выбирается пользователем и не превышает количества
записей в csv файле). Выбор первых или последних записей организовать с
использованием перечисления (enum, https://learn.microsoft.com/ruru/dotnet/csharp/language-reference/builtin-types/enum).

- Выполнить сортировку или фильтрацию по полям, указанным в
индивидуальном варианте (таблица 3). Значения полей для фильтрации
вводятся пользователем с клавиатуры.

- Сохранять результаты сортировок и фильтраций в файл формата csv.
Сохранение производится обязательно после выполнения сортировки /
фильтрации. В случае ошибок открывания / сохранения файла или
некорректных данных, программа должна выводить сообщение. Структура
файла для сохранения данных совпадает со структурой файла, заданного
индивидуальным вариантом.

- Режимы сохранения в файл. Режим позволяет по-разному сохранять данные
в файл. Выбор режима остаётся за пользователем, потребуется организовать
с ним диалог. Пути к файлам получать от пользователя.

> создание нового файла.
>
> замена содержимого уже существующего файла.
>
> добавление сохраняемых данных к содержимому существующего
файла.

2. Предусмотреть проверку корректности для каждого ввода данных, обработку
исключений, в т.ч. порождаемых классами библиотеки, организацию повторения
решения.

3. Допускается использование абстрактных типов данных, таких как List, ArrayList, Set,
Stack, их обобщённых реализаций и проч., но не в качестве замены определённых
вариантом структур данных.

4. Все данные приложения читают из csv файлов, результат работы и выводится на
экран, и сохраняется в csv файле. Программа должна уметь читать, созданные ей при
сохранении данных файлы.

5. Программа обязательно должна читать все созданные ей самой файлы без ошибок.

## Общие требования к работе

1. Цикл повторения решения и проверки корректности получаемых данных
обязательны.

2. Соблюдение определённых программой учебной дисциплины требований к
программной реализации работ – обязательно.

3. Соблюдение соглашений о качестве кода – обязательно
(https://learn.microsoft.com/ru-ru/dotnet/csharp/fundamentals/coding-style/codingconventions).

4. Весь программный код должен быть написан на языке программирования C# с
учётом использования .net 6.0;

5. исходный код должен содержать комментарии, объясняющие неочевидные
фрагменты и решения, резюме кода, описание целей кода (см. материалы лекции 1,
модуль 1);

6. при перемещении папки проекта библиотеки (копировании / переносе на другое
устройство) файлы должны открываться программой также успешно, как и на
компьютере создателя, т.е. по относительному пути;

7. текстовые данные, включая данные на русском языке, успешно декодируются при
представлении пользователю и человекочитаемы;

8. программа не допускает пользователя до решения задач, пока с клавиатуры не будут
введены корректные данные;

9. консольное приложение обрабатывает исключительные ситуации, связанные (1) со
вводом и преобразованием / приведением данных как с клавиатуры, так и из файлов;
(2) с созданием, инициализацией, обращением к элементам массивов и строк; (3)
вызовом методов библиотеки.

10. представленная к проверке библиотека классов должна решать все поставленные
задачи, успешно компилироваться.

11. Запрещено использование сторонних библиотек, файлов исходного кода, NuGet пакетов.
