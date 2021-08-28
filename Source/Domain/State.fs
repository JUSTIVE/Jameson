module State

type ReduceState<'a> = 
    | Init
    | NonInit of 'a

type Result<'a,'b> = 
    | Success of 'a
    | Fail of 'b