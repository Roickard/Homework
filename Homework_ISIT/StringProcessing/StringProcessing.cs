using System;
using System.Collections.Generic;
using System.Text;

namespace StringProcessing
{
	class StringProcessing
	{
        static void Main(string[] args)
        {
            int exercise;
            do
            {
                exercise = Parser.ParseInt("Выберите задание.\n" +
                                               "1. Вывод числа прописью.\n" +
                                               "2. Преобразование латиницы в кириллицу.\n" +
                                               "3. Замена подстрок в строке на символ \'*\'\n" +
                                               "Любое другое число - выход.\n\n" +
                                               "Ввод: ");
                Console.Clear();
                switch (exercise)
                {
                    case 1:
                        Console.Write("Введите строку: ");
                        Console.WriteLine("Результат: " + ConvertNums.Calculate(Console.ReadLine()));

                        Console.WriteLine("\nНажмите любую клавишу, чтобы выйти.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        Console.Write("Введите строку: ");
                        Console.WriteLine("Результат: " + TranslateString.Calculate(Console.ReadLine()));

                        Console.WriteLine("\nНажмите любую клавишу, чтобы выйти.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Write("Введите предложение: ");
                        string sentence = Console.ReadLine();
                        List<string> subStrings = new List<string>();

                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Введите подстроку (для выхода введите \'exit\'): ");
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

                        Console.Clear();
                        Console.WriteLine("Резуьтат: " + ReplaceSubstrings.Calculate(sentence, subStrings));

                        Console.WriteLine("\nНажмите любую клавишу, чтобы выйти.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }
            while (exercise < 4 && exercise > 0);       
        }
    }
}
