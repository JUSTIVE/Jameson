module ShowRunner
open JamesonResults
open JamesonResult
open Diff


let run jamesonOption showRunnerOption:Result<DiffResults.t,list<JamesonFail.t>> = 
    Error [JamesonFail.make INVALID_RUNNER_TYPE Option.None]