module Reconstructor

open FileType
open FSharp.Data
open Diff
open State

//type treeDepthElement = int*string

//let addToLeaf (state:JsonValue) 

//let rec reconstruct_ (state:JsonValue) (itemList:list<string>):JsonValue = 
//    match itemList with
//    | [] -> state
//    | h::t -> reconstruct_ (something state) t


//let rec reconstruct (fileKeySet:FileKeySet):JsonValue =
    
//    test fileKeySet
//    //reconstruct_ fileKeySet

//and private test (fileKeySet:FileKeySet):Unit =
//    let original = fileKeySet
//    let regenerated =
//        fileKeySet
//        |>reconstruct
//        |>JsonParser.parse

//    let compareResult =
//        Compare.compare
//            ({filename="origin";path=""},OriginFile,original)
//            ({filename="reconstruct";path=""},CompareeFile,regenerated)

//    match compareResult with
//    | Success compareResult -> 
//        match compareResult with
//        | Same _ -> "Clear"
//        | __ -> "Not Clear"
//    | Fail _ -> "Not Clear"
//    |> printfn "%s"

