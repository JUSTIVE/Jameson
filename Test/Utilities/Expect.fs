module Expect
open TestResult

let expect<'T when 'T:equality> (x:'T) (y:'T) :TestState = 
    match x = y with
    | true -> Success
    | false -> Fail("unmatch")