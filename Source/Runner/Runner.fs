module Runner
open JamesonResult
open JamesonOption
open Printer

let run (option:JamesonOption):JamesonResult =
   printJamesonOption option
   match option.runnerType with
   | TargetRunnerOption(x:TargetRunnerOption) ->
        TargetRunner.run x.sourcePath x.targetPath
   | GeneralRunnerOption(x:GeneralRunnerOption) ->
        GeneralRunner.run x.sourcePath
   