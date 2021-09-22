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
        match Runner.run jamesonOption with
        | Success diffFile -> 
            //printDiffFile true diffFile
            Success GOOD
        | Fail jamesonResult -> Fail [jamesonResult]
    | Fail jamesonResults ->
        Fail jamesonResults