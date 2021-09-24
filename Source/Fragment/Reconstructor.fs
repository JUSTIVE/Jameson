module Reconstructor

open FileType
open FSharp.Data
open Diff
open State

//type treeDepthElement = int*string

//let rec reconstruct_ (state:JsonValue) (itemList:list<string>):JsonValue = 
//    match itemList with
//    | [] -> state
//    | h::t -> reconstruct_ (something state) t


//let rec reconstruct (fileKeySet:FileKeySet):JsonValue =
//    FileKeySet

    
//    test fileKeySet
//    result

//and private test (fileKeySet:FileKeySet):Unit =
//    let original = fileKeySet
//    let regenerated =
//        fileKeySet
//        |>reconstruct
//        |>JsonParser.parse

//    let compareResult =
//        Compare.compare
//            ("origin",OriginFile,original)
//            ("reconstruct",CompareeFile,regenerated)

//    match compareResult with
//    | Success compareResult -> 
//        match compareResult with
//        | Same _ -> "Clear"
//        | __ -> "Not Clear"
//    | Fail _ -> "Not Clear"
//    |> printfn "%s"

