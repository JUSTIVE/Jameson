module Printer
open JamesonResult

open JamesonOption
open Stringify


let print (jamesonReult:JamesonResult):int = 
    printfn "%s" jamesonReult.message
    jamesonReult.errorCode

let printWithIndent (indent:int32) (content:string) =
    let rec multiplyString (state:string) (times:int32) (content:string) =
        match times with
        | 0 -> state
        | __ -> multiplyString (state+content) (times - 1) content
    let tabs = multiplyString "" indent "\t"
    printfn "%s%s" tabs content

let printJamesonOption (indent:int32) (jamesonOption:JamesonOption):Unit =
    let printRunnerType (indent:int32) (runnerType:RunnerType)=
        sprintf "RunnerType : %s" (stringifyRunnerType runnerType)
        |> printWithIndent indent
    
    printRunnerType indent jamesonOption.runnerType
    sprintf "Write to file : %s" (stringifyBool jamesonOption.writeToFile)
    |> printWithIndent indent
    
        

        