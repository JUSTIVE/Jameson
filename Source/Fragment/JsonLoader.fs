module JsonLoader
open State
open JamesonResult
open JamesonResults
open FSharp.Data
open FSharp.Data.JsonExtensions

let readJSONFile (path:string):Result<JsonValue,JamesonResult> = 
    try
        Success(JsonValue.Load(path))
    with
        :? System.IO.FileNotFoundException->
            Fail(FILE_NOT_FOUND path)