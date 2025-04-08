module JamesonResult
open State
open Diff

type JamesonResult = {
    message:string;
    errorCode:int;
}

module JamesonFail =
    type t = {
        result:JamesonResult;
        reason:Option<DiffFile>
    }

    let make (result:JamesonResult) (reason:Option<DiffFile>):t = {
        result=result;
        reason=reason
    }

    let Default (result:JamesonResult):t = {
        result=result;
        reason=Option.None
    }

let joinResultJamesonResult 
    (result:Result<'a,JamesonFail.t>)
    (action:'a->Result<'b,JamesonFail.t>)
    :Result<'b,JamesonFail.t> =
    match result with
    | Ok x -> action x 
    | Error a -> Error a