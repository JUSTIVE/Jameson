module Compare
open State
open Diff
open FileType
open JamesonResult
open JamesonResults

let compare_ (origin:Set<string>) (comparee:Set<string>) (changedLine:string->ChangedLine)  filename  :DiffFile = 
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
    | true -> Same filename
    | false -> Different (filename,diffLines)

let compare ((filename,filetype,filevalue):FileData) ((filename2,filetype2,filevalue2):FileData) :Result<DiffFile,JamesonResult> =
    match (filetype,filetype2) with
    | (OriginFile, CompareeFile) -> 
        Success <| compare_ filevalue filevalue2 Removed filename
    | (CompareeFile, OriginFile) -> 
        Success <| compare_ filevalue filevalue2 Added filename2
    | __ -> Fail(INVALID_KEYSET)