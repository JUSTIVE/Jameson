module JsonLoader
open State
open Result
open JamesonResult
open JamesonResults
open FSharp.Data

let readJSONFile (path:string):Result<JsonValue,JamesonResult> = 
    try
        Ok(JsonValue.Load(path))
    with
        :? System.IO.FileNotFoundException->
            Error(FILE_NOT_FOUND path)