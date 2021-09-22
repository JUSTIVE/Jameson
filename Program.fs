open System
open Help
open Printer
open JamesonOption
open JamesonResult
open JamesonResults
open State
open ArgumentParser
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
            printJamesonResult true goodResult
        | Fail failedResults -> 
            printJamesonResults true failedResults
    

