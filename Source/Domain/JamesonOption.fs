module JamesonOption

//string(path)->JsonValue->Set<string>->list<diffline>

type path = string

type GeneralRunnerOption = {
    sourcePath:string
    targetCandidate:list<string>
}

type TargetRunnerOption = {
    sourcePath:string;
    targetPath:string
}

type RunnerTypeOption = 
    | GeneralRunnerOption of GeneralRunnerOption
    | TargetRunnerOption of TargetRunnerOption
    | None

type JamesonOption = {
    runnerType:RunnerTypeOption
    writeToFile:option<string>
    verbose:bool
    help:bool
}

let JamesonOptionSetRunnerTypeLens state runnerTypeOption :JamesonOption=
    {
        runnerType = runnerTypeOption;
        writeToFile = state.writeToFile;
        verbose = state.verbose
        help = state.help
    } 

let JamesonOptionSetBoolFlag state key:JamesonOption=
    match key with
    | "verbose" -> {
               runnerType = state.runnerType;
               writeToFile = state.writeToFile;
               verbose = true
               help = state.help
           }
    | "help" -> 
        {
            runnerType = state.runnerType;
            writeToFile = state.writeToFile;
            verbose = state.verbose
            help = true
        }

type ArgumentOption = {
    name:string;
    keyString:string;
    argumentLength:int;
}