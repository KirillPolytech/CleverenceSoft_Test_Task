using Cleverence_Test_Task;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string test1 = "aaabbcccdde";
            string test2 = "abbcccdde";
            string test3 = "";
            Console.WriteLine(Task1.Execute(test1));
            Console.WriteLine(Task1.Execute(test2));
            Console.WriteLine(Task1.Execute(test3));

            Task3.Executing();
        }
    }
}