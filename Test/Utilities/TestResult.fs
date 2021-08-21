module TestResult

type FailedReason = string

type UnitTestState = 
    | Success 
    | Fail of FailedReason

type UnitName = string

type UnitTestResult = (UnitName*UnitTestState)


        

type ModuleResultState = 
    | Init
    | NonInit of ModuleResult
        
and ModuleResult=
    | ModuleSuccess 
    | ModuleFail

type ModuleTest = (string*ModuleResult*list<UnitTestResult>)

let JoinResult (x:UnitTestResult) (y:UnitTestResult) :UnitTestResult= 
    match x with 
    | (unitName,Fail(resaon)) -> (unitName,Fail(resaon))
    | (unitName,Success)->
        match y with
        | (unitName,Fail(resaon)) -> (unitName,Fail(resaon))
        | (unitName,Success) -> (unitName,Success)


