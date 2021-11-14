module JamesonResult
open State
open Result

type JamesonResult = {
    message:string;
    errorCode:int
}

let joinResultJamesonResult (result:Result<'a,JamesonResult>) (action:'a->Result<'b,JamesonResult>) :Result<'b,JamesonResult> =
    match result with
    | Success x -> action x 
    | Fail a -> Fail a