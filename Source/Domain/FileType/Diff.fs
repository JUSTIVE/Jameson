module Diff

open FileType

type ChangedLine =
    | Added of PseudoJson.t
    | Removed of PseudoJson.t
    
type DiffLine =
    | Changed of ChangedLine
    | NotChanged of PseudoJson.t

let isChangedLine (x: DiffLine) : bool =
    match x with
    | Changed x -> true
    | NotChanged y -> false

type DiffFile =
    | Same of FileArgument
    | Different of FileArgument * list<DiffLine>


module DiffResults =
    type t =
        { originFile: DiffFile
          compareeFiles: list<DiffFile> }

    let make x y : t = { originFile = x; compareeFiles = y }
