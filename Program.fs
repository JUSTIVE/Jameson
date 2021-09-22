open System
open Help
open Printer
open State
open ArgumentParser
open PrinterType
open PipeLine

[<EntryPoint>]
let main (argv:string[]):int =
    match argv with
    | [||] -> 
        help()|>ignore
        1
    | __ ->
        match Flow <| parse argv with
        | Success goodResult ->
            printJamesonResult true [NoneChild] goodResult
        | Fail failedResults -> 
            printJamesonResults true [NoneChild] failedResults
    

