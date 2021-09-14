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


let rec keySet (parentPath:string) (state:Set<string>) (jsonValue:JsonValue) :FileContent = 
    match jsonValue.Properties with
    | [||] -> state
    | propList ->
        let setJoinToSeq (x:seq<'a>) (y:Set<'a>) :seq<'a> =
            x
            |>Seq.append (y|>Set.toSeq)
            
        let transformer ((key,value):(string*JsonValue)):JsonStructure = 
            match value with
            | JsonValue.Array elements ->
                elements
                |> Array.map (keySet ($"{parentPath}:{key}") Set.empty<string>)
                |> Array.fold setJoinToSeq Seq.empty
                |> Set.ofSeq
                |> ComplexValue
            | JsonValue.Record (properties)->
                properties
                |> Array.map (fun (key,jsonValue) -> keySet ($"{parentPath}:{key}") Set.empty<string> jsonValue)
                |> Array.fold setJoinToSeq Seq.empty
                |> Set.ofSeq
                |> ComplexValue
            | others ->
                $"{parentPath}:{key}"
                |>SimpleValue
                
        let foldJsonStructure (state:seq<string>) (x:JsonStructure) :seq<string>= 
            match x with
            | SimpleValue(simpleValue)-> 
                (simpleValue::(state|>Seq.toList))
                |>List.toSeq
            | ComplexValue(complexValue)->
                setJoinToSeq state complexValue

        propList
        |> Array.map transformer 
        |> Array.fold foldJsonStructure Seq.empty<string>
        |> Set.ofSeq

let parse (jsonValue:JsonValue)=
    keySet "" Set.empty jsonValue