module TestPrinter
open System
open TestResult 
open PrinterUtil

let PrintUnitResult (prefix:string) (x:UnitTestResult):Unit = 
    printf $"{prefix}\t"
    match x with 
    | (name, UnitSuccess) ->
        Console.BackgroundColor <- ConsoleColor.DarkGreen
        printf "Success"
        initializeColor()
        printfn $"\t{name}"
        initializeColor()
    | (name, UnitFail(reason:FailedReason)) -> 
        Console.BackgroundColor <- ConsoleColor.DarkRed
        printf "Fail"
        initializeColor()
        printf $"\t{name}\t"
        Console.ResetColor()
        printf $"expected : "
        Console.ForegroundColor<- ConsoleColor.DarkYellow
        printf $"{reason.expected}"
        Console.ResetColor()
        printf $" \tgiven : "
        Console.ForegroundColor<- ConsoleColor.DarkRed
        printfn $"{reason.given}"
        initializeColor()
    

let PrintModuleResult (x:ModuleTest):int =
    match x with
    | (moduleName,result,unitTestResults)-> 
        initializeColor()
        let badge:string=   
            match result with
            | ModuleSuccess-> 
                Console.BackgroundColor <- ConsoleColor.DarkGreen
                "SUCCESS"
            | ModuleFail-> 
                Console.BackgroundColor <- ConsoleColor.DarkRed
                "FAIL"
        printf $"{badge}"
        initializeColor()
        let totalUnitLength = List.length unitTestResults
        let successUnitLength = 
            unitTestResults
            |> successUnitTests
            |> List.length
        printfn $"\t[{successUnitLength}/{totalUnitLength}]\tModule::{moduleName}"
        if successUnitLength <> totalUnitLength then 
            unitTestResults
            |>List.mapi (
                fun i x ->
                    PrintUnitResult (if i = totalUnitLength - 1 then "└─  " else "├─  ") x)
            |>ignore
        if successUnitLength <> totalUnitLength then 1 else 0
