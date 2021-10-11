﻿module GeneralRunner
open JamesonResult
open JamesonResults
open State
open JamesonOption
open FileType
open Compare
open Diff

let run jamesonOption (generalRunnerOption:GeneralRunnerOption):Result<DiffResults,list<JamesonResult>> =
    let source = generalRunnerOption.source
    let targetFiles = generalRunnerOption.targetCandidate

    let readKeyFileSetResult path =
        match readJSONFile path with
        | Fail(jamesonResult)-> Fail(jamesonResult)
        | Success(jsonValue) -> Success <|parse jsonValue

    let originFileKeySetResult = readKeyFileSetResult source.path

    let compareStep originFileKeySet comparingFileKeySet =
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
        let targetFileKeySetResult = readKeyFileSetResult comparingFileKeySet
        let compareeResult = 
            compare
                (target,CompareeFile,targetFileKeySetResult)
                (source,OriginFile,originFileKeySetResult)
        match compareeResult with

       

    let rec generalFileRunner (runResults:list<JamesonResult>) (targetFileList:list<FileArgument>) = 
        match targetFile with
        | [] -> runResults
        | h::t ->
            
            generalFileRunner (runResults::compareeResult) (t)
    generalFileRunner [] generalRunnerOption.targetCandidate


