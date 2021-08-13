module JamesonOption

type GeneralRunner = {
    sourcePath:string
    targetCandidate:list<string>
}

type TargetRunner = {
    sourcePath:string;
    targetPath:string
}

type RunnerType = 
    | GeneralRunner of GeneralRunner
    | TargetRunner of TargetRunner

type JamesonOption = {
    runnerType:RunnerType
    writeToFile:bool
}