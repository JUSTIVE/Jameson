module Diff


type ChangedLine =
    | Added of string
    | Removed of string

type DiffLine =
    | Changed of ChangedLine
    | NotChanged

let isChangedLine (x:DiffLine):bool = 
    match x with
    | Changed(x) -> true
    | NotChanged -> false

type DiffFile =
    | Same
    | Different of list<DiffLine>
