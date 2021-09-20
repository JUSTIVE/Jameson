module TargetRunner
open State
open JamesonResult
open JamesonResults
open FileType
open JsonLoader
open JsonParser
open Compare
open Diff

let run (originFilePath:string) (comparingFilePath:string) :Result<DiffFile,JamesonResult>= 
    match readJSONFile originFilePath with
    | Fail(jamesonResult)->Fail(jamesonResult)
    | Success(jsonValue) ->
        let originFilekeySet = parse jsonValue
        match readJSONFile comparingFilePath with
        | Fail(jamesonResult) ->Fail(jamesonResult)
        | Success(jsonValue) ->
            let comparingFileKeySet = parse jsonValue
            compare (OriginFile originFilekeySet) (CompareeFile comparingFileKeySet)
