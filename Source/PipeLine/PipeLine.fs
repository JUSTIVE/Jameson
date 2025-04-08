module PipeLine
open JamesonOption.JamesonOption
open JamesonResult
open JamesonResults
open Printer
open PrinterType

let Flow jamesonOptionR: Result<JamesonResult,list<JamesonFail.t>> =
    match jamesonOptionR with
    | Ok jamesonOption ->
        printJamesonOption jamesonOption.verbose [NoneChild] jamesonOption
        if jamesonOption.help then 
            Ok <|Help.help()
        else
            match Runner.run jamesonOption with
            | Ok diffFile -> 
                printDiffFile true [] diffFile.originFile
                diffFile.compareeFiles
                |> List.iter (printDiffFile true [])
                Ok GOOD
            | Error jamesonResults ->
                Error jamesonResults
    | Error jamesonResults ->
        Error jamesonResults