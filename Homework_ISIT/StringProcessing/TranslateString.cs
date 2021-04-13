namespace StringProcessing
{
	class TranslateString
	{
        static private string[] letters = new string[2] 
        { 
            "qwertyuiop[]asdfghjkl;\'zxcvbnm,.QWERTYUIOP{}ASDFGHJKL:\"ZXCVBNM<>?/`~", 
            "йцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ,.ёЁ" 
        };
        static public string Calculate(string value)
        {
            for (int letter = 0; letter < letters[0].Length; ++letter)
            {
                value = value.Replace(letters[0][letter], letters[1][letter]);
            }

            return value;
        }
    }
}
