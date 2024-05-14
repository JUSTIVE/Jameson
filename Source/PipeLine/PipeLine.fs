module PipeLine
open JamesonOption
open JamesonResult
open JamesonResults
open Printer
open PrinterType

let Flow jamesonOptionR: Result<JamesonResult,list<JamesonFail>> =
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
                |> List.map (printDiffFile true [])
                |> ignore
                Ok GOOD
            | Error jamesonResults ->
                Error jamesonResults
    | Error jamesonResults ->
        Error jamesonResults