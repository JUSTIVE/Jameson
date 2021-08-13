module JamesonResults
open JamesonResult

let CLEAR:JamesonResult = {
    message = "good to go";
    errorCode = 0
}

let ARGUMENT_LENGTH_ERROR:JamesonResult = {
    message = "argument should be one or two";
    errorCode = 1
}