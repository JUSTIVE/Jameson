module State

type ReduceState<'a> = 
    | Init
    | NonInit of 'a

