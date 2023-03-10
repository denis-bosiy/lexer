using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;

namespace LL1
{
    public class Program
    {
        public static void InitializeTable(Table table)
        {
            using (TextFieldParser parser = new TextFieldParser("grammatics.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    string[] fields = parser.ReadFields();

                    string symbol = fields[1];
                    string ptrSymbolSet = fields[2];
                    bool isShift = fields[3].ToUpper() == "ДА" ? true : false;
                    bool isError = fields[4].ToUpper() == "ДА" ? true : false;
                    bool isNeedToAddToStack = fields[5].ToUpper() == "ДА" ? true : false;
                    bool isEndOfAction = fields[6].ToUpper() == "ДА" ? true : false;
                    int next = Int32.Parse(fields[7]);

                    table.addToTable(new TableElem(symbol, ptrSymbolSet, isShift, isError, isNeedToAddToStack, isEndOfAction, next));
                }
            }
        }

        public static void Main(string[] args)
        {
            StreamReader codeStream = new StreamReader(args[0]);
            StreamReader keywordsStream = new StreamReader("keywords.txt");
            Table table = new Table();
            InitializeTable(table);
            string code = codeStream.ReadLine();
            List<string> keywords = StringUtil.Split(keywordsStream.ReadLine());
            if (code == null)
            {
                Console.WriteLine("File is empty");
                return;
            }
            if (code.Equals(String.Empty))
            {
                Console.WriteLine("First line in file is empty");
                return;
            }
            LL1 ll1 = new LL1(code, table, keywords);
            try
            {
                ll1.Check();
                Console.WriteLine($"Code is lexically correct");
            }
            catch (ApplicationException e)
            {
                Console.WriteLine($"Parsing error: {e.Message}");
            }
        }
    }
}
