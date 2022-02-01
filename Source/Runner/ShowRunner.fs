module ShowRunner
open JamesonResults
open State
open JamesonResult
open Diff
open Result

let run jamesonOption showRunnerOption:Result<DiffResults,list<JamesonFail>> = 
    Fail [JamesonFail_ INVALID_RUNNER_TYPE Option.None]