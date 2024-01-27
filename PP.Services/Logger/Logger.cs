namespace PP.Services.Logger
{
    public class Logger
    {
        private readonly string logFolderPath;
        private readonly string logFileName;

        public Logger()
        {
            // Set the log folder path (you might adjust this based on your project structure)
            logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logger");

            // Set the log file name
            logFileName = "log.txt";

            // Ensure the log folder exists
            Directory.CreateDirectory(logFolderPath);
        }

        public void Log(string message)
        {
            // Construct the full path to the log file
            string logFilePath = Path.Combine(logFolderPath, logFileName);

            // Log the message with timestamp
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

            // Append the log entry to the file
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
    }
}
