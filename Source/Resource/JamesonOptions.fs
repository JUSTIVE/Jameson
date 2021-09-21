module JamesonOptions
open JamesonOption

let OPTION_DEFAULT (runnerType:RunnerTypeOption):JamesonOption = {
    runnerType=runnerType;
    writeToFile=Option.None
    verbose=false
    help=false
}

let OptionDefault :JamesonOption= {
    runnerType=None
    writeToFile=Option.None
    verbose=false
    help=false
}


let OptionList:list<JamesonOption.ArgumentOption> = [
    {
        name="WriteToFile";
        keyString="o";
        argumentLength=1;
    };
]