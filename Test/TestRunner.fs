module TestRunner
open TestResult
open TestPrinter

let runTest argv:int =
    match argv with
    | h::t->
        match h with 
        | "all" ->
            [
                ArgumentParserTest.Test;
                NameChangerTest.Test
            ]
            |>List.map (fun x-> PrintModuleResult x)
            |>List.fold (+) 0
        | x ->
            match x with
            | "argumentParser" ->   ArgumentParserTest.Test
            | "nameChanger" ->      NameChangerTest.Test
            | __ ->                 ModuleTest("INVALID_TEST",ModuleFail,List.empty<UnitTestResult>)
            |> PrintModuleResult
    | [] ->  PrintModuleResult <|ModuleTest("INVALID_TEST",ModuleFail,List.empty<UnitTestResult>)

    