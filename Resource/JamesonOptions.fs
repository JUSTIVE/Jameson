module JamesonOptions
open JamesonOption

let DEFAULT (runnerType:RunnerType):JamesonOption = {
    runnerType=runnerType;
    writeToFile=false
}