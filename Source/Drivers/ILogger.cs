namespace RadioConformanceTests.Drivers;

public interface ILogger
{
    void Info(string message);
    void Error(string message);
    void Debug(string message); 
    void Reset();
}