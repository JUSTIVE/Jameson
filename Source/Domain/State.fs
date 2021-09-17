module State

type ReduceState<'a> = 
    | Init
    | NonInit of 'a

type Result<'a,'b> = 
    | Success of 'a
    | Fail of 'b

let flatMap (result:Result<'a,'b>) (mapfun:'a->'c) :Result<'c,'b> = 
    match result with
    | Success(x) ->
        Success <| mapfun x
    | Fail(x)-> Fail(x)