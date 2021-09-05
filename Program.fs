open System
open Help
open Printer
open JamesonOption
open JamesonResult
open ArgumentParser

[<EntryPoint>]
let main (argv:string[]):int =
    match argv with
    | [||] -> help
    | x ->
        let option = ArgumentParser.parse argv
        match option with
        | ParseResult.Success(x:JamesonOption) -> Runner.run x
        | ParseResult.Fail(x:JamesonResult) -> x
    |>print

