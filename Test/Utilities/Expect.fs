module Expect
open TestResult

let expect<'T when 'T:equality> (x:'T) (y:'T) :UnitTestState = 
    match x = y with
    | true -> UnitSuccess
    | false -> UnitFail <|{expected=($"{x}");given=($"{y}")}
