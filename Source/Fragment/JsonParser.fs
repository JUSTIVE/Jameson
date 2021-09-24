module JsonParser
open FSharp.Data
open FileType

let joinKey (key:string) (parentPath:string):string = 
    match parentPath with
    | "" -> key
    | _  -> $"{parentPath}.{key}"

type JsonStructure = 
    | ComplexValue of Set<string>
    | SimpleValue of string


let rec keySet (parentPath:string) (state:Set<string>) (jsonValue:JsonValue) :FileKeySet= 
    let setJoinToSeq (x:seq<'a>) (y:Set<'a>) :seq<'a> =
        x
        |>Seq.append (y|>Set.toSeq)
    match jsonValue with
    | JsonValue.Array elements ->
        elements
        |> Array.map (keySet ($"{parentPath}") Set.empty<string>)
        |> Array.fold setJoinToSeq Seq.empty
        |> Set.ofSeq
    | JsonValue.Record properties->
        properties
        |> Array.map (fun (key,jsonValue) -> keySet ($"{parentPath}:{key}") Set.empty<string> jsonValue)
        |> Array.fold setJoinToSeq Seq.empty
        |> Set.ofSeq
    | x -> Set.add (PseudoJson_ parentPath x) Set.empty

let parse (jsonValue:JsonValue):FileKeySet=
    keySet "" Set.empty jsonValue