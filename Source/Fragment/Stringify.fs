module Stringify
open JamesonOption
open Diff

let stringifyBool (boolValue:bool):string =
    match boolValue with
    | true -> "true"
    | false -> "false"

let stringifyRunnerType (runnerType:RunnerTypeOption):string =
    match runnerType with
    | TargetRunnerOption(x) -> $"TargetRunner\n\tsourcePath : {x.sourcePath}\n\ttargetPath : {x.targetPath}"
    | GeneralRunnerOption(x) -> $"GeneralRunner\n\tsourcePath : {x.sourcePath}\n\ttargetCandidate : {x.targetCandidate}"

let stringifyDiffFile (diffFile:DiffFile):string =
    match diffFile with
    | Same -> "Same"
    | Different(x)-> "Different"