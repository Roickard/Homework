using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatStrings
{
    class main
    {
        static void Main(string[] args)
        {
            Console.Write("Введите предложение: ");
            string sentence = Console.ReadLine();
            List<string> subStrings = new List<string>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine(sentence);
                string subStringToPrint = "";
                foreach (string temp in subStrings)
                {
                    subStringToPrint += ", " + temp;
                }
                if(subStringToPrint.Length > 2)
                {
                    Console.WriteLine("Подстроки: " + subStringToPrint.Substring(2));
                }
                Console.Write($"Введите подстроку (для выхода введите \'exit\'): ");
                string subString = Console.ReadLine();

                if (subString == "exit")
                {
                    break;
                }
                else
                {
                    subStrings.Add(subString);
                }
            }
            Console.WriteLine(Formatting.Calculate(sentence, subStrings));
            Console.ReadKey();
        }
    }
}
