module JamesonResults
open JamesonResult

let GOOD:JamesonResult = {
    message = "good to go";
    errorCode = 0
}

let ARGUMENT_LENGTH_ERROR:JamesonResult = {
    message = "argument should be one or two";
    errorCode = 1
}

let INSUFFICIENT_PATH_ARGUMENT_GENERALRUNNER:JamesonResult={
    message = "insufficient path argument given";
    errorCode = 1
}

let INSUFFICIENT_PATH_ARGUMENT_TARGETRUNNER:JamesonResult ={
    message = "insufficient path argument given";
    errorCode = 1
}

let ARGUMENT_ERROR:JamesonResult ={
    message = "invalid argument";
    errorCode = 1
}

let INVALID_ARGUMENT (option:string) :JamesonResult ={
    message = $"invalid option : {option}";
    errorCode = 1
}

let NO_OUTPUT_DIRECTORY_ARGUMENT:JamesonResult = {
    message = "output directory not specified";
    errorCode = 1
}

let FILE_NOT_FOUND (targetFile:string):JamesonResult ={
    message = $"file not found : {targetFile}";
    errorCode = 1
}

let INVALID_COMPARE_TARGET (x:string) (y:string):JamesonResult ={
    message = $"invalid comparing targets : {x}, {y}";
    errorCode = 1
}

let INVALID_KEYSET:JamesonResult = {
    message = $"invalid keyset combination";
    errorCode = 1
}

let INVALID_PATH_TYPE path:JamesonResult = {
    message = $"invalid path argument : {path}";
    errorCode = 1
}