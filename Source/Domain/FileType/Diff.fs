module Diff


type ChangedLine =
    | Added of string
    | Removed of string

type DiffLine =
    | Changed of ChangedLine
    | NotChanged

let isChangedLine (x:DiffLine):bool = 
    match x with
    | Changed x -> true
    | NotChanged -> false

type DiffFile =
    | Same of string
    | Different of string*list<DiffLine>

type DiffResults = {
    originFile : DiffFile;
    compareeFiles : list<DiffFile>
}
let DiffFile_ x y:DiffResults = 
{
    originFile = x;
    compareeFiles = y
}