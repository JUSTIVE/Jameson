module TestResult

type FailedReason = string

type UnitTestState = 
    | Success 
    | Fail of FailedReason

type UnitName = string

type UnitTestResult = (UnitName*UnitTestState)

type ModuleName = string

type ModuleResultState = 
    | Init of ModuleName
    | NonInit of ModuleTest
        
and ModuleTestState=
    | ModuleSuccess 
    | ModuleFail

and ModuleTest = (ModuleName*ModuleTestState*list<UnitTestResult>)

let UnitResultToModuleResult (x:UnitTestState) :ModuleTestState = 
    match x with 
    | Success -> ModuleSuccess
    | Fail(reason) -> ModuleFail

let UnitTestResultToModuleResult (x:UnitTestResult) : ModuleTestState = 
    let (unitName,state) = x
    UnitResultToModuleResult state

let JoinResult (state:ModuleResultState) (currently:UnitTestResult) :ModuleResultState= 
    match state with 
    | Init(moduleName) ->
        NonInit(moduleName,UnitTestResultToModuleResult currently,currently::[])
    | NonInit(moduleName,moduleResult,unitList) ->
        let resultType:ModuleTestState = 
            match moduleResult with
            | ModuleFail -> ModuleFail
            | ModuleSuccess ->
                UnitTestResultToModuleResult currently
        NonInit(moduleName,resultType,currently::unitList)