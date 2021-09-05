module TestPrinter
open TestResult 

let PrintResult (x:UnitTestState):Unit = 
    match x with 
    | Success -> "Success"
    | Fail(reason:string) -> $"Fail with {reason}"
    |> printf "%s"

let PrintModuleResult (x:ModuleTest):Unit =
    match x with
    | (moduleName,result,unitTestResults)-> 
        let badge:string=   
            match result with
            | ModuleSuccess-> "✔️"
            | ModuleFail-> "❌"
        printfn $"Module::{moduleName} {badge}" 
