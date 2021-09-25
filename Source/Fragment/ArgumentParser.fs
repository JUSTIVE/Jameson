﻿module ArgumentParser
open System
open JamesonOption
open JamesonResult
open JamesonResults
open FileType
open State
open System.IO

type ArgumentType =
    | ValidArgument

type MassageTarget =
    | FileT
    | DirectoryT

type MassageResult = 
    | FileR of FileArgument
    | DirectoryR of list<FileArgument>

let rec parse_ (state:JamesonOption) (argument:list<string>):Result<JamesonOption,list<JamesonResult>> = 
    match argument with
    | h::t ->
        let subParser = 
            match h with
            | "-g" -> Some parseGeneralRunnerOption
            | "-t" -> Some parseTargetRunnerOption
            | "-w" -> Some parseShowRunnerOption
            | "-c" -> Some parseCheckConvention
            | "--v" -> Some parseVerboseOption
            | "--s" -> Some parseStrictOption
            | "--h" -> Some parseHelpOption
            | "--f" -> Some parseAutoFillOption
            | __ -> Option.None
        match subParser with 
        | Some subParseFunction-> 
            match subParseFunction state t with
            | Success(jamesonOption,restArgs) ->
                parse_ jamesonOption restArgs
            | Fail x -> Fail x
        | Option.None -> Fail [INVALID_ARGUMENT h]
    | __ -> Success state

and pathToFileArgument path =
    {
        filename = IO.FileInfo(path).Name
        path = FileInfo(path).FullName
    }

and massagePath (massageTarget:MassageTarget) (path:string):Result<MassageResult,JamesonResult> = 
    let pathType =
        if File.GetAttributes(path).HasFlag(FileAttributes.Directory) 
        then 
            Array.toList (IO.Directory.GetFiles(path))
            |> List.map pathToFileArgument 
            |> DirectoryR 
        else
            pathToFileArgument path 
            |> FileR 
    match massageTarget with
    | FileT ->
        match pathType with 
        | FileR _ -> Success pathType
        | DirectoryR _ -> Fail(INVALID_PATH_TYPE path)
    | DirectoryT ->
        match pathType with
        | FileR _ -> 
            IO.Directory.GetParent(path).GetFiles()
            |> Array.toList
            |> List.filter(fun x-> x.FullName <> FileInfo(path).FullName)
            |> List.map(fun x -> x.FullName)
            |> List.map pathToFileArgument 
            |> DirectoryR
            |> Success
        | DirectoryR _ -> Success pathType

and parseCheckConvention (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonResult>> = 
    match argument with
    | g::t -> 
        match g with
        | "camel"  -> Success (JamesonOptionSetCheckConventionLens state CamelCase,t)
        | "pascal" -> Success (JamesonOptionSetCheckConventionLens state PascalCase,t)
        | "upper"  -> Success (JamesonOptionSetCheckConventionLens state UpperCase,t)
        | "lower"  -> Success (JamesonOptionSetCheckConventionLens state LowerCase,t)
        | __ -> Fail [INSUFFICIENT_PATH_ARGUMENT_GENERALRUNNER]
    | __ -> Fail [INSUFFICIENT_PATH_ARGUMENT_GENERALRUNNER]

and parseShowRunnerOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonResult>> =
    match argument with
    | g::t -> 
        let sourcePathResolveResult = massagePath FileT g
        match sourcePathResolveResult with
        | Success(FileR sourceFileArgument)->
            let newOption = 
                {
                    source=sourceFileArgument;
                }
                |>ShowRunnerOption
                |>JamesonOptionSetRunnerTypeLens state 
            Success(newOption,t)
        | Success _ -> Fail [INVALID_PATH_TYPE g]
        | Fail e    -> Fail [e]
    | __-> Fail [INSUFFICIENT_PATH_ARGUMENT_GENERALRUNNER]

and parseGeneralRunnerOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonResult>> =
    match argument with
    | g::t -> 
        let sourcePathResolveResult = massagePath FileT g
        let targetPathResolveResult = massagePath DirectoryT g
        match (sourcePathResolveResult ,targetPathResolveResult) with
        | Success(FileR(sourceFileArgument)),Success(DirectoryR(neighbourFiles))->
            let newOption = 
                {
                    source=sourceFileArgument;
                    targetCandidate=neighbourFiles
                }
                |>GeneralRunnerOption
                |>JamesonOptionSetRunnerTypeLens state 
            Success(newOption,t)
        | Success _, Success _-> Fail [INVALID_PATH_TYPE g]
        | Success _, Fail e1 -> Fail [e1]
        | Fail e1, Success _ -> Fail [e1]
        | Fail e1, Fail e2 -> Fail [e1;e2]
    | __-> Fail([INSUFFICIENT_PATH_ARGUMENT_GENERALRUNNER])

and parseTargetRunnerOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonResult>> =
    match argument with
    | t1::t -> 
        match t with 
        | t2::t_ ->
            let sourcePathResolveResult = massagePath FileT t1
            let targetPathResolveResult = massagePath FileT t2
            match (sourcePathResolveResult ,targetPathResolveResult) with
            | Success(FileR(sourcePath)),Success(FileR(targetPath))->
                let newOption = 
                    TargetRunnerOption({source= sourcePath; target=targetPath})
                    |>JamesonOptionSetRunnerTypeLens state 
                Success(newOption,t_)
            | Success(FileR(_)),Success(DirectoryR(_))-> Fail [INVALID_PATH_TYPE t2]
            | Success(DirectoryR(_)),Success(FileR(_))-> Fail [INVALID_PATH_TYPE t1]
            | Success(DirectoryR(_)),Success(DirectoryR(_))-> Fail [INVALID_PATH_TYPE t1;INVALID_PATH_TYPE t2]
            | Success(_),Fail(e1) -> Fail [e1]
            | Fail(e1),Success(_) -> Fail [e1]
            | Fail(e1),Fail(e2) -> Fail [e1;e2]
        | __-> Fail [INSUFFICIENT_PATH_ARGUMENT_TARGETRUNNER]
    | __-> Fail [INSUFFICIENT_PATH_ARGUMENT_TARGETRUNNER]

and parseBooleanOption state argument key =
    Success(JamesonOptionSetBoolFlag state key,argument)

and parseStrictOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonResult>> =
    parseBooleanOption state argument "strict"

and parseVerboseOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonResult>> =
    parseBooleanOption state argument "verbose"

and parseHelpOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonResult>> =
    parseBooleanOption state argument "help"

and parseAutoFillOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonResult>> =
    parseBooleanOption state argument "autoFill"
    
let parse (arguments:string[]):Result<JamesonOption,list<JamesonResult>> =
    parse_ JamesonOptions.OptionDefault <| Array.toList arguments
