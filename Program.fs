open System
open Help
open Printer
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
        | Ok goodResult ->
            printJamesonResult true [NoneChild] goodResult
        | Error failedResults -> 
            printJamesonFails true [NoneChild] failedResults
    

