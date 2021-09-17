module Printer
open JamesonResult
open JamesonOption
open Stringify
open PrinterUtil
open System

let printJamesonResult (jamesonResult:JamesonResult):int = 
    match jamesonResult.errorCode = 0 with
    | true -> ConsoleColor.Green
    | false -> ConsoleColor.Red
    |> colorize jamesonResult.message 
    jamesonResult.errorCode

let printJamesonOption (jamesonOption:JamesonOption):Unit =
    stringify (Stringify.PrintableType.JamesonOption jamesonOption)
    |> print