module JamesonOptions
open JamesonOption

let OPTION_DEFAULT (runnerType:RunnerType):JamesonOption = {
    runnerType=runnerType;
    writeToFile=false
}

let OptionList:list<JamesonOption.ArgumentOption> = [
    {
        name="WriteToFile";
        keyString="o";
        argumentLength=1;
    };
]