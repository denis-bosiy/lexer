using System.Collections.Generic;

namespace LL1
{
    public class StringUtil
    {
        static public List<string> Split(string line, char delimiter = ',')
        {
            List<string> elems = new List<string>();

            string lexem = "";
            foreach (char symbol in line)
            {
                if (symbol == delimiter)
                {
                    if (lexem == "")
                    {
                        lexem = delimiter.ToString();
                    }
                    elems.Add(lexem);
                    lexem = "";
                }
                else
                {
                    lexem += symbol.ToString();
                }
            }
            elems.Add(lexem);

            return elems;
        }
    }
}
