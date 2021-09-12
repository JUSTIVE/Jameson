module Stringify
open JamesonOption
open Diff

let stringifyBool (boolValue:bool):string =
    match boolValue with
    | true -> "true"
    | false -> "false"

let stringifyRunnerType (runnerType:RunnerType):string =
    match runnerType with
    | TargetRunner(x) -> $"TargetRunner\n\tsourcePath : {x.sourcePath}\n\ttargetPath : {x.targetPath}"
    | GeneralRunner(x) -> $"GeneralRunner\n\tsourcePath : {x.sourcePath}\n\ttargetCandidate : {x.targetCandidate}"

let stringifyDiffFile (diffFile:DiffFile):string =
    match diffFile with
    | Same -> "Same"
    | Different(x)-> "Different"