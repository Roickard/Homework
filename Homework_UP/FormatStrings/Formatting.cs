using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatStrings
{
    class Formatting
    {
        static public string Calculate(string value, List<string> subStrings)
        {
            string result = string.Copy(value);

            foreach (string subString in subStrings)
            {
                string tempValue = value.Replace(subString, subString.ToUpper());

                string tempRes = "";
                for (int letter = 0; letter < result.Length; ++letter)
                {
                    if (tempValue[letter].ToString().ToUpper() == tempValue[letter].ToString())
                    {
                        tempRes += tempValue[letter];
                    }
                    else
                    {
                        tempRes += result[letter];
                    }
                }
                result = string.Copy(tempRes);
            }

            return result;
        }
    }
}
