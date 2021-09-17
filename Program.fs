open System
open Help
open Printer
open JamesonOption
open JamesonResult
open ArgumentParser

[<EntryPoint>]
let main (argv:string[]):int =
    match argv with
    | [||] -> 
        help()|>ignore
        1
    | x ->
        let option = ArgumentParser.parse argv
        match option with
        | ParseResult.Success(x:JamesonOption) -> 
            Runner.run x
            |>printJamesonResult
        | ParseResult.Fail(x:JamesonResult) ->
            printJamesonResult x|>ignore
            help()|>ignore
            1

    

