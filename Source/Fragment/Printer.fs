module Printer
open JamesonResult
open JamesonOption
open PrinterType
open PrinterUtil
open System

let printJamesonResult show indent (jamesonResult:JamesonResult) :int = 
    let innerPrint () =
        printType show indent "JamseonResult"
        let messageColor = 
            match jamesonResult.errorCode = 0 with
            | true -> ConsoleColor.DarkGreen
            | false -> ConsoleColor.Red
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) messageColor "message" print jamesonResult.message 
        printWithOptionName show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.Gray "errorCode" print jamesonResult.errorCode 
    innerPrint
    |> showPrint show
    jamesonResult.errorCode

let printJamesonResults show indent (jamesonResults:list<JamesonResult>) :int =
    jamesonResults
    |>List.map(printJamesonResult show indent)
    |>List.fold (fun x y->if x > y then x else y) 0

let printTargetRunnerOption show indent (targetRunnerOption:TargetRunnerOption)= 
    let innerPrint () =
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.Gray "sourcePath" print $"{targetRunnerOption.sourcePath}"
        printWithOptionName show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.Gray "targetPath" print $"{targetRunnerOption.targetPath}"
    innerPrint
    |> showPrint show

let printRunnerType show indent runnerType = 
    let innerPrint () =
        match runnerType with
        | TargetRunnerOption t -> 
            printType show indent $"RunnerType : TargetRunnerOption"
            printTargetRunnerOption  show indent t
        | GeneralRunnerOption g ->
            printType show indent $"RunnerType : GeneralRunnerOption"
        | None -> 
            print show indent ConsoleColor.White true "none"
    innerPrint
    |> showPrint show

let printOption show indent consoleColor newline option =
    match option with
    | Option.Some x -> print show indent consoleColor newline x
    | Option.None   -> print show indent ConsoleColor.DarkRed newline "None" 
    

let printJamesonOption show indent (jamesonOption:JamesonOption) =
    let innerPrint () =
        printType show indent "JamseonOption"
        printRunnerType show (ColorableIndent(MidChild,ConsoleColor.White)::indent) jamesonOption.runnerType
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.White "write to file" printOption jamesonOption.writeToFile
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.White "verbose" printBool jamesonOption.verbose
        printWithOptionName show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.White "help" printBool jamesonOption.help
    innerPrint
    |> showPrint show

