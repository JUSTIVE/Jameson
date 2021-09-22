module TargetRunner
open State
open JsonLoader
open JsonParser
open JamesonOption
open FileType
open JamesonResult
open JamesonResults
open Compare
open Diff

let run jamesonOption targetRunnerOption :Result<DiffResults,list<JamesonResult>> = 
    match readJSONFile targetRunnerOption.source.path with
    | Fail(jamesonResult)->Fail([jamesonResult])
    | Success(jsonValue) ->
        let originFilekeySet = parse jsonValue
        match readJSONFile targetRunnerOption.target.path with
        | Fail(jamesonResult) ->Fail([jamesonResult])
        | Success(jsonValue) ->
            let comparingFileKeySet = parse jsonValue
            let originResult = 
                compare 
                    (targetRunnerOption.source.filename,OriginFile,originFilekeySet)
                    (targetRunnerOption.target.filename,CompareeFile,comparingFileKeySet)
            let compareeResult = 
                compare
                    (targetRunnerOption.target.filename,CompareeFile,comparingFileKeySet)
                    (targetRunnerOption.source.filename,OriginFile,originFilekeySet)
            match (originResult,compareeResult) with
            | Success originFile, Success compareeResults ->
                if jamesonOption.strict 
                    then
                        match originFile with
                        | Same __ -> 
                            {
                                originFile = originFile
                                compareeFiles = [compareeResults]
                            }
                            |>Success
                        | Different _ -> 
                            Fail [NOT_SAME]
                    else 
                        {
                            originFile = originFile
                            compareeFiles = [compareeResults]
                        }
                        |>Success
            | Fail x, Success _  -> Fail [x]
            | Success _, Fail x -> Fail [x]
            | Fail x, Fail y -> Fail [x;y]

                
