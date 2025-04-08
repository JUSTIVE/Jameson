module Runner
open JamesonResult
open JamesonResults
open JamesonOption
open Diff

let run (option:JamesonOption.t):Result<DiffResults.t,list<JamesonFail.t>> =
   //printJamesonOption true [NoneChild] option
   match option.runnerType with
   | TargetRunnerOption targetRunnerOption ->
        TargetRunner.run option targetRunnerOption
   | GeneralRunnerOption generalRunnerOption -> 
        GeneralRunner.run option generalRunnerOption
   | ShowRunnerOption showRunnerOption ->
        ShowRunner.run option showRunnerOption
   | None ->
        Error [JamesonFail.make INVALID_RUNNER_TYPE Option.None]