public class SimpleRequestHandler : IRequestHandler
{
	public string HandleRequest(string? message, string?[] arguments)
	{
		var result = "Нет результата";
		message = message?.ToLower();
		if (message == null || message.Length == 0)
		{
			throw new ArgumentNullException(nameof(message));
		}
		if (message.Contains("sum") || message.Contains("сум"))
		{
			Thread.Sleep(5_000);
			var sum = 0;
			foreach (var arg in arguments)
			{
				if (!int.TryParse(arg, out var num))
					throw new ArgumentException("arguments must be integers");
				sum += num;
			}
			result = sum.ToString();
		}
		if (message.Contains("упади") || message.Contains("сломайся"))
			throw new Exception("crashed by request");
		Thread.Sleep(5_000); 
		
		return $"Результат: {result} с идентификатором {Guid.NewGuid()}";
	}
}