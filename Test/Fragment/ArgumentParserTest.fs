module ArgumentParserTest
open TestResult

let Test_joinKey :TestState = 
    Success

let Test_keySet:TestState = 
    Success

let Test:ModuleResult = 
    [Test_joinKey;Test_keySet]
    |>List.reduce(JoinResult)
