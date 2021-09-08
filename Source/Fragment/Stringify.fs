module Stringify
open JamesonOption
open Diff

let stringifyBool (boolValue:bool):string =
    match boolValue with
    | true -> "true"
    | false -> "false"

let stringifyRunnerType (runnerType:RunnerType):string =
    match runnerType with
    | TargetRunner(x) -> "TargetRunner"
    | GeneralRunner(x) -> "GeneralRunner"

let stringifyDiffFile (diffFile:DiffFile):string =
    match diffFile with
    | Same -> "Same"
    | Different(x)-> "Different"