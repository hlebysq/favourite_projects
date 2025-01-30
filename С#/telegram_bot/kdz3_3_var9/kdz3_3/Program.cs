using ClassLibrary1;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Serilog;



class program
{

    private static ITelegramBotClient botClient;
    private static ReceiverOptions receiverOptions;
    private static ConfigClass conf = new ConfigClass();
    private static bool ChoiseFactor = false;

    static async Task Main()
    {
        // Main - реализован в виде static asynс task, потому что иначе код бы просто завершался.
        botClient = new TelegramBotClient("6565808278:AAFJjWWc4Pa63LOpdPmSkEVMM9jz9t62A7E");
        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File(@$"../../../../var/log.txt",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true)
        .CreateLogger();
        receiverOptions = new ReceiverOptions 
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message, 
                UpdateType.CallbackQuery 
            },
            ThrowPendingUpdates = true,
        };

        using var cts = new CancellationTokenSource();

        botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);

        var me = await botClient.GetMeAsync(); 
        Log.Information($"{me.FirstName} turns on!");
        await Task.Delay(-1);
    }

    /// <summary>
    /// Апдейтер - таск для принятия сообщений.
    /// </summary>
    /// <param name="botClient"></param>
    /// <param name="update"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        var message = update.Message;
                        if(message is null)
                        {
                            return;
                        }
                        var user = message.From;
                        var chat = message.Chat;
                        if(user is null || message is null)
                        {
                            return;
                        }
                        // Если другой пользователь отправил нам сообщение, отправляем его за тридевять земель
                        // пока текущий пользователь не завершит работу.
                        // Можно было сделать базу данных с пользователями и тьоже ее хранить...
                        // ...но мне не платят за это кдз поэтому одновременно работает только 1 пользователь)))
                        if (conf.ChatId != -1 && conf.ChatId != message.Chat.Id)
                        {
                            Log.Information($"{user.FirstName} в чате {message.Chat.Id} только что получил реджект:)");
                            await botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                $"Бот занят с другим пользователем, подождите пожалуйста");
                            return;
                        }
                        conf.ChatId = message.Chat.Id;
                        conf.LastActivity = DateTime.Now;
                        Thread thread = new Thread(() => ConfigClass.ActivityCheck(ref conf));
                        thread.Start();
                        switch (message.Type)
                        {
                            case MessageType.Text:
                            {
                                await TextCase(user, message, chat);
                                return;
                            }
                            case MessageType.Document:
                            {
                                await DocumentCase(user, message, chat, cancellationToken);
                                return;
                            }
                            default:
                                {
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        "Фатальная ошибка! Ты делаешь что-то не так.");
                                    return;
                                }
                        }
                    }

                case UpdateType.CallbackQuery:
                    {
                        var callbackQuery = update.CallbackQuery;
                        if( callbackQuery is null || callbackQuery.Message is null)
                        {
                            return;
                        }
                        var user = callbackQuery.From;
                        var chat = callbackQuery.Message.Chat;
                        if(conf.ChatId != -1 && conf.ChatId != callbackQuery.Message.Chat.Id)
                        {
                            Log.Information($"{user.FirstName} в чате {callbackQuery.Message.Chat.Id} только что получил реджект:)");
                            await botClient.SendTextMessageAsync(
                                callbackQuery.Message.Chat.Id,
                                $"Бот занят с другим пользователем, подождите пожалуйста");
                            return;
                        }
                        conf.ChatId = callbackQuery.Message.Chat.Id;
                        conf.LastActivity = DateTime.Now;
                        Thread thread = new Thread(() => ConfigClass.ActivityCheck(ref conf));
                        thread.Start();
                        Log.Information($"{user.FirstName} {user.Id} press the button: {callbackQuery.Data}");
                        switch (callbackQuery.Data)
                        {
                            case "button1_1":
                                {

                                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        $"Пришлите CSV-файл");
                                    return;
                                }

                            case "button1_2":
                                {
                                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        $"Пришлите JSON-файл");
                                    return;
                                }
                            case "button2_1":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                if (conf.HockeyList is null)
                                {
                                    await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    $"Вы уже завершили сеанс и не начали новый! Введите /start");
                                    return;
                                }
                                ChoiseFactor = true;
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    "Отправьте значение поля для выборки");
                                conf.Field = InterestingFields.ObjectName;
                                break;
                            case "button2_2":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                if (conf.HockeyList is null)
                                {
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        $"Вы уже завершили сеанс и не начали новый! Введите /start");
                                    return;
                                }

                                ChoiseFactor = true;
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    "Отправьте значение поля для выборки");
                                conf.Field = InterestingFields.NameWinter;
                                break;
                            case "button2_3":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                if (conf.HockeyList is null)
                                {
                                    await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    $"Вы уже завершили сеанс и не начали новый! Введите /start");
                                    return;
                                }

                                ChoiseFactor = true;
                                await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        "Отправьте значение полей для выборки через точку с запятой");
                                conf.Field = InterestingFields.DistrictAndHasDressingRoom;
                                break;
                            case "button2_4":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                if (conf.HockeyList is null)
                                {
                                    await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    $"Вы уже завершили сеанс и не начали новый! Введите /start");
                                    return;
                                }
                                await Sort(chat.Id, InterestingFields.Lightning);
                                break;
                            case "button2_5":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                if (conf.HockeyList is null)
                                {
                                    await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    $"Вы уже завершили сеанс и не начали новый! Введите /start");
                                    return;
                                }
                                await Sort(chat.Id, InterestingFields.Seats);
                                break;
                            case "button2_6":
                                await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                if (conf.HockeyList is null)
                                {
                                    await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    $"Вы уже завершили сеанс и не начали новый! Введите /start");
                                    return;
                                }
                                Log.Information($"{user.FirstName} {user.Id} finished a session");
                                conf.ChatId = -1;
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    $"Вы успешно завершили работу с файлом. Чтобы начать" +
                                    $" работу с новым файлом введите /start");
                                Directory.Delete(@$"../../../../data/user{chat.Id}", true); // Удаление файла.
                                break;
                        }
                        return;
                    }
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
        }
    }

    private static async Task TextCase(User user, Message message, Chat chat)
    {
        Log.Information($"{user.FirstName} {user.Id} sent a message: {message.Text}");
        if (ChoiseFactor)
        {
            if(message.Text is null) { return; }
            await Choise(chat.Id, conf.Field, message.Text);
            return;
        }

        switch (message.Text)
        {
            case "/start":
                conf.LastActivity = DateTime.Now;
                conf.ChatId = chat.Id;
                var inlineKeyboard = new InlineKeyboardMarkup(
                    new List<InlineKeyboardButton[]>
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("CSV", "button1_1")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("JSON", "button1_2")
                        }
                    });
                await botClient.SendTextMessageAsync(
                    chat.Id,
                    "Добро пожаловать! Выберите один из двух вариантов:",
                    replyMarkup: inlineKeyboard);
                Log.Information("");
                conf.LastActivity = DateTime.Now;
                return;
            default:
                await botClient.SendTextMessageAsync(
                    chat.Id,
                    "Чтобы начать диалог отправьте /start");
                return;
        }
    }

    private static async Task DocumentCase(User user, Message message, Chat chat, CancellationToken cancellationToken)
    {
        if(message.Document is null)
        {
            return;
        }
        if(conf.ChatId == -1)
        {
            await botClient.SendTextMessageAsync(
                chat.Id,
                "Чтобы начать диалог отправьте /start");
            return;
        }
        var fileId = message.Document.FileId;
        var fileSize = message.Document.FileSize;
        if (fileSize > 10486760) // Если размер >10мб - отклоняем и не скачиваем
        {
            await botClient.SendTextMessageAsync(
                chat.Id,
                "Слишком большой размер файла - скиньте другой файл.");
            Log.Information($"{user.FirstName} {user.Id} sent big document with size = {fileSize}b");
            return;
        }
        var fileInfo = await botClient.GetFileAsync(fileId);
        var filePath = fileInfo.FilePath;
        string fileExtension = Path.GetExtension(filePath);
        Log.Information($"{user.FirstName} {user.Id} sent a document with extention: {fileExtension}");
        if (fileExtension == ".json" || fileExtension == ".csv")
        {
            Log.Debug($"File with extention {fileExtension} downloaded from {user} in chat {chat.Id} ");
            string folderPath = @$"../../../../data/user{chat.Id}";

            // Создание папки, если она не существует
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string newFilePath = folderPath+@$"\downloaded{fileExtension}";
            using Stream stream = System.IO.File.Create(newFilePath);
            await botClient.DownloadFileAsync(
                filePath: filePath,
                destination: stream,
                cancellationToken: cancellationToken);
            List<HockeyData> records = new List<HockeyData>();
            try
            {
                if (fileExtension == ".csv")
                {
                    var csvProcessing = new CSVprocessing();
                    records = csvProcessing.CsvRead(stream);
                }
                else
                {
                    var json = new JSONprocessing();
                    records = json.JsonRead(stream);
                }
                conf.HockeyList = records;
                await botClient.SendTextMessageAsync(
                    chat.Id,
                    $"Файл успешно считан! В нем информация о " +
                    $"{conf.HockeyList.Count} объектах.");
                await CallMenu();
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(
                    chat.Id,
                    "С вашим файлом что-то не так! Предположительно, по этой " +
                    $"причине: {ex.Message}. \nОтправьте другой файл.");
            }
        }
        else
        {
            await botClient.SendTextMessageAsync(
                chat.Id,
                "Что ты скинул? Это не json и не csv!");
        }
        return;
    }
    /// <summary>
    /// ЕррорХендлер - таск для обработки ошибок бота на уровне АПИ.
    /// </summary>
    /// <param name="botClient"></param>
    /// <param name="error"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    { 
        var ErrorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Log.Error(ErrorMessage);
        return Task.CompletedTask;
    }
    private static Task CallMenu()
    {
        var inlineKeyboard = new InlineKeyboardMarkup(
        new List<InlineKeyboardButton[]>()
                {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Произвести выборку по " +
                    "ObjectName", "button2_1"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Произвести выборку по " +
                    "NameWinter", "button2_2"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Произвести выборку по " +
                    "District и HasDressingRoom", "button2_3"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Отсортировать по Lighting " +
                    " по алфавиту в прямом порядке", "button2_4"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Отсортировать по Seats " +
                    "в порядке возрастания", "button2_5"),
                },
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("Завершить работу с этим файлом", "button2_6"),
                },
                });
        botClient.SendTextMessageAsync(
            conf.ChatId,
            "Выберите один из вариантов:",
            replyMarkup: inlineKeyboard);
        return Task.CompletedTask;
    }
    /// <summary>
    /// Таск для реализации выборки.
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private static Task Choise(long chatId, InterestingFields field, string value)
    {
        List<HockeyData> choisedList;
        try
        {
            switch (field)
            {
                case (InterestingFields.ObjectName):
                    List<string> uniqueValues = DataProcessing.AllValues(conf.HockeyList, field);
                    if (uniqueValues.Count == 0)
                    {
                        botClient.SendTextMessageAsync(
                            chatId,
                            $"В этом поле нет значений! Выберите другой вариант.");
                        ChoiseFactor = false;
                        return Task.CompletedTask;
                    }
                    choisedList = conf.HockeyList.Where(obj => obj.ObjectName == value).ToList();
                    break;
                case (InterestingFields.NameWinter):
                    uniqueValues = DataProcessing.AllValues(conf.HockeyList, field);
                    if (uniqueValues.Count == 0)
                    {
                        botClient.SendTextMessageAsync(
                            chatId,
                            $"В этом поле нет значений! Выберите другой вариант.");
                        return Task.CompletedTask;
                    }
                    choisedList = conf.HockeyList.Where(obj => obj.NameWinter == value).ToList();
                    break;
                case (InterestingFields.DistrictAndHasDressingRoom):
                    List<string> uniqueValues1 = DataProcessing.AllValues(conf.HockeyList, InterestingFields.District);
                    List<string> uniqueValues2 = DataProcessing.AllValues(conf.HockeyList, InterestingFields.HasDressingRoom);
                    if (uniqueValues1.Count == 0 || uniqueValues2.Count == 0)
                    {
                        botClient.SendTextMessageAsync(
                            chatId,
                            $"В одном из полей нет значений! Выберите другой вариант.");
                        return Task.CompletedTask;
                    }
                    try
                    {
                        var valueArr = value.Split(';');
                        string value1 = valueArr[0], value2 = valueArr[1];
                        choisedList = conf.HockeyList.Where(obj => obj.District == value1 && obj.HasDressingRoom == value2).ToList();
                    }
                    catch
                    {
                        botClient.SendTextMessageAsync(
                            chatId,
                            $"С введенными значениями что-то не так! Попробуйте ввести их еще раз.");
                        return Task.CompletedTask;
                    }
                    break;
                default:
                    choisedList = new List<HockeyData>();
                    break;
            }
            var writer = new CSVprocessing();
            var jWriter = new JSONprocessing();
            botClient.SendDocumentAsync(
                chatId,
                new InputFileStream(writer.CsvWrite(choisedList), fileName: "ChoisedData.csv")
                );
            botClient.SendDocumentAsync(
                chatId,
                new InputFileStream(jWriter.JsonWrite(choisedList), fileName: "ChoisedData.json")
                );
            CallMenu();
        }
        catch(Exception ex)
        {
            botClient.SendTextMessageAsync(
                chatId,
                $"Что-то пошло не так! Предположительная ошибка: "+ex.Message);

        }
        ChoiseFactor = false;
        return Task.CompletedTask;
    }
    /// <summary>
    /// Таск для реализации сортировки.
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    private static Task Sort(long chatId, InterestingFields field)
    {
        var writer = new CSVprocessing();
        var jWriter = new JSONprocessing();
        List<HockeyData> sortedList;
        switch (field)
        {
            case InterestingFields.Seats:
                sortedList = conf.HockeyList.ToArray().ToList();
                sortedList.Sort(HockeyData.CompareBySeats);
                break;
            case InterestingFields.Lightning:
                sortedList = conf.HockeyList.ToArray().ToList();
                sortedList.Sort(HockeyData.CompareByLighting);
                break;
            default:
                sortedList = new List<HockeyData>();
                break;
        }
        botClient.SendDocumentAsync(
                chatId,
                new InputFileStream(writer.CsvWrite(sortedList), fileName: "SortedData.csv")
                );
        botClient.SendDocumentAsync(
                chatId,
                new InputFileStream(jWriter.JsonWrite(sortedList), fileName: "SortedData.json")
                );
        CallMenu();
        return Task.CompletedTask;
    }
}
