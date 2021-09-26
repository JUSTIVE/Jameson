module PrinterUtil

open System
open PrinterType

let showPrint show action =
    match show with
    | true -> 
        action()
    | false -> ()

let  initializeColor () =
    Console.BackgroundColor <- ConsoleColor.Black
    Console.ForegroundColor <- ConsoleColor.White

let private colorize color =
    Console.ForegroundColor <- color

let printLogo () = 
    $"
    d8,                                                         
  `8P                                                          
                                                                
  d88   d888b8b    88bd8b,d88b  d8888b .d888b,d8888b   88bd88b 
  ?88  d8P' ?88    88P'`?8P'?8bd8b_,dP ?8b,  d8P' ?88  88P' ?8b
    88b 88b  ,88b  d88  d88  88P88b       `?8b88b  d88 d88   88P
    `88b`?88P'`88bd88' d88'  88b`?888P'`?888P'`?8888P'd88'   88b
     )88                                                        
    ,88P                                                        
`?888P                                                         
"
    |>printfn "%s" 

let printIndentItem (indentItem:IndentItem) =
    match indentItem with
    | ColorableIndent (colorableIndent,color) ->
        colorize color
        match colorableIndent with
        | OngoingChild  -> "│   "
        | MidChild      -> "├─  "
        | LastChild     -> "└─  "
        |>printf "%s"
        initializeColor()
    | EmptyChild -> printf "    " 
    | NoneChild  -> ()

let printIndent (indent:Indent) =
    let midToOngoing indentItem =
        match indentItem with 
        | ColorableIndent (colorableIndent,color) ->
            match colorableIndent with
            | MidChild -> ColorableIndent(OngoingChild,color)
            | __ -> ColorableIndent(colorableIndent,color)
        | __ -> indentItem

    match indent with
    | h::t ->
        h::(List.map midToOngoing t)
    | __ -> []
    |>List.rev
    |>List.map printIndentItem 
    |>ignore

let print show (indent:Indent) color newline content =
    if show 
        then
            printIndent indent
            colorize color
            printf $"{content}"
            initializeColor()
            if newline 
                then printf "\n" 
                else ()
        else ()
let printInline show indent color content = 
    print show indent color false content

let printBool show indent color newline bool = 
    print show indent (if bool then ConsoleColor.DarkGreen else ConsoleColor.DarkRed) newline bool

let printWithOptionName show indent color name printFunction content = 
    print show indent ConsoleColor.White false $"{name} : "
    printFunction show [] color true content 

let printType show indent typeName = 
    print show indent  ConsoleColor.White false $"Type Name : "
    print show [] ConsoleColor.DarkYellow true $"{typeName}" 

let printEmptyLine ()=
    printfn ""