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

type JamesonOption = {
    runnerType:RunnerTypeOption
    writeToFile:bool
}

type ArgumentOption = {
    name:string;
    keyString:string;
    argumentLength:int;
}