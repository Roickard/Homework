using System;

namespace StringProcessing
{
    class Parser
    {
        static private string message;
        public static string ParseNumber(string message = null)
		{
            Parser.message = message;
            string value;

            bool failedParse = false;
            do
            {
                value = GetUnparsedValue();

                foreach(char number in value)
				{
                    failedParse = !(byte.TryParse(number.ToString(), out _));
                    if (failedParse == true)
					{                        
                        break;
					}
				}
            }
            while (failedParse == true);

            return value;
        }
        public static int ParseInt(string message = null)
        {
            Parser.message = message;
            int value;
            bool failedParse;

            do
            {
                string stringToParse = GetUnparsedValue();

                failedParse = !(int.TryParse(stringToParse, out value));
            }
            while (failedParse == true);

            return value;
        }
        public static double ParseDouble(string message = null)
        {
            Parser.message = message;
            double value;
            bool failedParse;

            do
            {
                string stringToParse = GetUnparsedValue();
                failedParse = !(double.TryParse(stringToParse, out value));
            }
            while (failedParse == true);

            return value;
        }
        public static bool ParseBool(string message = null)
        {
            Parser.message = message;
            bool value;
            bool failedParse;

            do
            {
                string stringToParse = GetUnparsedValue();
                failedParse = !(bool.TryParse(stringToParse, out value));
                if (failedParse)
                {
                    if (int.TryParse(stringToParse, out int temp) && (temp == 0 || temp == 1))
                    {
                        value = Convert.ToBoolean(temp);
                        failedParse = false;
                    }
                }
            }
            while (failedParse == true);

            return value;
        }
        private static string GetUnparsedValue()
        {
            Console.Clear();
            Console.Write(message);

            string stringForParse = Console.ReadLine();

            return stringForParse;
        }
    }
}
