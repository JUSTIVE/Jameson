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

let run jamesonOption (targetRunnerOption:TargetRunnerOption) :Result<DiffResults,list<JamesonFail>> = 
    let source = targetRunnerOption.source
    let target = targetRunnerOption.target

    let readKeyFileSetResult path :Result<FileKeySet,JamesonFail> =
        match readJSONFile path with
        | Fail(jamesonResult)-> Fail <| JamesonFail_ jamesonResult Option.None
        | Success(jsonValue) -> Success <|parse jsonValue
    let originFileKeySetResult = readKeyFileSetResult source.path
    let targetFileKeySetResult = readKeyFileSetResult target.path

    let compareStep originFileKeySet comparingFileKeySet :Result<DiffResults,list<JamesonFail>> =
        let originResult = 
            compare 
                (source,OriginFile,originFileKeySet)
                (target,CompareeFile,comparingFileKeySet)
        let compareeResult = 
            compare
                (target,CompareeFile,comparingFileKeySet)
                (source,OriginFile,originFileKeySet)

        let strictStep originFile compareeFile :Result<DiffResults,list<JamesonFail>> =
            let diffResults = DiffResults_ originFile [compareeFile]
            match originFile,compareeFile with
            | Same _,Same _ ->  Success diffResults
            | Same _, Different(fileArgument,difflineList) -> Fail [JamesonFail_ NOT_SAME (Option.Some (Different(fileArgument,difflineList)))]
            | Different(fileArgument,difflineList), Same _ -> Fail [JamesonFail_ NOT_SAME (Option.Some (Different(fileArgument,difflineList)))]
            | Different(fileArgumentA,difflineListA), Different(fileArgumentB,difflineListB) -> 
                Fail [
                    JamesonFail_ NOT_SAME (Option.Some (Different(fileArgumentA,difflineListA)));
                    JamesonFail_ NOT_SAME (Option.Some (Different(fileArgumentB,difflineListB)))
                ]
        HandleResultTuple (originResult,compareeResult) (strictStep)
    HandleResultTuple (originFileKeySetResult,targetFileKeySetResult) compareStep
