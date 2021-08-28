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
    message = "";
    errorCode = 1
}