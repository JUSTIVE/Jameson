module NameChangerTest
open JamesonOption
open TestResult
open Expect
open NameChanger

let testRunner action input expectations = action input |> expect expectations
let testGen convention  = testRunner (setNameConvention convention)

let camelTest = testGen CamelCase
let Test_CamelCase_Upper = camelTest "CAMELIZETHIS" "cAMELIZETHIS"
let Test_CamelCase_Lower = camelTest "camelizethis" "camelizethis"
let Test_CamelCase_Camel = camelTest "camelizeThis" "camelizeThis"
let Test_CamelCase_Pascal= camelTest "CamelizeThis" "camelizeThis"

let pascalTest = testGen PascalCase
let Test_PascalCase_Upper = pascalTest "PASCALIZETHIS" "PASCALIZETHIS"
let Test_PascalCase_Lower = pascalTest "pascalizethis" "Pascalizethis"
let Test_PascalCase_Camel = pascalTest "pascalizeThis" "PascalizeThis"
let Test_PascalCase_Pascal= pascalTest "PascalizeThis" "PascalizeThis"

let upperTest = testGen UpperCase
let Test_UpperCase_Upper = upperTest "UPPERTHIS" "UPPERTHIS"
let Test_UpperCase_Lower = upperTest "upperthis" "UPPERTHIS"
let Test_UpperCase_Camel = upperTest "upperThis" "UPPERTHIS"
let Test_UpperCase_Pascal= upperTest "UpperThis" "UPPERTHIS"


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

            ("Test_UpperCase_Upper",Test_UpperCase_Upper);
            ("Test_UpperCase_Lower",Test_UpperCase_Lower);
            ("Test_UpperCase_Camel",Test_UpperCase_Upper);
            ("Test_UpperCase_Pascal",Test_UpperCase_Pascal);
        ]
        |>List.rev
        |>List.fold JoinResult (ModuleResultState.Init("NameChangerTest"))
    match result with
    | NonInit x -> x
    | __ -> ModuleTest("INVALID_TEST",ModuleFail,List.empty)