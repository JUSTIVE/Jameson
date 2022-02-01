module Result

type Result<'success,'failed> = 
    | Success of 'success
    | Fail of 'failed

let isSuccess result =
    match result with
    | Success _ -> true
    | Fail _ ->false

let isFail result =
    match result with
    | Success _ -> false
    | Fail _ -> true

let flatMapResult (result:Result<'success,'failed>) (mapfun:'success->'newType)
    :Result<'newType,'failed> = 
    match result with
    | Success(x) ->
        Success <| mapfun x
    | Fail(x)-> Fail(x)

let HandleResultTuple
    (tuple:Result<'success,'failed> * Result<'newType,'failed>)
    (action:'success->'newType->Result<'newSuccess,list<'failed>>)
    :Result<'newSuccess,list<'failed>> = 
    match tuple with
    | Success x,Success y   -> action x y
    | Fail b,   Success _   -> Fail [b]
    | Success _,Fail c      -> Fail [c]
    | Fail d,   Fail e      -> Fail [d;e]