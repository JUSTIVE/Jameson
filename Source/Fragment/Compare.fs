module Compare
open State
open Diff
open FileType
open JamesonResult
open JamesonResults

let compare_ (origin:Set<string>) (comparee:Set<string>) (changedLine:string->ChangedLine)  filename  :DiffFile = 
    let diffy  (comparee:Set<string>) (targetLine:string) :DiffLine= 
        if comparee.Contains targetLine
            then NotChanged
            else changedLine targetLine|>Changed

    let diffLines = 
        origin
        |>Set.map (diffy comparee)
        |>Set.toList

    if diffLines
        |>List.exists (isChangedLine)
        then Different (filename,diffLines)
        else Same filename

let compare ((filename,filetype,filevalue):FileData) ((filename2,filetype2,filevalue2):FileData) :Result<DiffFile,JamesonResult> =
    match (filetype,filetype2) with
    | (OriginFile, CompareeFile) -> Success <| compare_ filevalue filevalue2 Removed filename
    | (CompareeFile, OriginFile) -> Success <| compare_ filevalue filevalue2 Added filename2
    | __ -> Fail(INVALID_KEYSET)