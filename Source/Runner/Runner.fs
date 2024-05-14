module Runner
open JamesonResult
open JamesonResults
open JamesonOption
open Diff

let run (option:JamesonOption):Result<DiffResults,list<JamesonFail>> =
   //printJamesonOption true [NoneChild] option
   match option.runnerType with
   | TargetRunnerOption targetRunnerOption ->
        TargetRunner.run option targetRunnerOption
   | GeneralRunnerOption generalRunnerOption -> 
        GeneralRunner.run option generalRunnerOption
   | ShowRunnerOption showRunnerOption ->
        ShowRunner.run option showRunnerOption
   | None ->
        Error [JamesonFail_ INVALID_RUNNER_TYPE Option.None]