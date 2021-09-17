module JamesonOptions
open JamesonOption

let OPTION_DEFAULT (runnerType:RunnerTypeOption):JamesonOption = {
    runnerType=runnerType;
    writeToFile=false
    verbose=false
}

let OptionList:list<JamesonOption.ArgumentOption> = [
    {
        name="WriteToFile";
        keyString="o";
        argumentLength=1;
    };
]