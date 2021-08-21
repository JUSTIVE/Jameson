module Printer
open TestResult 

let PrintResult (x:TestState):Unit = 
    match x with 
    | Success -> "Success"
    | Fail(reason:string) -> $"Fail with {reason}"
    |> printf "%s"

let PrintModuleResult (x:ModuleTest):Unit =
    match x with
    | (moduleName,result)-> 
        let badge:string=   
            match result with
            | ModuleSuccess-> "✔️"
            | ModuleFail-> "❌"
        printfn $"Module::{moduleName} {badge}" 
