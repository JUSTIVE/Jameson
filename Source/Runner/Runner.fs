module Runner
open JamesonResult
open JamesonResults
open JamesonOption
open Printer
open PrinterType
open Diff
open State

let run (option:JamesonOption):Result<DiffFile,JamesonResult> =
   printJamesonOption true [NoneChild] option
   match option.runnerType with
   | TargetRunnerOption targetRunnerOption ->
        TargetRunner.run targetRunnerOption
   | GeneralRunnerOption generalRunnerOption ->
        GeneralRunner.run generalRunnerOption
   | None ->
        Fail INVALID_RUNNER_TYPE