
const string ExitCommand = "/exit";
const string ArgumentsEndCommand = "/end";

Console.WriteLine("Приложение запущено.");
Console.WriteLine($"Введите текст запроса. Для выходи введите {ExitCommand}");

var command = Console.ReadLine();
while (command != ExitCommand)
{ 
	IRequestHandler requestHandler = new DummyRequestHandler();
	var arguments = new List<string?>();

	Console.WriteLine(
		$"Введите аргументы сообщения. Если аргументы не нужны введите {ArgumentsEndCommand}");
	var argumentCommand = Console.ReadLine();

	while (argumentCommand != ArgumentsEndCommand)
	{
		arguments.Add(argumentCommand);

		Console.WriteLine(
			$"Введите следующий аргумент. Для окончания добавления введите {ArgumentsEndCommand}");
		argumentCommand = Console.ReadLine();
	}

	var id = Guid.NewGuid();

	ThreadPool.QueueUserWorkItem(_ =>
	{
		var result = "";
		var message = command;
		try
		{
			result = requestHandler.HandleRequest(message, arguments.ToArray());
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Сообщение \"{message}\" с идентификатором {id} получило ответ: {result}");
			Console.ForegroundColor = ConsoleColor.White;
		}
		catch (Exception ex)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"Сообщение \"{message}\" с идентификатором {id} упало с ошибкой: {ex.Message}");
			Console.ForegroundColor = ConsoleColor.White;
		}
	});

	Console.ForegroundColor = ConsoleColor.Cyan;
	Console.WriteLine($"Было отправлено сообщение {command} присвоен идентификатор {id}");
	Console.ForegroundColor = ConsoleColor.White;

	Console.WriteLine($"Введите текст запроса. Для выхода введите {ExitCommand}");
	command = Console.ReadLine();
}

Console.WriteLine("Приложение завершает работу.");
