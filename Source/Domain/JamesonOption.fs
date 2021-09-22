module JamesonOption
open JamesonResult
//string(path)->JsonValue->Set<string>->list<diffline>



type FileArgument = {
    filename:string;
    path:string
}

type GeneralRunnerOption = {
    source:FileArgument
    targetCandidate:list<FileArgument>
}

type TargetRunnerOption = {
    source:FileArgument;
    target:FileArgument
}

type RunnerTypeOption = 
    | GeneralRunnerOption of GeneralRunnerOption
    | TargetRunnerOption of TargetRunnerOption
    | None

type JamesonOption = {
    runnerType:RunnerTypeOption
    writeToFile:option<string>
    strict:bool
    verbose:bool
    help:bool
}

let JamesonOptionSetRunnerTypeLens state runnerTypeOption :JamesonOption=
    {
        runnerType = runnerTypeOption;
        writeToFile = state.writeToFile;
        strict = state.strict;
        verbose = state.verbose
        help = state.help
    } 

let JamesonOptionSetBoolFlag state key:JamesonOption=
    match key with
    | "verbose" ->
        {
               runnerType = state.runnerType;
               writeToFile = state.writeToFile;
               strict = state.strict;
               verbose = true
               help = state.help
           }
    | "help" -> 
        {
            runnerType = state.runnerType;
            writeToFile = state.writeToFile;
            verbose = state.verbose
            strict = state.strict;
            help = true
        }
    | "strict" ->
        {
            runnerType = state.runnerType;
            writeToFile = state.writeToFile;
            verbose = state.verbose
            strict = true;
            help = state.help
        }
    | __ -> state

type ArgumentOption = {
    name:string;
    keyString:string;
    argumentLength:int;
}