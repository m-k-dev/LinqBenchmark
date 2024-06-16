# region info

// Simple stopwatch class
// 2024-06-09 [+] initial version

# endregion

using System.Diagnostics;

namespace CTimers;

public class CTimer : IDisposable
{
	private readonly string _message;
	private readonly Stopwatch _stopwatch;

	public CTimer(string message = "")
	{
		_message = message;
		_stopwatch = Stopwatch.StartNew();
	}

/*
        ~CTimer()
        {
            Console.WriteLine("=== Timer dtor ===");
        }
*/
	public void Dispose()
	{
		_stopwatch.Stop();
		var resultTime = _stopwatch.Elapsed;

		string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}",
			resultTime.Hours,
			resultTime.Minutes,
			resultTime.Seconds,
			resultTime.Milliseconds);

		string message = _message.Trim().Length == 0 ? _message : (_message + "                     ").Substring(0, 30);
		Console.WriteLine($"{message}{elapsedTime}");
	}
}