module Compare
open State
open Diff
open FileType
open JamesonResult

let compare_ (changedLine:string->ChangedLine) (origin:Set<string>) (comparee:Set<string>)  :DiffFile = 
    let diffy  (comparee:Set<string>) (targetLine:string) :DiffLine= 
        match comparee.Contains targetLine with
        | true -> NotChanged
        | false -> changedLine targetLine|>Changed

    let diffLines = 
        origin
        |>Set.map (diffy comparee)
        |>Set.toList

    match diffLines
        |>List.filter (isChangedLine)
        |>List.isEmpty
        with 
    | true -> Same
    | false -> Different diffLines

let compareOrigin (origin:FileType) (comparee:FileType) :Result<DiffFile,JamesonResult> =
    match (origin,comparee) with
    | (OriginFile(x),CompareeFile(y)) -> 
        Success(compare_ Removed x y)
    | (CompareeFile(x),OriginFile(y)) -> 
        Success(compare_ Added x y)
    | __ -> INVALID_COMPARE_TARGET
    
    
