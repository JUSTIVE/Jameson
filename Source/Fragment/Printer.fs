module Printer
open JamesonResult
open JamesonOption
open Stringify
open PrinterUtil
open System

let printJamesonResult show (jamesonResult:JamesonResult) :int = 
    let innerPrint =
        match jamesonResult.errorCode = 0 with
        | true -> ConsoleColor.Green
        | false -> ConsoleColor.Red
        |> colorize jamesonResult.message
    innerPrint
    |> showPrint show
    jamesonResult.errorCode

let printJamesonResults show (jamesonResults:list<JamesonResult>) :int =
    jamesonResults
    |>List.map(printJamesonResult show)
    |>List.fold (fun x y->if x > y then x else y) 0

let printJamesonOption show jamesonOption =
    jamesonOption
    |> JamesonOption
    |> stringify
    |> print

let printDiffFile show (diffFile) = 
    let innerPrint = 
        diffFile
        |>DiffFile
        |>stringify
        |>print
    innerPrint
    |> showPrint show