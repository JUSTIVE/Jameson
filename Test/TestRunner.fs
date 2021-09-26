module TestRunner
open TestResult
open TestPrinter

let runTest argv:int =
    match argv with
    | h::t->
        match h with 
        | "all"
        | "argumentParser" -> 
            ArgumentParserTest.Test
        | __ -> 
            ModuleTest("INVALID_TEST",ModuleFail,List.empty<UnitTestResult>)
    | [] -> 
        ModuleTest("INVALID_TEST",ModuleFail,List.empty<UnitTestResult>)
    |> PrintModuleResult
    