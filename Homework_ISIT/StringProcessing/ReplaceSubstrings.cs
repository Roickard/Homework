using System.Collections.Generic;

namespace StringProcessing
{
	class ReplaceSubstrings
	{
        static public string Calculate(string value, List<string> subStrings)
        {
            string result = string.Copy(value);

            foreach (string subString in subStrings)
            {
                string tempValue = value.Replace(subString, "".PadLeft(subString.Length, '*'));

                string tempRes = "";
                for(int letter = 0; letter < result.Length; ++letter)
				{
                    if(tempValue[letter] == '*')
					{
                        tempRes += '*';
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
