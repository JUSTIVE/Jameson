module JamesonOptions
open JamesonOption

let OPTION_DEFAULT (runnerType:RunnerType):JamesonOption = {
    runnerType=runnerType;
    writeToFile=false
}