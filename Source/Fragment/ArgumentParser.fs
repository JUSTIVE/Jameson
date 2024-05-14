module ArgumentParser
open System
open JamesonOption
open JamesonResult
open JamesonResults
open FileType
open System.IO

type ArgumentType =
    | ValidArgument

type MassageTarget =
    | FileT
    | DirectoryT

type MassageResult = 
    | FileR of FileArgument
    | DirectoryR of list<FileArgument>

let rec parse_ (state:JamesonOption) (argument:list<string>):Result<JamesonOption,list<JamesonFail>> =
    let subParser x = 
        match x with
        | "-g" -> Some parseGeneralRunnerOption
        | "-t" -> Some parseTargetRunnerOption
        | "-w" -> Some parseShowRunnerOption
        | "-c" -> Some parseCheckConvention
        | "--v" -> Some parseVerboseOption
        | "--s" -> Some parseStrictOption
        | "--h" -> Some parseHelpOption
        | "--f" -> Some parseAutoFillOption
        | __ -> Option.None
    match argument with
    | h::t ->
        subParser h
        |> Option.map (fun func ->
            func state t
            |> Result.bind (fun (option,restArg) -> parse_ option restArg)
        )
        |> Option.defaultWith(fun () -> Error [JamesonFail_Default (INVALID_ARGUMENT h)])
    | __ -> Ok state

and pathToFileArgument path =
    {
        filename = IO.FileInfo(path).Name
        path = FileInfo(path).FullName
    }

and massagePath (massageTarget:MassageTarget) (path:string):Result<MassageResult,JamesonFail> = 
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
        | FileR _ -> Ok pathType
        | DirectoryR _ -> Error<| JamesonFail_Default (INVALID_PATH_TYPE path)
    | DirectoryT ->
        match pathType with
        | FileR _ -> 
            IO.Directory.GetParent(path).GetFiles()
            |> Array.toList
            |> List.filter(fun x-> x.FullName <> FileInfo(path).FullName)
            |> List.map(fun x -> x.FullName)
            |> List.map pathToFileArgument 
            |> DirectoryR
            |> Ok
        | DirectoryR _ -> Ok pathType

and parseCheckConvention state argument  = 
    match argument with
    | g::t -> 
        let OkValue x = Ok (JamesonOptionSetCheckConventionLens state x,t)
        match g.ToLower() with
        | "camel"  -> OkValue (HeadCharConventionType CamelCase)
        | "pascal" -> OkValue (HeadCharConventionType PascalCase)
        | "upper"  -> OkValue (SimpleConventionType UpperCase)
        | "lower"  -> OkValue (SimpleConventionType LowerCase)
        | "snake"  -> OkValue (ComplexConventionType SnakeCase)
        | __ -> Error [JamesonFail_Default (INVALID_ARGUMENT g)]
    | __ -> Error [JamesonFail_Default INSUFFICIENT_PATH_ARGUMENT]

and parseShowRunnerOption state argument =
    match argument with
    | g::t -> 
        let sourcePathResolveResult = massagePath FileT g
        match sourcePathResolveResult with
        | Ok (FileR sourceFileArgument)->
            let newOption = 
                {
                    source=sourceFileArgument;
                }
                |>ShowRunnerOption
                |>JamesonOptionSetRunnerTypeLens state 
            Ok (newOption,t)
        | Ok _ -> Error [JamesonFail_Default (INVALID_PATH_TYPE g)]
        | Error e    -> Error [e]
    | __-> Error [JamesonFail_Default INSUFFICIENT_PATH_ARGUMENT]

and parseGeneralRunnerOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonFail>> =
    match argument with
    | g::t -> 
        let sourcePathResolveResult = massagePath FileT g
        let targetPathResolveResult = massagePath DirectoryT g
        match (sourcePathResolveResult ,targetPathResolveResult) with
        | Ok(FileR(sourceFileArgument)),Ok (DirectoryR(neighbourFiles))->
            let newOption = 
                {
                    source=sourceFileArgument;
                    targetList=neighbourFiles
                }
                |>GeneralRunnerOption
                |>JamesonOptionSetRunnerTypeLens state 
            Ok(newOption,t)
        | Ok _, Ok _-> Error [JamesonFail_Default (INVALID_PATH_TYPE g)]
        | Ok _, Error e1 -> Error [e1]
        | Error e1, Ok _ -> Error [e1]
        | Error e1, Error e2 -> Error [e1;e2]
    | __-> Error [JamesonFail_Default INSUFFICIENT_PATH_ARGUMENT]

and parseTargetRunnerOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonFail>> =
    match argument with
    | t1::t -> 
        match t with 
        | t2::t_ ->
            let sourcePathResolveResult = massagePath FileT t1
            let targetPathResolveResult = massagePath FileT t2
            match (sourcePathResolveResult ,targetPathResolveResult) with
            | Ok(FileR(sourcePath)),Ok(FileR(targetPath))->
                let newOption = 
                    TargetRunnerOption({source= sourcePath; target=targetPath})
                    |>JamesonOptionSetRunnerTypeLens state 
                Ok(newOption,t_)
            | Ok(FileR(_)),Ok(DirectoryR(_))-> Error [JamesonFail_Default (INVALID_PATH_TYPE t2)]
            | Ok(DirectoryR(_)),Ok(FileR(_))-> Error [JamesonFail_Default (INVALID_PATH_TYPE t1)]
            | Ok(DirectoryR(_)),Ok(DirectoryR(_))-> 
                Error [JamesonFail_Default (INVALID_PATH_TYPE t1);
                    JamesonFail_Default (INVALID_PATH_TYPE t2)]
            | Ok(_),Error(e1) -> Error [e1]
            | Error(e1),Ok(_) -> Error [e1]
            | Error(e1),Error(e2) -> Error [e1;e2]
        | __-> Error [JamesonFail_Default INSUFFICIENT_PATH_ARGUMENT]
    | __-> Error [JamesonFail_Default INSUFFICIENT_PATH_ARGUMENT]

and parseBooleanOption state argument key =
    Ok(JamesonOptionSetBoolFlag state key,argument)

and parseStrictOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonFail>> =
    parseBooleanOption state argument "strict"

and parseVerboseOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonFail>> =
    parseBooleanOption state argument "verbose"

and parseHelpOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonFail>> =
    parseBooleanOption state argument "help"

and parseAutoFillOption (state:JamesonOption) (argument:list<string>):Result<JamesonOption*list<string>,list<JamesonFail>> =
    parseBooleanOption state argument "autoFill"
    
let parse (arguments:string[]):Result<JamesonOption,list<JamesonFail>> =
    parse_ JamesonOptions.OptionDefault <| Array.toList arguments
