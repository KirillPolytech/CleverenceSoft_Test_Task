using System.Text;

namespace Cleverence_Test_Task
{
    internal class Task1
    {
        public static string Execute(string task)
        {
            if (task.Length == 0)
                return string.Empty;
            
            char currentSymbol = task[0];
            byte symbolCount = 1;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < task.Length; i++)
            {
                if (i + 1 < task.Length && currentSymbol != task[i + 1])
                {
                    string symbolLabel = symbolCount == 1 ? currentSymbol.ToString()
                        : currentSymbol.ToString() + symbolCount;

                    result.Append(symbolLabel);
                    symbolCount = 1;
                    currentSymbol = task[i + 1];
                    continue;
                }

                if (i + 1 == task.Length && symbolCount == 1)
                {
                    result.Append(currentSymbol);
                }

                symbolCount++;
            }

            return result.ToString();
        }
    }
}
