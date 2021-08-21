module Printer
open JamesonResult

let print (jamesonReult:JamesonResult):int = 
    printfn "%s" jamesonReult.message
    jamesonReult.errorCode