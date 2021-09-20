module Runner
open JamesonResult
open JamesonOption
open Printer
open Diff
open State

let run (option:JamesonOption):Result<DiffFile,JamesonResult> =
   printJamesonOption option
   match option.runnerType with
   | TargetRunnerOption(x:TargetRunnerOption) ->
        TargetRunner.run x.sourcePath x.targetPath
   | GeneralRunnerOption(x:GeneralRunnerOption) ->
        GeneralRunner.run x.sourcePath
   