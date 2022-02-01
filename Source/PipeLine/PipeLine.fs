module PipeLine
open JamesonOption
open JamesonResult
open JamesonResults
open State
open Result
open Printer
open PrinterType
open Runner

let private construct previousResult action =
    match previousResult with
    | Success someValue -> action someValue
    | Fail x -> x

let Flow jamesonOptionR: Result<JamesonResult,list<JamesonResult>> =
    match jamesonOptionR with
    | Success jamesonOption ->
        printJamesonOption jamesonOption.verbose [NoneChild] jamesonOption
        if jamesonOption.help then 
            Success <|Help.help()
        else
            match Runner.run jamesonOption with
            | Success diffFile -> 
                printDiffFile true [] diffFile.originFile
                diffFile.compareeFiles
                |> List.map (printDiffFile true [])
                |> ignore
                Success GOOD
            | Fail jamesonResult ->
                printJamesonResults true [NoneChild] jamesonResult
                |>ignore
                Fail jamesonResult
    | Fail jamesonResults ->
        Fail jamesonResults