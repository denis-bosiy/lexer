using System.Collections.Generic;

namespace LL1
{
    public class Table
    {
        private List<TableElem> elements = new List<TableElem>();

        public Table()
        {
        }

        public void addToTable(TableElem elem)
        {
            elements.Add(elem);
        }

        public TableElem GetElemtFromTableByIndex(int Index)
        {
            return elements[Index];
        }

        public List<TableElem> GetElemtsFromTableByChar(string Simbol)
        {
            List<TableElem> result = new List<TableElem>();
            foreach (TableElem elem in elements)
            {
                if (elem.Char == Simbol)
                {
                    result.Add(elem);
                }

            }

            return result;
        }
    }
}

