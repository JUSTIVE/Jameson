module JsonParser
open FSharp.Data
open FSharp.Data.JsonExtensions

let joinKey (key:string) (parentPath:string):string = 
    match parentPath with
    | "" -> key
    | _  -> $"{parentPath}.{key}"

let rec keySet (parentPath:string) (state:Set<string>) (jsonValue:JsonValue) :Set<string> = 
    match jsonValue.Properties with
    | [||] -> state
    | propList ->
        let setJoinToSeq (x:seq<'a>) (y:Set<'a>)=
            x
            |>Seq.append (y|>Set.toSeq)
            
        let transformer ((key,value):(string*JsonValue)):Set<string> = 
            match value with
            | JsonValue.Array elements ->
                elements
                |> Array.map (keySet ($"{parentPath}:{key}") Set.empty<string>)
                |> Array.fold setJoinToSeq Seq.empty
                |> Set.ofSeq
            | JsonValue.Record properties->
                properties
                |> Array.map (keySet ($"{parentPath}:{key}") Set.empty<string>)
                |> Array.fold setJoinToSeq Seq.empty
                |> Set.ofSeq
            | others ->
                
                
        //let applySetString ((key,value):(string*JsonValue)):Set<string> = 
        //    match parentPath with
        //    | "" -> 
        //        Set.add (joinKey key parentPath) state
        //        |>keySet value key 
        
        propList
        |> Array.map transformer 
        |> Array.fold setJoinToSeq Seq.empty<string>
        |> Set.ofSeq