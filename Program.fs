open System
open Help
open Printer
open JamesonOption
open ArgumentParser

[<EntryPoint>]
let main (argv:string[]):int =
    match argv with
    | [||] -> help
    | x ->
        let option = ArgumentParser.parse argv
        match option with
        | Success(x:JamesonOption) -> Runner.run x
        | Fail(x) -> x
    |>print

