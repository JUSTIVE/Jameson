module TargetRunner
open State
open JamesonResult
open JamesonResults
open FileType
open JsonLoader
open JsonParser
open Compare

let run (originFilePath:string) (comparingFilePath:string) :JamesonResult= 
    match readJSONFile originFilePath with
    | Fail(jamesonResult)->jamesonResult
    | Success(jsonValue) ->
        let originFilekeySet = parse jsonValue
        match readJSONFile comparingFilePath with
        | Fail(jamesonResult) ->jamesonResult
        | Success(jsonValue) ->
            let comparingFileKeySet = parse jsonValue
            match compare (OriginFile originFilekeySet) (CompareeFile comparingFileKeySet) with
            | Fail(jamesonResult) -> jamesonResult
            | Success(x) -> GOOD
            
            //GOOD
        
    
