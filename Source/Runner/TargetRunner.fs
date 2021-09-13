module TargetRunner
open State
open JamesonResult
open JamesonResults
open JsonLoader
open JsonParser

let run (originFilePath:string) (comparingFilePath:string) :JamesonResult= 
    match readJSONFile originFilePath with
    | Fail(jamesonResult)->jamesonResult
    | Success(jsonValue) ->
        let originFilekeySet = parse jsonValue
        match readJSONFile comparingFilePath with
        | Fail(jamesonResult) ->jamesonResult
        | Success(jsonValue) ->
            let comparingFileKeySet = parse jsonValue
            GOOD
        
    
