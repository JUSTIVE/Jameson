module NameChangerTest
open JamesonOption
open TestResult
open Expect
open NameChanger


let camelTest = setNameConvention CamelCase
let Test_CamelCase_Upper:UnitTestState =
    camelTest "CAMELIZETHIS" |> expect "cAMELIZETHIS"
let Test_CamelCase_Lower:UnitTestState = 
    camelTest "camelizethis" |> expect "camelizethis"
let Test_CamelCase_Camel:UnitTestState = 
    camelTest "camelizeThis" |> expect "camelizeThis"
let Test_CamelCase_Pascal:UnitTestState = 
    camelTest "CamelizeThis" |> expect "camelizeThis"

let pascalTest = setNameConvention PascalCase
let Test_PascalCase_Upper:UnitTestState =
    pascalTest "PASCALIZETHIS" |> expect "PASCALIZETHIS"
let Test_PascalCase_Lower:UnitTestState = 
    pascalTest "pascalizethis" |> expect "Pascalizethis"
let Test_PascalCase_Camel:UnitTestState = 
    pascalTest "pascalizeThis" |> expect "PascalizeThis"
let Test_PascalCase_Pascal:UnitTestState = 
    pascalTest "PascalizeThis"|> expect "PascalizeThis"


let Test:ModuleTest = 
    let result =
        [
            ("Test_CamelCase_Upper",Test_CamelCase_Upper);
            ("Test_CamelCase_Lower",Test_CamelCase_Lower);
            ("Test_CamelCase_Camel",Test_CamelCase_Camel);
            ("Test_CamelCase_Pascal",Test_CamelCase_Pascal);

            ("Test_PascalCase_Upper",Test_PascalCase_Upper);
            ("Test_PascalCase_Lower",Test_PascalCase_Lower);
            ("Test_PascalCase_Camel",Test_PascalCase_Pascal);
            ("Test_PascalCase_Pascal",Test_PascalCase_Pascal);
        ]
        |>List.rev
        |>List.fold JoinResult (ModuleResultState.Init("NameChangerTest"))
    match result with
    | NonInit x -> x
    | __ -> ModuleTest("INVALID_TEST",ModuleFail,List.empty)