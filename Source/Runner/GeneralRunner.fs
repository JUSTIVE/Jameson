module GeneralRunner
open JamesonResult
open State
open JamesonResults
open Diff

let run (originFilePath:string):Result<DiffFile,JamesonResult>=
    Success(Same)