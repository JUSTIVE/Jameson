module ArgumentParser
open JamesonOption
open JamesonResult

type ParseResult = 
    | Success of JamesonOption
    | Fail of JamesonResult

let parse (argument:string[]):ParseResult = 
    Success
