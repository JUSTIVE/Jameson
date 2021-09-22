module GeneralRunner
open JamesonResult
open State
open JamesonOption
open Diff

let run (generalRunnerOption:GeneralRunnerOption):Result<DiffFile,JamesonResult>=
    Success Same