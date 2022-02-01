module Result

type FailedReason<'failed,'reason> = {
    message:'failed;
    reason:'reason
}

type Result<'success,'failed,'reason> = 
    | Success of 'success
    | Fail of FailedReason<'failed,'reason>

let isSuccess result =
    match result with
    | Success _ -> true
    | Fail _ ->false

let isFail result =
    match result with
    | Success _ -> false
    | Fail _ -> true

let flatMapResult (result:Result<'success,'failed,'reason>) (mapfun:'success->'newType) :Result<'newType,'failed,'reason> = 
    match result with
    | Success(x) ->
        Success <| mapfun x
    | Fail(x)-> Fail(x)

let HandleResultTuple
    (tuple:Result<'success,'failed,'reason> * Result<'newType,'failed,'reason>)
    (action:'success->'newType->Result<'newSuccess,list<'failed>,'reason>)
    :Result<'newSuccess,list<'failed>,'reason> = 
    match tuple with
    | Success x,Success y   -> action x y
    | Fail b,   Success _   -> Fail ({message=[b.message];reason=b.reason})
    | Success _,Fail c      -> Fail ({message=[c.message];reason=c.reason})
    | Fail d,   Fail e      -> Fail [d;e]