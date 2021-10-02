module NameChanger
open System
open JamesonOption
open FileType

let setNameConvention namingConventionType (value:string):string =
    let simpleConvention (simpleConvention:SimpleConventionType) (value:string) =
        match simpleConvention with
        | SimpleConventionType.UpperCase -> value.ToUpper()
        | SimpleConventionType.LowerCase -> value.ToLower()    

    let headCharConvention headChartConvention (value:string) = 
        let head = value.[0]
        let touchedChar = 
            match headChartConvention with
            | HeadCharConventionType.PascalCase -> (string(head)).ToUpper()
            | HeadCharConventionType.CamelCase  -> (string(head)).ToLower()
        touchedChar.[0]::(Seq.toList value.[1..])
        |>List.toSeq
        |>String.Concat

    let complexConvention complexConvention value=
        match complexConvention with
        | ComplexConventionType.SnakeCase ->
            value


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
    
        
let run (targetNamingConvention:CheckConventionType) (key:FileData):FileData= 
    key