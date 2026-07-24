namespace WebAPIScratch.Common;

public class Logger : ILogger
{
    public void Error(string error)
    {
        Console.WriteLine($"Error: {error}");
    }

    public void Info(string text)
    {
        Console.WriteLine($"Info: {text}");
    }

    public void Warn(string warning)
    {
        Console.WriteLine($"Warning: {warning}");
    }
}
