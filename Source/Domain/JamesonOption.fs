﻿module JamesonOption
open FileType
open JamesonResult
//string(path)->JsonValue->Set<string>->list<diffline>


type GeneralRunnerOption = {
    source:FileArgument
    targetList:list<FileArgument>
}

type TargetRunnerOption = {
    source:FileArgument
    target:FileArgument
}

type ShowRunnerOption = {
    source:FileArgument;
}

type RunnerTypeOption = 
    | GeneralRunnerOption of GeneralRunnerOption
    | TargetRunnerOption of TargetRunnerOption
    | ShowRunnerOption of ShowRunnerOption
    | None

type SimpleConventionType =
    | LowerCase 
    | UpperCase

type HeadCharConventionType =
    | PascalCase
    | CamelCase

type ComplexConventionType =
    | SnakeCase

type CheckConventionType =
    | NoConvention 
    | SimpleConventionType of SimpleConventionType
    | HeadCharConventionType of HeadCharConventionType
    | ComplexConventionType of ComplexConventionType

module JamesonOption = 
    type t = {
        runnerType:RunnerTypeOption
        writeToFile:option<string>
        strict:bool
        verbose:bool
        help:bool
        autoFill:bool
        checkConvention:CheckConventionType
    }

    let setRunnerTypeLens runnerTypeOption state :t=
        {
            runnerType = runnerTypeOption;
            writeToFile = state.writeToFile;
            strict = state.strict;
            verbose = state.verbose
            help = state.help
            autoFill = state.autoFill
            checkConvention = state.checkConvention
        } 

    let setCheckConventionLens  checkConvention state : t=
        {
            runnerType = state.runnerType;
            writeToFile = state.writeToFile;
            strict = state.strict;
            verbose = state.verbose
            help = state.help
            autoFill = state.autoFill
            checkConvention = checkConvention
        } 

    let setBoolFlag  key state:t=
        match key with
        | "verbose" ->
            { 
                runnerType = state.runnerType;
                writeToFile = state.writeToFile;
                strict = state.strict;
                verbose = true
                help = state.help
                autoFill = state.autoFill
                checkConvention = state.checkConvention
            }
        | "help" -> 
            {
                runnerType = state.runnerType;
                writeToFile = state.writeToFile;
                verbose = state.verbose
                strict = state.strict
                help = true
                autoFill = state.autoFill
                checkConvention = state.checkConvention
            }
        | "strict" ->
            {
                runnerType = state.runnerType;
                writeToFile = state.writeToFile;
                verbose = state.verbose
                strict = true;
                help = state.help
                autoFill = state.autoFill
                checkConvention = state.checkConvention
            }
        | "autoFill" ->
            {
                runnerType = state.runnerType;
                writeToFile = state.writeToFile;
                verbose = state.verbose
                strict = state.strict;
                help = state.help
                autoFill = true
                checkConvention = state.checkConvention
            }
        | __ -> state

type ArgumentOption = {
    name:string;
    keyString:string;
    argumentLength:int;
}