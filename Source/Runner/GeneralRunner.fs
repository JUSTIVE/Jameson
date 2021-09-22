module GeneralRunner
open JamesonResult
open JamesonResults
open State
open JamesonOption
open Diff

let run jamesonOption generalRunnerOption:Result<DiffResults,list<JamesonResult>> =
    Fail [INVALID_RUNNER_TYPE]