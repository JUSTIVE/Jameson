module Diff

type ChangedLine =
    | Added of string
    | Removed of string

type DiffLine =
    | Changed of ChangedLine
    | NotChanged

type DiffFile =
    | Same
    | Different of array<DiffLine>
