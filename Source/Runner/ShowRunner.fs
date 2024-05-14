module ShowRunner
open JamesonResults
open JamesonResult
open Diff


let run jamesonOption showRunnerOption:Result<DiffResults,list<JamesonFail>> = 
    Error [JamesonFail_ INVALID_RUNNER_TYPE Option.None]