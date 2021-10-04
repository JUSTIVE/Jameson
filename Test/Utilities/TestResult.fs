module TestResult

type FailedReason = {
    expected:string;
    given:string;
}

type UnitTestState = 
    | UnitSuccess 
    | UnitFail of FailedReason

type UnitName = string

type UnitTestResult = (UnitName*UnitTestState)

let successUnitTests (unitList:list<UnitTestResult>) =
    unitList
    |>List.filter(
        fun x ->
            match x with
            | (name,UnitSuccess) -> true
            | (name,UnitFail y) -> false
        )

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
    | UnitSuccess -> ModuleSuccess
    | UnitFail(reason) -> ModuleFail

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