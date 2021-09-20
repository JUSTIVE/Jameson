module JsonParser
open System
open FSharp.Data
open FSharp.Data.JsonExtensions
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
    | JsonValue.Null -> Set.add parentPath Set.empty
    | JsonValue.Number n -> Set.add parentPath Set.empty
    | JsonValue.Float f -> Set.add parentPath Set.empty
    | JsonValue.Boolean b -> Set.add parentPath Set.empty
    | JsonValue.String x ->  Set.add parentPath Set.empty
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

let parse (jsonValue:JsonValue):FileKeySet=
    keySet "" Set.empty jsonValue