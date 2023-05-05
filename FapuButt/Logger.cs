using Discord;

namespace FapuButt;

public class Logger {
    public static void Log(string message) {
        LogAsync(new LogMessage(LogSeverity.Info, "Fapu", message));
    }
    
    public static void Error(string message) {
        LogAsync(new LogMessage(LogSeverity.Error, "Fapu", message));
    }
    
    public static void Error(Exception exception) {
        LogAsync(new LogMessage(LogSeverity.Error, "Fapu", exception.Message, exception));
    }
    
    public static void Error(string message, Exception exception) {
        LogAsync(new LogMessage(LogSeverity.Error, "Fapu", message, exception));
    }
    
    public static void Warn(string message) {
        LogAsync(new LogMessage(LogSeverity.Warning, "Fapu", message));
    }
    
    public static void Debug(string message) {
        LogAsync(new LogMessage(LogSeverity.Debug, "Fapu", message));
    }

    public static Task LogAsync(LogMessage message) {
        var task = new Task(() => {
            Console.WriteLine(message.ToString());
        });
        
        task.Start();
        
        return task;
    }
}