using System;
using System.Collections.Generic;

namespace MoneyParser
{
    
    class MoneyParser
    {
        static List<string> Res = new List<string>();
        static int number = 0;
        static void Main()
        {
            number = Parser.ParseInt("Enter number: ");

            Print();

			Console.WriteLine("Press any key to close application...");
            Console.ReadKey();
        }
        static IEnumerable<string> Calculate()
        {
            string seven = "";
            for (int first = 0; first <= number / 7; ++first)
            {
                string five = "";
                for (int second = 0; second <= number / 5; ++second)
                {
                    string three = "";
                    for (int third = 0; third <= number / 3; ++third)
                    {
                        if ((first * 7 + second * 5 + third * 3) == number)
                        {
                            yield return (seven + five + three).ToString();
                        }
                        else if ((first * 7 + second * 5 + third * 3) > number)
                        {
                            break;
                        }
                        three += "+ 3 ";
                    }
                    five += "+ 5 ";
                }
                seven += "+ 7 ";
            }
        }
        static void Print()
		{
            foreach (string res in Calculate())
            {
                if (res.Length > 3)
                {
                    Console.WriteLine($"{number} = {res.Substring(2)}");
                }
            }
        }
    }
}