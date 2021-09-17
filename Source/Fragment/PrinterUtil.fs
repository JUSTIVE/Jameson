module PrinterUtil

open System
open Stringify

let colorize (target:string) (consoleColor:ConsoleColor) = 
    let InitializeColor () =
        Console.ForegroundColor <- ConsoleColor.White
    Console.ForegroundColor <- consoleColor
    printfn "%s" target
    InitializeColor()

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

let printWithIndent (indent:int32) (content:string) =
    let rec multiplyString (state:string) (times:int32) (content:string) =
        match times with
        | 0 -> state
        | __ -> multiplyString (state+content) (times - 1) content
    let tabs = multiplyString "" indent "\t"
    printfn "%s%s" tabs content