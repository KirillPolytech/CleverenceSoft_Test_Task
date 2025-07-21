using System.Text;

namespace Cleverence_Test_Task
{
    internal class Task3
    {
        public static void Executing()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));

            string inputPath = Path.Combine(projectPath, "Logs", "input.txt");
            string outputPath = Path.Combine(projectPath, "Logs", "output.txt");
            string problemsPath = Path.Combine(projectPath, "Logs", "problems.txt");

            var logLevelMap = new Dictionary<string, string>
            {
                { "INFORMATION", "INFO" },
                { "INFO", "INFO" },
                { "WARNING", "WARN" },
                { "WARN", "WARN" },
                { "ERROR", "ERROR" },
                { "DEBUG", "DEBUG" }
            };

            string[] lines = File.ReadAllLines(inputPath, Encoding.GetEncoding("windows-1251"));
            using var outputWriter = new StreamWriter(outputPath);
            using var problemsWriter = new StreamWriter(problemsPath);

            foreach (string line in lines)
            {
                try
                {
                    string date = "", time = "", level = "", method = "DEFAULT", message = "";

                    if (line.Length >= 24 && line[2] == '.' && line[5] == '.' && line[10] == ' ')
                    {
                        // Формат 1: "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'"
                        string[] parts = line.Split(' ', 4);
                        if (parts.Length < 4)
                            throw new Exception();

                        string[] dateParts = parts[0].Split('.');
                        date = $"{dateParts[2]}-{dateParts[1]}-{dateParts[0]}";
                        time = parts[1];
                        level = NormalizeLevel(parts[2], logLevelMap);
                        message = parts[3];
                    }
                    else if (line.Contains('|'))
                    {
                        // Формат 2: "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код ..."
                        string[] mainParts = line.Split('|');
                        if (mainParts.Length < 5)
                            throw new Exception();

                        
                        string[] datetimeParts = mainParts[0].Trim().Split(' ');
                        if (datetimeParts.Length != 2)
                            throw new Exception();

                        date = datetimeParts[0];
                        time = datetimeParts[1];

                        level = NormalizeLevel(mainParts[1].Trim(), logLevelMap);
                        method = mainParts[3].Trim();
                        message = mainParts[4].Trim();
                    }
                    else
                    {
                        throw new Exception();
                    }

                    outputWriter.WriteLine($"{date}\t{time}\t{level}\t{method}\t{message}");
                }
                catch
                {
                    problemsWriter.WriteLine(line);
                }
            }

            Console.WriteLine("Обработка завершена.");
        }

        static string NormalizeLevel(string level, Dictionary<string, string> map)
        {
            return map.TryGetValue(level.ToUpper(), out string normalized) ? normalized : level.ToUpper();
        }
    }
}