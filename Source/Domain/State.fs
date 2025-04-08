module State
open FSharp.Core

type ReduceState<'a> = 
    | Init
    | NonInit of 'a

let HandleResultTuple (tuple:Result<'a,'b> * Result<'c,'b>) (action:'a->'c->Result<'d,list<'b>>):Result<'d,list<'b>> = 
    match tuple with
    | Ok x, Ok y   -> action x y
    | Error b,   Ok _   -> Error [b]
    | Ok _,Error c      -> Error [c]
    | Error d,   Error e      -> Error [d;e]