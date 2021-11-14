module Result

type Result<'a,'b> = 
    | Success of 'a
    | Fail of 'b

let isSuccess result =
    match result with
    | Success _ -> true
    | Fail _ ->false

let isFail result =
    match result with
    | Success _ -> false
    | Fail _ -> true

let flatMapResult (result:Result<'a,'b>) (mapfun:'a->'c) :Result<'c,'b> = 
    match result with
    | Success(x) ->
        Success <| mapfun x
    | Fail(x)-> Fail(x)

let HandleResultTuple (tuple:Result<'a,'b> * Result<'c,'b>) (action:'a->'c->Result<'d,list<'b>>):Result<'d,list<'b>> = 
    match tuple with
    | Success x,Success y   -> action x y
    | Fail b,   Success _   -> Fail [b]
    | Success _,Fail c      -> Fail [c]
    | Fail d,   Fail e      -> Fail [d;e]