module Diff
open FileType

type ChangedLine =
    | Added of PseudoJson
    | Removed of PseudoJson

type DiffLine =
    | Changed of ChangedLine
    | NotChanged of PseudoJson

let isChangedLine (x:DiffLine):bool = 
    match x with
    | Changed x -> true
    | NotChanged y -> false

type DiffFile =
    | Same of FileArgument
    | Different of FileArgument*list<DiffLine>

type DiffResults = {
    originFile : DiffFile;
    compareeFiles : list<DiffFile>
}
let DiffResults_ x y:DiffResults = {
    originFile = x;
    compareeFiles = y
}