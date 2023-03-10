using System;
using System.Collections.Generic;
using System.IO;

namespace LL1
{
    public class LL1
    {
        private int _currentReadIndex;
        private readonly List<string> _lexems;
        private string _currentLexem;

        private int _currentTableElemIndex;
        private Table _table;

        private List<string> Split(string code, List<string> keywords)
        {
            List<string> elems = new List<string>();
            List<char> delimiters = new List<char>();
            delimiters.Add(' ');
            delimiters.Add('!');
            delimiters.Add('(');
            delimiters.Add(')');
            int possibleKeywordChIndex = 0;
            bool isStringValue = false;
            string lexem = "";

            foreach (char symbol in code)
            {
                lexem += symbol;

                if (symbol == '"')
                {
                    isStringValue = !isStringValue;
                    char lastLexemSymbol = lexem[lexem.Length - 1];
                    string clearKeyword = lexem.Substring(0, lexem.Length - 1);
                    if (keywords.Contains(clearKeyword))
                    {
                        elems.Add(clearKeyword);
                    }
                    else
                    {
                        foreach (char clearKeywordCh in clearKeyword)
                        {
                            elems.Add(clearKeywordCh.ToString());
                        }
                    }
                    elems.Add(lastLexemSymbol.ToString());
                    lexem = "";
                    possibleKeywordChIndex = 0;
                    continue;
                }
                if (!isStringValue)
                {
                    bool isCompatibleToSomeKeyword = false;
                    foreach (string keyword in keywords)
                    {
                        if (possibleKeywordChIndex < keyword.Length && possibleKeywordChIndex < lexem.Length && keyword.Substring(0, possibleKeywordChIndex + 1) == lexem.Substring(0, possibleKeywordChIndex + 1))
                        {
                            possibleKeywordChIndex++;
                            isCompatibleToSomeKeyword = true;
                            break;
                        }
                    }
                    if (!isCompatibleToSomeKeyword)
                    {
                        possibleKeywordChIndex = 0;
                    }
                }

                if (lexem != "" && possibleKeywordChIndex == 0)
                {
                    char lastLexemSymbol = lexem[lexem.Length - 1];
                    string clearKeyword = lexem.Substring(0, lexem.Length - 1);
                    if (keywords.Contains(clearKeyword) && delimiters.Contains(lastLexemSymbol))
                    {
                        elems.Add(clearKeyword);
                    }
                    else
                    {
                        foreach (char clearKeywordCh in clearKeyword)
                        {
                            elems.Add(clearKeywordCh.ToString());
                        }
                    }
                    elems.Add(lastLexemSymbol.ToString());
                    lexem = "";
                }
            }
            elems.Add(lexem);

            return elems;
        }

        public LL1(string code, Table table, List<string> keywords)
        {
            _currentReadIndex = 0;
            _lexems = Split(code.ToLower(), keywords);

            _currentTableElemIndex = 0;
            _table = table;
        }

        private int GetCurrentLexemChIndex()
        {
            int currentLexemChIndex = 0;

            for (int i = 0; i < _currentReadIndex; i++)
            {
                currentLexemChIndex += _lexems[i].Length;
            }

            return currentLexemChIndex;
        }

        private void ThrowErrorAtIndex()
        {
            throw new ApplicationException("Error on '" + _currentLexem.ToString() + "' lexem on " + GetCurrentLexemChIndex() + " index");
        }

        #region Lexem
        private void MoveLexem()
        {
            if (_currentReadIndex == _lexems.Count)
            {
                _currentLexem = "E";
                throw new EndOfStreamException();
            }

            while (_lexems[_currentReadIndex].Equals(" "))
            {
                _currentReadIndex++;
            }

            _currentLexem = _lexems[_currentReadIndex++];
        }

        private string GetCurrentLexem()
        {
            return _currentLexem.ToLower();
        }
        #endregion

        public void Check()
        {
            bool isReachedEndOfStream = false;
            bool isFoundEndOfProgram = false;
            Stack<int> addresses = new Stack<int>();
            MoveLexem();

            while (!isReachedEndOfStream)
            {
                TableElem tableRow = _table.GetElemtFromTableByIndex(_currentTableElemIndex);
                //Console.WriteLine(_currentTableElemIndex + " " + _currentLexem);

                bool isCurrentLexemInPtrCharSet = tableRow.PtrCharSet.Contains(GetCurrentLexem());
                bool isEmptySymbolInPtrCharSet = tableRow.PtrCharSet.IndexOf("E") != -1;

                if (isCurrentLexemInPtrCharSet || isEmptySymbolInPtrCharSet)
                {
                    if (tableRow.IsEndOfAction && addresses.Count == 0)
                    {
                        isFoundEndOfProgram = true;
                        break;
                    }

                    if (tableRow.IsNeedToAddToStack)
                    {
                        addresses.Push(_currentTableElemIndex + 1);
                    }

                    if (isCurrentLexemInPtrCharSet && tableRow.IsShift)
                    {
                        try
                        {
                            MoveLexem();
                        }
                        catch (EndOfStreamException)
                        {
                            isReachedEndOfStream = true;
                        }
                    }

                    _currentTableElemIndex = tableRow.NextElem != -1 ? tableRow.NextElem : addresses.Pop();
                }
                else
                {
                    if (tableRow.IsError)
                    {
                        ThrowErrorAtIndex();
                    }

                    _currentTableElemIndex++;
                }
            }

            if (!isFoundEndOfProgram)
            {
                ThrowErrorAtIndex();
            }
        }
    }
}
