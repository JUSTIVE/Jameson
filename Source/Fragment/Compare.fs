module Compare
open State
open Diff
open FileType
open JamesonResult
open JamesonResults

let private compare_ (origin:FileKeySet) (comparee:FileKeySet) (changedLine:PseudoJson->ChangedLine)  filename  :DiffFile =
    let compareePathSetOnly =
        comparee
        |>Set.map(fun x -> x.path)
        
    let diffy  (targetLine:PseudoJson) :DiffLine= 
        match Set.contains targetLine.path compareePathSetOnly with
        | true -> NotChanged
        | false -> changedLine targetLine|>Changed

    let diffLines = 
        origin
        |>Set.map diffy
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