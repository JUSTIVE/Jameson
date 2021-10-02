module NameChangerTest
open JamesonOption
open TestResult
open Expect
open NameChanger

let stringTestRunner action input expectations = action input |> expect expectations

let singleStringTestGen convention  = stringTestRunner (setNameConvention convention)

let camelSingleStringTest = singleStringTestGen (HeadCharConventionType CamelCase)
let Test_SingleStringCamelCase_Upper = camelSingleStringTest "CAMELIZETHIS" "cAMELIZETHIS"
let Test_SingleStringCamelCase_Lower = camelSingleStringTest "camelizethis" "camelizethis"
let Test_SingleStringCamelCase_Camel = camelSingleStringTest "camelizeThis" "camelizeThis"
let Test_SingleStringCamelCase_Pascal= camelSingleStringTest "CamelizeThis" "camelizeThis"

let pascalSingleStringTest = singleStringTestGen (HeadCharConventionType PascalCase)
let Test_SingleStringPascalCase_Upper = pascalSingleStringTest "PASCALIZETHIS" "PASCALIZETHIS"
let Test_SingleStringPascalCase_Lower = pascalSingleStringTest "pascalizethis" "Pascalizethis"
let Test_SingleStringPascalCase_Camel = pascalSingleStringTest "pascalizeThis" "PascalizeThis"
let Test_SingleStringPascalCase_Pascal= pascalSingleStringTest "PascalizeThis" "PascalizeThis"

let upperSingleStringTest = singleStringTestGen (SimpleConventionType UpperCase)
let Test_SingleStringUpperCase_Upper = upperSingleStringTest "UPPERTHIS" "UPPERTHIS"
let Test_SingleStringUpperCase_Lower = upperSingleStringTest "upperthis" "UPPERTHIS"
let Test_SingleStringUpperCase_Camel = upperSingleStringTest "upperThis" "UPPERTHIS"
let Test_SingleStringUpperCase_Pascal= upperSingleStringTest "UpperThis" "UPPERTHIS"

let lowerSingleStringTest = singleStringTestGen (SimpleConventionType LowerCase)
let Test_SingleStringLowerCase_Upper = lowerSingleStringTest "LOWERTHIS" "lowerthis"
let Test_SingleStringLowerCase_Lower = lowerSingleStringTest "lowerthis" "lowerthis"
let Test_SingleStringLowerCase_Camel = lowerSingleStringTest "lowerThis" "lowerthis"
let Test_SingleStringLowerCase_Pascal= lowerSingleStringTest "LowerThis" "lowerthis"

let pathTestGen convention = stringTestRunner (pathConvention convention)

let camelPathTest = pathTestGen (HeadCharConventionType CamelCase)
let Test_PathCamelCase_Upper = camelPathTest "MATCHTHIS:A:SOMETHINGTHIS:B" "mATCHTHIS:a:sOMETHINGTHIS:b"
let Test_PathCamelCase_Lower = camelPathTest "matchthis:a:somethingthis:b" "matchthis:a:somethingthis:b"
let Test_PathCamelCase_Camel = camelPathTest "matchThis:a:somethingThis:b" "matchThis:a:somethingThis:b"
let Test_PathCamelCase_Pascal= camelPathTest "MatchThis:A:SomethingThis:B" "matchThis:a:somethingThis:b"

let Test:ModuleTest = 
    let result =
        [
            ("Test_SingleStringCamelCase_Upper ",Test_SingleStringCamelCase_Upper );
            ("Test_SingleStringCamelCase_Lower ",Test_SingleStringCamelCase_Lower );
            ("Test_SingleStringCamelCase_Camel ",Test_SingleStringCamelCase_Camel );
            ("Test_SingleStringCamelCase_Pascal",Test_SingleStringCamelCase_Pascal);

            ("Test_SingleStringPascalCase_Upper ",Test_SingleStringPascalCase_Upper );
            ("Test_SingleStringPascalCase_Lower ",Test_SingleStringPascalCase_Lower );
            ("Test_SingleStringPascalCase_Camel ",Test_SingleStringPascalCase_Camel );
            ("Test_SingleStringPascalCase_Pascal",Test_SingleStringPascalCase_Pascal);

            ("Test_SingleStringUpperCase_Upper ",Test_SingleStringUpperCase_Upper );
            ("Test_SingleStringUpperCase_Lower ",Test_SingleStringUpperCase_Lower );
            ("Test_SingleStringUpperCase_Camel ",Test_SingleStringUpperCase_Camel );
            ("Test_SingleStringUpperCase_Pascal",Test_SingleStringUpperCase_Pascal);

            ("Test_SingleStringLowerCase_Upper ",Test_SingleStringLowerCase_Upper );
            ("Test_SingleStringLowerCase_Lower ",Test_SingleStringLowerCase_Lower );
            ("Test_SingleStringLowerCase_Camel ",Test_SingleStringLowerCase_Camel );
            ("Test_SingleStringLowerCase_Pascal",Test_SingleStringLowerCase_Pascal);

            ("Test_PathCamelCase_Upper" ,Test_PathCamelCase_Upper);
            ("Test_PathCamelCase_Lower" ,Test_PathCamelCase_Lower);
            ("Test_PathCamelCase_Camel" ,Test_PathCamelCase_Camel);
            ("Test_PathCamelCase_Pascal",Test_PathCamelCase_Pascal);
        ]
        |>List.rev
        |>List.fold JoinResult (ModuleResultState.Init("NameChangerTest"))
    match result with
    | NonInit x -> x
    | __ -> ModuleTest("INVALID_TEST",ModuleFail,List.empty)