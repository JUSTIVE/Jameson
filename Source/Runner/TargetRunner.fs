module TargetRunner
open State
open FileType
open JsonLoader
open JsonParser
open Compare
open JamesonOption

let run targetRunnerOption = 
    match readJSONFile targetRunnerOption.sourcePath with
    | Fail(jamesonResult)->Fail(jamesonResult)
    | Success(jsonValue) ->
        let originFilekeySet = parse jsonValue
        match readJSONFile targetRunnerOption.targetPath with
        | Fail(jamesonResult) ->Fail(jamesonResult)
        | Success(jsonValue) ->
            let comparingFileKeySet = parse jsonValue
            compare (OriginFile originFilekeySet) (CompareeFile comparingFileKeySet)
