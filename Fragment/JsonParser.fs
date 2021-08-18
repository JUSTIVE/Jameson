module JsonParser
open FSharp.Data
open FSharp.Data.JsonExtensions

let joinKey (key:string) (parentPath:string):string = 
    match parentPath with
    | "" -> key
    | _  -> $"{parentPath}.{key}"

let rec keySet (jsonValue:JsonValue) (parentPath:string) (state:Set<string>):Set<string> = 
    match jsonValue.Properties with
    | [||] -> state
    | propList ->
        let applySetString ((key,value):(string*JsonValue)) = 
            match parentPath with
            | "" -> 
                Set.add (joinKey key parentPath) state
                |>keySet value key 
        propList
        |>Array.map applySetString 
        |>Array.reduce(Seq.append)
        |>Set.ofSeq