﻿module FileType

open FSharp.Data

type FileArgument = {
    filename:string;
    path:string
}

let FileArgument_ filename path :FileArgument = {
    filename= filename;
    path=path;
}

type PseudoJson =
    {
        path:string;
        value:JsonValue
    }

let PseudoJson_ path value =
    {
        path=path;
        value=value;
    }

let compareJsonLeaf (originPath,originValue) (targetPath,targetValue) =
    originPath=targetPath

type FileKeySet = Set<PseudoJson>

type FileType = 
    | OriginFile 
    | CompareeFile

type FileData = FileArgument*FileType*FileKeySet

