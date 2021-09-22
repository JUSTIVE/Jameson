module PipeLine
open JamesonOption
open JamesonResult
open JamesonResults
open State
open Printer
open Runner

let private construct previousResult action =
    match previousResult with
    | Success someValue -> action someValue
    | Fail x -> x

let Flow jamesonOptionR: Result<JamesonResult,list<JamesonResult>> =
    match jamesonOptionR with
    | Success jamesonOption ->
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
            | Fail jamesonResult -> Fail jamesonResult
    | Fail jamesonResults ->
        Fail jamesonResults