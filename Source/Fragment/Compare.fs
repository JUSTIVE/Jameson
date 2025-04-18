﻿module Compare

open FSharp.Core
open Diff
open FileType
open JamesonResult

let private compare_ (origin:FileKeySet) (comparee:FileKeySet) (changedLine:PseudoJson.t->ChangedLine)  (fileArgument:FileArgument)  :DiffFile =
    let compareePathSetOnly =
        comparee
        |>Set.map(fun x -> x.path)
        
    let diffy (targetLine:PseudoJson.t) :DiffLine= 
        match Set.contains targetLine.path compareePathSetOnly with
        | true -> NotChanged targetLine
        | false -> changedLine targetLine|>Changed

    let diffLines = 
        origin
        |>Set.map diffy
        |>Set.toList

    if diffLines
        |>List.exists (isChangedLine)
        then Different (fileArgument,diffLines)
        else Same fileArgument

let compare ((filename,filetype,filevalue):FileData) ((filename2,filetype2,filevalue2):FileData) :Result<DiffFile,JamesonFail.t> =
    match (filetype,filetype2) with
    | (OriginFile, CompareeFile) -> Ok <| compare_ filevalue filevalue2 Removed filename
    | (CompareeFile, OriginFile) -> Ok <| compare_ filevalue filevalue2 Added filename2
    
        