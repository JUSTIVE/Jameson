module PrinterUtil

open System
open Stringify

let showPrint show action =
    match show with
    | true -> action
    | false -> ()

let initializeColor () =
    Console.ForegroundColor <- ConsoleColor.White

let colorize color =
    Console.ForegroundColor <- color

let print(content:PrintType)=
    printfn "Type Name : %s%s" content.name content.content

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

let printType typeName = 
    colorize typeName ConsoleColor.Yellow
