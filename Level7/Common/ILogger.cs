namespace WebAPIScratch.Common;

public interface ILogger
{
    void Info(string text);
    void Error(string error);
    void Warn(string warning);
}
