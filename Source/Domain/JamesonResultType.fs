module JamesonResult
open State
open Diff
open Result

type JamesonResult = {
    message:string;
    errorCode:int;
}

type JamesonFail = {
    result:JamesonResult;
    reason:Option<DiffFile>
}

let JamesonFail_ (result:JamesonResult) (reason:Option<DiffFile>):JamesonFail = {
    result=result;
    reason=reason
}

let JamesonFail_Default (result:JamesonResult):JamesonFail = {
    result=result;
    reason=Option.None
}

let joinResultJamesonResult 
    (result:Result<'a,JamesonFail>)
    (action:'a->Result<'b,JamesonFail>)
    :Result<'b,JamesonFail> =
    match result with
    | Success x -> action x 
    | Fail a -> Fail a