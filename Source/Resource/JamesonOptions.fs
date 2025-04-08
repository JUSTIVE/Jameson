module JamesonOptions
open JamesonOption

let OptionDefault :JamesonOption.t= {
    runnerType=None
    writeToFile=Option.None
    strict = false
    verbose=false
    help=false
    autoFill = false
    checkConvention = NoConvention
}

let OptionList:list<JamesonOption.ArgumentOption> = [
    {
        name="write to file";
        keyString="o";
        argumentLength=0;
    };
    {
        name="verbose log";
        keyString="v";
        argumentLength=0;
    }
]