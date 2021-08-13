module Runner
open JamesonResult
open JamesonOption

let run (option:JamesonOption):JamesonResult =
   match option.runnerType with
   | TargetRunner(x:TargetRunner) ->
        TargetRunner.run x.sourcePath x.targetPath
   | GeneralRunner(x:GeneralRunner) ->
        GeneralRunner.run x.sourcePath
   