module TargetRunner
open State
open Result
open JsonLoader
open JsonParser
open JamesonOption
open FileType
open JamesonResult
open JamesonResults
open Compare
open Diff

let run jamesonOption (targetRunnerOption:TargetRunnerOption) :Result<DiffResults,list<JamesonResult>> = 
    let source = targetRunnerOption.source
    let target = targetRunnerOption.target

    let readKeyFileSetResult path =
        match readJSONFile path with
        | Fail(jamesonResult)-> Fail(jamesonResult)
        | Success(jsonValue) -> Success <|parse jsonValue
    let originFileKeySetResult = readKeyFileSetResult source.path
    let targetFileKeySetResult = readKeyFileSetResult target.path

    let compareStep originFileKeySet comparingFileKeySet =
        let originResult = 
            compare 
                (source,OriginFile,originFileKeySet)
                (target,CompareeFile,comparingFileKeySet)
        let compareeResult = 
            compare
                (target,CompareeFile,comparingFileKeySet)
                (source,OriginFile,originFileKeySet)

        let strictStep strict originFile compareeFile :Result<DiffResults,list<JamesonResult>> =
            let diffResults =  Success <|DiffFile_ originFile [compareeFile]
            match originFile,compareeFile with
            | Same _,Same _ ->  diffResults 
            |  _ ->  
                if strict 
                then Fail [NOT_SAME] 
                else diffResults
        HandleResultTuple (originResult,compareeResult) (strictStep jamesonOption.strict)
    HandleResultTuple (originFileKeySetResult,targetFileKeySetResult) compareStep
