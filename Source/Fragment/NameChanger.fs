module NameChanger
open System
open JamesonOption
open FileType

let setNameConvention namingConventionType (value:string):string =
    match namingConventionType with
    | UpperCase -> value.ToUpper()
    | LowerCase -> value.ToLower()
    | __ ->
        let head = value.[0]
        let touchedChar = 
            match namingConventionType with
            | CamelCase -> (string(head)).ToLower()
            | PascalCase -> (string(head)).ToUpper()
            | x -> string(head)
        touchedChar.[0]::(Seq.toList value.[1..])
        |>List.toSeq
        |>String.Concat
        
let run (targetNamingConvention:CheckConventionType) (key:FileData):FileData= 
    key