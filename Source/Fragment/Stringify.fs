module Stringify
open System
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

let rec stringify (param:PrintableType) : PrintType = 
    let name = 
        match param with
        | RunnerTypeOption runnerTypeOption -> "RunnerTypeOption"
        | DiffFile diffFile -> "DiffFile"
        | JamesonOption jamesonOption -> "JamesonOption"

    let content = 
        match param with
        | RunnerTypeOption runnerTypeOption -> stringifyRunnerTypeOption runnerTypeOption
        | DiffFile diffFile -> stringifyDiffFile diffFile
        | JamesonOption jamesonOption -> stringifyJamesonOption jamesonOption
    {name=name;content=content}    

    
and stringifyOption (value:option<'a>) : string=
    match value with
    | Some x -> string x
    | Option.None -> "None"

and stringifyRunnerTypeOption (runnerType:RunnerTypeOption):string =
    match runnerType with
    | TargetRunnerOption(x) -> 
        $"TargetRunner
        ├  sourcePath : {x.sourcePath}
        └  targetPath : {x.targetPath}"
    | GeneralRunnerOption(x) -> 
        $"GeneralRunner
        ├  sourcePath : {x.sourcePath}
        └  targetCandidate : {x.targetCandidate}"
    | None ->
        $"Invalid RunnerType"

and stringifyDiffFile (diffFile:DiffFile):string =
    match diffFile with
    | Same -> $"
    └ Same"
    | Different x -> $": Different"

and stringifyJamesonOption (jamesonOption:JamesonOption) :string = 
    $"
    ├  RunnerType : {stringifyRunnerTypeOption jamesonOption.runnerType}
    ├  WriteFile : {stringifyOption jamesonOption.writeToFile}
    └  Verbose : {string jamesonOption.verbose}\n"

