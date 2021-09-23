module ShowRunner
open JamesonResults
open State

let run jamesonOption showRunnerOption = 
    Fail [INVALID_RUNNER_TYPE]