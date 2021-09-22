module PrinterType
open System

type ColorableIndent = 
    | OngoingChild
    | MidChild
    | LastChild

type IndentItem  =
    | ColorableIndent of ColorableIndent*ConsoleColor
    | EmptyChild
    | NoneChild

type Indent = list<IndentItem>