module NameChanger
open System
open JamesonOption
open FileType

let setNameConvention namingConventionType value=
    let simpleConvention simpleConvention (value:string) =
        let handleChunk (value:string)=
            match simpleConvention with
            | SimpleConventionType.UpperCase -> value.ToUpper()
            | SimpleConventionType.LowerCase -> value.ToLower()
        String.Join(
            "",
            value.Split("_")
            |>Array.map handleChunk
        )

    let headCharConvention headCharConvention (value:string) = 
        let handleChunk index (value:string) =
            let head = value.[0]
            let touchedChar = 
                match headCharConvention with
                | HeadCharConventionType.PascalCase -> (string(head)).ToUpper()
                | HeadCharConventionType.CamelCase  when index=0-> (string(head)).ToLower()
                | HeadCharConventionType.CamelCase  -> (string(head)).ToUpper()
            touchedChar.[0]::(Seq.toList value.[1..])
            |>List.toSeq
            |>String.Concat
        String.Join(
            "",
            value.Split("_")
            |>Array.mapi handleChunk
        )

    let complexConvention complexConvention value=
        let capitalToSnake index (value:char):string=
            match value with
            | x when Char.IsUpper x && index <> 0 -> $"_{Char.ToLower x}"
            | y when Char.IsLetter y -> $"{Char.ToLower y}"
            | z -> $"{z}"
        match complexConvention with
        | ComplexConventionType.SnakeCase ->
            String.Join("", Seq.mapi capitalToSnake value)

    match namingConventionType with
    | NoConvention -> (fun x -> x) 
    | SimpleConventionType convention -> simpleConvention convention
    | HeadCharConventionType convention -> headCharConvention convention
    | ComplexConventionType convention -> complexConvention convention
    <| value

let pathConvention targetNamingConvention (path:string):string =
    (":",
        path.Split(":")
        |>Seq.map (setNameConvention targetNamingConvention))
    |>String.Join

let pseudoJsonConvention targetNamingConvention (pseudoJson:PseudoJson.t) =
    PseudoJson.make (pathConvention targetNamingConvention pseudoJson.path) pseudoJson.value

let FileKeySetConvention targetnamingConvention (fileKeySet:FileKeySet) =
    Set.map (pseudoJsonConvention targetnamingConvention) fileKeySet
        
let run (targetNamingConvention:CheckConventionType) ((fileArgument,fileType,fileKeySet):FileData):FileData= 
        (fileArgument,fileType,FileKeySetConvention targetNamingConvention fileKeySet)