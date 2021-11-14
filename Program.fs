open System
open Help
open Printer
open State
open Result
open ArgumentParser
open PrinterType
open PipeLine

[<EntryPoint>]
let main (argv:string[]):int =
    
    match Array.toList argv with
    | [] -> 
        help()|>ignore
        1
    | "test"::t ->
        TestRunner.runTest t

    | __ ->
        match Flow <| parse argv with
        | Success goodResult ->
            printJamesonResult true [NoneChild] goodResult
        | Fail failedResults -> 
            printJamesonResults true [NoneChild] failedResults
    

