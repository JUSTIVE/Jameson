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

let NO_OUTPUT_DIRECTORY_ARGUMENT:JamesonResult = {
    message = "output directory not specified";
    errorCode = 1
}

let FILE_NOT_FOUND (targetFile:string):JamesonResult ={
    message = $"file not found : {targetFile}";
    errorCode = 1
}