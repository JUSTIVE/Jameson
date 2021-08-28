module ArgumentParserTest
open TestResult

let Test_joinKey:UnitTestState = 
    Success

let Test_keySet:UnitTestState = 
    Success

let Test:ModuleResultState =
    [
        ("Test_joinkKey",Test_joinKey);
        ("Test_keySet",Test_keySet)
    ]
    |>List.fold JoinResult (ModuleResultState.Init("ArgumentParserTest"))
    
