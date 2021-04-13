using System.Collections.Generic;
using System.Linq;

namespace StringProcessing
{
	class ConvertNums
	{
        enum Level : byte
        {
            Ones,
            TensBelow20,
            Tens,
            Hundreds,
            AboveHundreds
        };
        static List<List<string>> words = new List<List<string>>()
        {
            new List<string>() { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" },
            new List<string>() { "десять", "одинадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" },
            new List<string>() { "", "десять", "двадцать", "тридцать", "сорок", "пятдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" },
            new List<string>() { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" },
            new List<string>() { "", "тысяч", "миллион", "миллиард", "триллион", "квадриллион", "квинтиллион" }
        };
        public static string Calculate(string value)
        {
            List<string> numbers = new List<string>();
            string digits = "";
            foreach(char letter in value)
			{
                if(letter <= '9' && letter >= '0')
				{
                    digits += letter;
                }
                else
				{
                    if(digits != "")
					{
                        numbers.Add(digits);
					}

                    digits = "";
                }
			}
            if (digits != "")
            {
                numbers.Add(digits);
            }

            string tempNumber = "";
            foreach(string number in numbers)
			{
                foreach (string convertedNumber in Solve(number))
                {
                    tempNumber += convertedNumber + " ";
                }

                tempNumber = tempNumber.Trim();

                value = value.Replace(number, tempNumber);
                tempNumber = "";
            }

            return value;
        }
        private static IEnumerable<string> Solve(string value)
        {
            IEnumerable<string> numbers = SplitIntoCategories(value);

            int currentNumber = 0;

            if(numbers.Count() == 1 && numbers.First() == "000")
			{
                yield return "нуль";
			}
            else
            {
                foreach (string number in numbers)
                {
                    int countDown = numbers.Count() - (++currentNumber);
                    yield return
                        string.Format
                        (
                            "{0} {1} {2} {3}",
                            GetHundreds(number),
                            GetTens(number),
                            GetOnes(number, countDown),
                            GetAboveHundreds(number, countDown)
                        );
                }
            }
        }
        private static IEnumerable<string> SplitIntoCategories(string value)
        {
            value = value.PadLeft(value.Length + 3 - value.Length % 3, '0');
            return Enumerable.Range(0, value.Length / 3).Select(startIndex => value.Substring(startIndex * 3, 3));
        }
        private static string GetHundreds(string number)
        {
            number = number.Substring(0, 1);
            return (number == "0") ? ("") : (GetWord(Level.Hundreds, number[0]));
        }
        private static string GetTens(string number)
        {
            if (number[1] != '0')
            {
                if (number[1] == '1')
				{
                    return GetWord(Level.TensBelow20, number[2]);
                }
                else
				{
                    return GetWord(Level.Tens, number[1]);
                }
            }
            
            return "";
        }
        private static string GetOnes(string number, int countDown)
        {
            if (number[1] != '1')
            {
                if (number[2] != '0')
                {
                    if (number[2] == '1' && countDown == 1)
                    {
                        return "одна";
                    }
                    else if (number[2] == '2' && countDown == 1)
                    {
                        return "две";
                    }

                    return GetWord(Level.Ones, number[2]);
                }
            }

            return "";
        }
        private static string GetAboveHundreds(string number, int countDown)
        {
            if (countDown == 0 || number == "000") return "";

            string level = GetWord(Level.AboveHundreds, countDown.ToString()[0]);

            if (new[] { '1', '2', '3', '4' }.Contains(number[2]) && number[1] != '1')
            {
                if (number[2] == '1')
                {
                    return level + (countDown == 1 ? ("а") : (""));
                }

                return level + ((countDown == 1) ? ("и") : ("а"));
            }

            return level + (countDown == 1 ? "" : "ов");              
        }
        private static string GetWord(Level level, char digit)
        {
            return words[(int)level][GetDigit(digit)];
        }
        private static int GetDigit(char digit)
        {
            return int.Parse(digit.ToString());
        }
    }
}
