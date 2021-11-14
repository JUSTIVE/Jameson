module ShowRunner
open JamesonResults
open State
open Result

let run jamesonOption showRunnerOption = 
    Fail [INVALID_RUNNER_TYPE]