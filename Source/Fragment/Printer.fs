module Printer
open JamesonResult
open JamesonOption
open Stringify
open System

let colorize (target:string) (consoleColor:ConsoleColor) = 
    let InitializeColor () =
        Console.ForegroundColor <- ConsoleColor.White
    Console.ForegroundColor <- consoleColor
    printfn "%s" target
    InitializeColor()

let printJamesonResult (jamesonResult:JamesonResult):int = 
    match jamesonResult.errorCode = 0 with
    | true -> ConsoleColor.Green
    | false -> ConsoleColor.Red
    |> colorize jamesonResult.message 
    jamesonResult.errorCode

let printWithIndent (indent:int32) (content:string) =
    let rec multiplyString (state:string) (times:int32) (content:string) =
        match times with
        | 0 -> state
        | __ -> multiplyString (state+content) (times - 1) content
    let tabs = multiplyString "" indent "\t"
    printfn "%s%s" tabs content

let printJamesonOption (indent:int32) (jamesonOption:JamesonOption):Unit =
    let printRunnerType (indent:int32) (runnerType:RunnerTypeOption)=
        sprintf "RunnerType : %s" (stringifyRunnerType runnerType)
        |> printWithIndent indent
    
    printRunnerType indent jamesonOption.runnerType
    sprintf "Write to file : %s" (stringifyBool jamesonOption.writeToFile)
    |> printWithIndent indent

    sprintf "verbosity : %s" (stringifyBool jamesonOption.verbose)
    |> printWithIndent indent    

        