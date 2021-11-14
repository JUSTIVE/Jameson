module ArgumentParserTest
open State
open Result
open TestResult
open Expect
open ArgumentParser
open JamesonOption
open JamesonResults


let Test_Parse_None:UnitTestState = 
    parse [||]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = false
        help = false
        autoFill = false
        checkConvention = CheckConventionType.NoConvention
    })

let Test_Parse_Strict:UnitTestState = 
    parse [|"--s"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = true
        verbose = false
        help = false
        autoFill = false
        checkConvention = CheckConventionType.NoConvention
    })

let Test_Parse_Verbose:UnitTestState = 
    parse [|"--v"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = true
        help = false
        autoFill = false
        checkConvention = CheckConventionType.NoConvention
    })

let Test_Parse_Help:UnitTestState = 
    parse [|"--h"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = false
        help = true
        autoFill = false
        checkConvention = CheckConventionType.NoConvention
    })

let Test_Parse_AutoFill:UnitTestState = 
    parse [|"--f"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = false
        help = false
        autoFill = true
        checkConvention = CheckConventionType.NoConvention
    })

let Test_Parse_CheckConvention_Camel:UnitTestState = 
    parse [|"-c";"camel"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = false
        help = false
        autoFill = false
        checkConvention = (HeadCharConventionType CamelCase)
    })

let Test_Parse_CheckConvention_Pascal:UnitTestState = 
    parse [|"-c";"pascal"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = false
        help = false
        autoFill = false
        checkConvention = (HeadCharConventionType PascalCase)
    })

let Test_Parse_CheckConvention_Upper:UnitTestState = 
    parse [|"-c";"upper"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = false
        help = false
        autoFill = false
        checkConvention = (SimpleConventionType UpperCase)
    })

let Test_Parse_CheckConvention_Lower:UnitTestState = 
    parse [|"-c";"lower"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = false
        help = false
        autoFill = false
        checkConvention = (SimpleConventionType LowerCase)
    })

let Test_Parse_CheckConvention_Snake:UnitTestState = 
    parse [|"-c";"snake"|]
    |>expect (Success {
        runnerType = RunnerTypeOption.None
        writeToFile = Option.None
        strict = false
        verbose = false
        help = false
        autoFill = false
        checkConvention = (ComplexConventionType SnakeCase)
    })

let Test_Parse_CheckConvention_insufficient_arguemnt:UnitTestState = 
    parse [|"-c"|]
    |>expect (Fail [INSUFFICIENT_PATH_ARGUMENT])

let Test_Parse_CheckConvention_invalid_argument:UnitTestState = 
    parse [|"-c";""|]
    |>expect (Fail [INVALID_ARGUMENT ""])

let Test:ModuleTest =
    let result = 
        [
            ("Test_ParseNone",Test_Parse_None);
            ("Test_ParseStrict",Test_Parse_Strict);
            ("Test_ParseVerbose",Test_Parse_Verbose);
            ("Test_ParseHelp",Test_Parse_Help);
            ("Test_ParseAutoFill",Test_Parse_AutoFill);
            ("Test_Parse_CheckConvention_Camel",Test_Parse_CheckConvention_Camel);
            ("Test_Parse_CheckConvention_Pascal",Test_Parse_CheckConvention_Pascal);
            ("Test_Parse_CheckConvention_Upper",Test_Parse_CheckConvention_Upper);
            ("Test_Parse_CheckConvention_Lower",Test_Parse_CheckConvention_Lower)
            ("Test_Parse_CheckConvention_insufficient_arguemnt",Test_Parse_CheckConvention_insufficient_arguemnt);
            ("Test_Parse_CheckConvention_invalid_argument",Test_Parse_CheckConvention_invalid_argument)
        ]
        |>List.rev
        |>List.fold JoinResult (ModuleResultState.Init("ArgumentParserTest"))
    match result with
    | NonInit x -> x
    | __ -> ModuleTest("INVALID_TEST",ModuleFail,List.empty<UnitTestResult>)
