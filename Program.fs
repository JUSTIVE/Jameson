open System
open Help
open Printer
open JamesonOption
open JamesonResult
open JamesonResults
open State
open ArgumentParser

[<EntryPoint>]
let main (argv:string[]):int =
    match argv with
    | [||] -> 
        help()|>ignore
        1
    | x ->
        let option = parse argv
        match option with
        | Success(x:JamesonOption) -> 
            match Runner.run x with
            | Success(diffFile) ->
                printJamesonResult GOOD
            | Fail(jamesonResult) -> 
                printJamesonResult jamesonResult 
        | Fail(x:list<JamesonResult>) ->
            printJamesonResults x|>ignore
            help()|>ignore
            1

    

