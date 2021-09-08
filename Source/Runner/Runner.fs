module Runner
open JamesonResult
open JamesonOption
open Printer

let run (option:JamesonOption):JamesonResult =
   printJamesonOption 0 option
   match option.runnerType with
   | TargetRunner(x:TargetRunner) ->
        TargetRunner.run x.sourcePath x.targetPath
   | GeneralRunner(x:GeneralRunner) ->
        GeneralRunner.run x.sourcePath
   