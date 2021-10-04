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
let Test_SingleStringCamelCase_Snake = camelSingleStringTest "camelize_this" "camelizeThis"

let pascalSingleStringTest = singleStringTestGen (HeadCharConventionType PascalCase)
let Test_SingleStringPascalCase_Upper = pascalSingleStringTest "PASCALIZETHIS" "PASCALIZETHIS"
let Test_SingleStringPascalCase_Lower = pascalSingleStringTest "pascalizethis" "Pascalizethis"
let Test_SingleStringPascalCase_Camel = pascalSingleStringTest "pascalizeThis" "PascalizeThis"
let Test_SingleStringPascalCase_Pascal= pascalSingleStringTest "PascalizeThis" "PascalizeThis"
let Test_SingleStringPascalCase_Snake = pascalSingleStringTest "pascalize_this""PascalizeThis"

let upperSingleStringTest = singleStringTestGen (SimpleConventionType UpperCase)
let Test_SingleStringUpperCase_Upper = upperSingleStringTest "UPPERTHIS" "UPPERTHIS"
let Test_SingleStringUpperCase_Lower = upperSingleStringTest "upperthis" "UPPERTHIS"
let Test_SingleStringUpperCase_Camel = upperSingleStringTest "upperThis" "UPPERTHIS"
let Test_SingleStringUpperCase_Pascal= upperSingleStringTest "UpperThis" "UPPERTHIS"
let Test_SingleStringUpperCase_Snake = upperSingleStringTest "upper_this" "UPPERTHIS"

let lowerSingleStringTest = singleStringTestGen (SimpleConventionType LowerCase)
let Test_SingleStringLowerCase_Upper = lowerSingleStringTest "LOWERTHIS" "lowerthis"
let Test_SingleStringLowerCase_Lower = lowerSingleStringTest "lowerthis" "lowerthis"
let Test_SingleStringLowerCase_Camel = lowerSingleStringTest "lowerThis" "lowerthis"
let Test_SingleStringLowerCase_Pascal= lowerSingleStringTest "LowerThis" "lowerthis"
let Test_SingleStringLowerCase_Snake = lowerSingleStringTest "lower_this" "lowerthis"

let snakeSingleStringTest = singleStringTestGen (ComplexConventionType SnakeCase)
let Test_SingleStringSnakeCase_Upper = snakeSingleStringTest "SNAKETHIS" "s_n_a_k_e_t_h_i_s"
let Test_SingleStringSnakeCase_Lower = snakeSingleStringTest "snakethis" "snakethis"
let Test_SingleStringSnakeCase_Camel = snakeSingleStringTest "snakeThis" "snake_this"
let Test_SingleStringSnakeCase_Pascal= snakeSingleStringTest "SnakeThis" "snake_this"
let Test_SingleStringSnakeCase_Snake = snakeSingleStringTest "snake_this" "snake_this"

let pathTestGen convention = stringTestRunner (pathConvention convention)

let testUpper  runner = runner "MATCHTHIS:A:SOMETHINGTHIS:B"
let testLower  runner = runner "matchthis:a:somethingthis:b"
let testCamel  runner = runner "matchThis:a:somethingThis:b"
let testPascal runner = runner "MatchThis:A:SomethingThis:B"
let testSnake  runner = runner "match_this:a:something_this:b"

let camelPathTest = pathTestGen (HeadCharConventionType CamelCase)
let Test_PathCamelCase_Upper = testUpper  camelPathTest "mATCHTHIS:a:sOMETHINGTHIS:b"
let Test_PathCamelCase_Lower = testLower  camelPathTest "matchthis:a:somethingthis:b"
let Test_PathCamelCase_Camel = testCamel  camelPathTest "matchThis:a:somethingThis:b"
let Test_PathCamelCase_Pascal= testPascal camelPathTest "matchThis:a:somethingThis:b"
let Test_PathCamelCase_Snake = testSnake  camelPathTest "matchThis:a:somethingThis:b"

let pascalPathTest = pathTestGen (HeadCharConventionType PascalCase)
let Test_PathPascalCase_Upper = testUpper  pascalPathTest "MATCHTHIS:A:SOMETHINGTHIS:B"
let Test_PathPascalCase_Lower = testLower  pascalPathTest "Matchthis:A:Somethingthis:B"
let Test_PathPascalCase_Camel = testCamel  pascalPathTest "MatchThis:A:SomethingThis:B"
let Test_PathPascalCase_Pascal= testPascal pascalPathTest "MatchThis:A:SomethingThis:B"
let Test_PathPascalCase_Snake = testSnake  pascalPathTest "MatchThis:A:SomethingThis:B"

let lowerPathTest = pathTestGen (SimpleConventionType LowerCase)
let Test_PathLowerCase_Upper = testUpper  lowerPathTest "matchthis:a:somethingthis:b"
let Test_PathLowerCase_Lower = testLower  lowerPathTest "matchthis:a:somethingthis:b"
let Test_PathLowerCase_Camel = testCamel  lowerPathTest "matchthis:a:somethingthis:b"
let Test_PathLowerCase_Pascal= testPascal lowerPathTest "matchthis:a:somethingthis:b"
let Test_PathLowerCase_Snake = testSnake  lowerPathTest "matchthis:a:somethingthis:b"

let upperPathTest = pathTestGen (SimpleConventionType UpperCase)
let Test_PathUpperCase_Upper = testUpper  upperPathTest "MATCHTHIS:A:SOMETHINGTHIS:B"
let Test_PathUpperCase_Lower = testLower  upperPathTest "MATCHTHIS:A:SOMETHINGTHIS:B"
let Test_PathUpperCase_Camel = testCamel  upperPathTest "MATCHTHIS:A:SOMETHINGTHIS:B"
let Test_PathUpperCase_Pascal= testPascal upperPathTest "MATCHTHIS:A:SOMETHINGTHIS:B"
let Test_PathUpperCase_Snake = testSnake  upperPathTest "MATCHTHIS:A:SOMETHINGTHIS:B"

let snakePathTest = pathTestGen (ComplexConventionType SnakeCase)
let Test_PathSnakeCase_Upper = testUpper  snakePathTest "m_a_t_c_h_t_h_i_s:a:s_o_m_e_t_h_i_n_g_t_h_i_s:b"
let Test_PathSnakeCase_Lower = testLower  snakePathTest "matchthis:a:somethingthis:b"
let Test_PathSnakeCase_Camel = testCamel  snakePathTest "match_this:a:something_this:b"
let Test_PathSnakeCase_Pascal= testPascal snakePathTest "match_this:a:something_this:b"
let Test_PathSnakeCase_Snake = testSnake  snakePathTest "match_this:a:something_this:b"

let Test:ModuleTest = 
    let result =
        [
            ("Test_SingleStringCamelCase_Upper ",Test_SingleStringCamelCase_Upper );
            ("Test_SingleStringCamelCase_Lower ",Test_SingleStringCamelCase_Lower );
            ("Test_SingleStringCamelCase_Camel ",Test_SingleStringCamelCase_Camel );
            ("Test_SingleStringCamelCase_Pascal",Test_SingleStringCamelCase_Pascal);
            ("Test_SingleStringCamelCase_Snake" ,Test_SingleStringCamelCase_Snake );

            ("Test_SingleStringPascalCase_Upper ",Test_SingleStringPascalCase_Upper );
            ("Test_SingleStringPascalCase_Lower ",Test_SingleStringPascalCase_Lower );
            ("Test_SingleStringPascalCase_Camel ",Test_SingleStringPascalCase_Camel );
            ("Test_SingleStringPascalCase_Pascal",Test_SingleStringPascalCase_Pascal);
            ("Test_SingleStringPascalCase_Snake" ,Test_SingleStringPascalCase_Snake);

            ("Test_SingleStringUpperCase_Upper ",Test_SingleStringUpperCase_Upper );
            ("Test_SingleStringUpperCase_Lower ",Test_SingleStringUpperCase_Lower );
            ("Test_SingleStringUpperCase_Camel ",Test_SingleStringUpperCase_Camel );
            ("Test_SingleStringUpperCase_Pascal",Test_SingleStringUpperCase_Pascal);
            ("Test_SingleStringUpperCase_Snake" ,Test_SingleStringUpperCase_Snake);

            ("Test_SingleStringLowerCase_Upper ",Test_SingleStringLowerCase_Upper );
            ("Test_SingleStringLowerCase_Lower ",Test_SingleStringLowerCase_Lower );
            ("Test_SingleStringLowerCase_Camel ",Test_SingleStringLowerCase_Camel );
            ("Test_SingleStringLowerCase_Pascal",Test_SingleStringLowerCase_Pascal);
            ("Test_SingleStringLowerCase_Snake" ,Test_SingleStringLowerCase_Snake);

            ("Test_SingleStringSnakeCase_Upper ",Test_SingleStringSnakeCase_Upper );
            ("Test_SingleStringSnakeCase_Lower ",Test_SingleStringSnakeCase_Lower );
            ("Test_SingleStringSnakeCase_Camel ",Test_SingleStringSnakeCase_Camel );
            ("Test_SingleStringSnakeCase_Pascal",Test_SingleStringSnakeCase_Pascal);
            ("Test_SingleStringSnakeCase_Snake ",Test_SingleStringSnakeCase_Snake );

            ("Test_PathCamelCase_Upper" ,Test_PathCamelCase_Upper);
            ("Test_PathCamelCase_Lower" ,Test_PathCamelCase_Lower);
            ("Test_PathCamelCase_Camel" ,Test_PathCamelCase_Camel);
            ("Test_PathCamelCase_Pascal",Test_PathCamelCase_Pascal);
            ("Test_PathCamelCase_Snake" ,Test_PathCamelCase_Snake);

            ("Test_PathPascalCase_Upper ",Test_PathPascalCase_Upper );
            ("Test_PathPascalCase_Lower ",Test_PathPascalCase_Lower );
            ("Test_PathPascalCase_Camel ",Test_PathPascalCase_Camel );
            ("Test_PathPascalCase_Pascal",Test_PathPascalCase_Pascal);
            ("Test_PathPascalCase_Snake" ,Test_PathPascalCase_Snake);

            ("Test_PathLowerCase_Upper ",Test_PathLowerCase_Upper );
            ("Test_PathLowerCase_Lower ",Test_PathLowerCase_Lower );
            ("Test_PathLowerCase_Camel ",Test_PathLowerCase_Camel );
            ("Test_PathLowerCase_Pascal",Test_PathLowerCase_Pascal);
            ("Test_PathLowerCase_Snake ",Test_PathLowerCase_Snake );

            ("Test_PathUpperCase_Upper ",Test_PathUpperCase_Upper );
            ("Test_PathUpperCase_Lower ",Test_PathUpperCase_Lower );
            ("Test_PathUpperCase_Camel ",Test_PathUpperCase_Camel );
            ("Test_PathUpperCase_Pascal",Test_PathUpperCase_Pascal);
            ("Test_PathUpperCase_Snake ",Test_PathUpperCase_Snake );

            ("Test_PathSnakeCase_Upper ",Test_PathSnakeCase_Upper );
            ("Test_PathSnakeCase_Lower ",Test_PathSnakeCase_Lower );
            ("Test_PathSnakeCase_Camel ",Test_PathSnakeCase_Camel );
            ("Test_PathSnakeCase_Pascal",Test_PathSnakeCase_Pascal);
            ("Test_PathSnakeCase_Snake ",Test_PathSnakeCase_Snake );
        ]
        |>List.rev
        |>List.fold JoinResult (ModuleResultState.Init("NameChangerTest"))
    match result with
    | NonInit x -> x
    | __ -> ModuleTest("INVALID_TEST",ModuleFail,List.empty)