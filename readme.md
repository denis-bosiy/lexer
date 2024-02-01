# Лексер собственного языка программирования

### Суть задания
Изобрести свой язык программирования и написать для него лексический анализатор

### Как запустить тесты?
1. Сбилдить проект в дебаг окружении
2. Запустить run_tests.bat в bin/Debug/net6.0

### Синтаксические правила языка в итерационной форме
```
<PROGRAM> -> <VAR BLOCK><LOGIC BLOCK>

<VAR BLOCK> -> var<VAR BLOCK IDENTIFIERS>
<VAR BLOCK IDENTIFIERS> -> <IDENTIFIER NAMES>: <IDENTIFER TYPE>!\n<VAR BLOCK IDENTIFIERS>|E
<IDENTIFIER NAMES> -> <IDENTIFIER><SPARE 1>
<SPARE 1> -> ,<IDENTIFIER><SPARE 1>|E
<IDENTIFIER TYPE> -> int|short|string

<LOGIC BLOCK> -> beginn<LOGIC INNER>ende
<LOGIC INNER> -> <OPERATORS LIST>|E

<OPERATORS LIST> -> <OPERATOR><SPARE 2>
<SPARE 2> -> !<OPERATOR><SPARE 2>|E
<OPERATOR> -> <ASSIGN OPERATOR>|<WRITE OPERATOR>|<READ OPERATOR>|<CONDITIONAL OPERATOR>
<ASSIGN OPERATOR> -> <IDENTIFIER>=<EXPRESSION>

<EXPRESSION> -> <SPARE 7><SPARE 8>
<SPARE 8> -> +<SPARE 7><SPARE 8>|E
<SPARE 7> -> <NUMERIC VALUE(F)><SPARE 9>
<SPARE 9> -> *<NUMERIC VALUE(F)><SPARE 9>|E


<IDENTIFIER> -> <LETTER><SPARE 3>
<SPARE 3> -> <LETTER OR DIGIT><SPARE 3>|E

<VALUE> -> <STRING VALUE>|<NUMERIC VALUE>

<STRING VALUE> -> <IDENTIFIER>|<STRING>
<STRING> -> "<CHARACTER SEQUENCE>"
<CHARACTER SEQUENCE> -> <CHAR><SPARE4>
<SPARE 4> -> <CHAR><SPARE 4>|E

<NUMERIC VALUE(F)> -> -<NUMERIC VALUE(F)>|(<EXPRESSION>)|<IDENTIFIER>|<NUMBER>
<NUMBER> -> <DIGIT><SPARE 5>
<SPARE 5> -> <DIGIT><SPARE 5>|E

<WRITE OPERATOR> -> aufschreiben(<VALUE>)

<READ OPERATOR> -> erhalten(<IDENTIFIER>)

<CONDITIONAL OPERATOR> -> wenn <CONDITION> dann <OPERATOR>
<CONDITION> -> <SIMPLE CONDITION><SPARE 6>
<SIMPLE CONDITION> -> (<VALUE><CONDITIONAL SYMBOL><VALUE>)
<SPARE 6> -> <BOOL LOGIC OPERATOR><SIMPLE CONDITION><SPARE 6>|E
<BOOL LOGIC OPERATOR> -> && | ||
<CONDITIONAL SYMBOL> -> < | > | == | != | >= | <=

<SEPARATED_SYMBOL> -> <SPACE>|<NEW LINE SYMBOL>
```
