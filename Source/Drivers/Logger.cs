namespace RadioConformanceTests.Drivers;

public class Logger: ILogger{
    public void Info(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        this.log($"INFO - {message}");
    }

    public void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        this.log($"ERROR - {message}");
    }

    public void Debug(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        this.log($"DEBUG - {message}");
    }
    public void Reset(){
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void log(string message)
    {
        Console.WriteLine($"{DateTime.Now} - {message}");
    }
}