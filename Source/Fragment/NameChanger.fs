module NameChanger
open JamesonOption
open FileType

let setNameConvention namingConventionType (value:string):string =
    match namingConventionType with
    | UpperCase -> value.ToUpper()
    | LowerCase -> value.ToLower()
    | __ ->
        let listChar = Seq.toList value
        match listChar with
        | h::t ->
            match 
        | x -> x
        |> List.toSeq
let run (targetNamingConvention:CheckConventionType) (key:FileData):FileData= 
    key