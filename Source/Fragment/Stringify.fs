module Stringify
open JamesonOption
open Diff

type PrintableType = 
    | RunnerTypeOption of RunnerTypeOption
    | DiffFile of DiffFile
    | JamesonOption of JamesonOption

type PrintType ={
    name:string;
    content:string;
}

let stringifyBool (boolValue:bool):string =
    match boolValue with
    | true -> "true"
    | false -> "false"

let stringifyRunnerTypeOption (runnerType:RunnerTypeOption):string =
    match runnerType with
    | TargetRunnerOption(x) -> 
        $"TargetRunner
        ├  sourcePath : {x.sourcePath}
        └  targetPath : {x.targetPath}"
    | GeneralRunnerOption(x) -> 
        $"GeneralRunner
        ├  sourcePath : {x.sourcePath}
        └  targetCandidate : {x.targetCandidate}"

let stringifyDiffFile (diffFile:DiffFile):string =
    match diffFile with
    | Same -> "Same"
    | Different(x)-> "Different"

let stringifyJamesonOption (jamesonOption:JamesonOption) :string = 
    $"
    ├  RunnerType : {stringifyRunnerTypeOption jamesonOption.runnerType}
    ├  WriteFile : {stringifyBool jamesonOption.writeToFile}
    └  Verbose : {stringifyBool jamesonOption.verbose}\n"

let stringify (param:PrintableType) : PrintType = 
    let name = 
        match param with
        | RunnerTypeOption(runnerTypeOption) -> "RunnerTypeOption"
        | DiffFile(diffFile) -> "DiffFile"
        | JamesonOption(jamesonOption) -> "JamesonOption"

    let content = 
        match param with
        | RunnerTypeOption(runnerTypeOption)-> stringifyRunnerTypeOption runnerTypeOption
        | DiffFile(diffFile) -> stringifyDiffFile diffFile
        | JamesonOption(jamesonOption) -> stringifyJamesonOption jamesonOption

    {name=name;content=content}    



