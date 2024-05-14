module TargetRunner
open JsonLoader
open JsonParser
open JamesonOption
open FileType
open JamesonResult
open JamesonResults
open Compare
open Diff

let run jamesonOption (targetRunnerOption:TargetRunnerOption)=
    let readKeyFileSetResult path =
        readJSONFile path
        |> Result.map parse
        //match readJSONFile path with
        //| Fail(jamesonResult) -> Fail    <| JamesonFail_ jamesonResult Option.None
        //| Success(jsonValue)  -> Success <| parse jsonValue
    let source = targetRunnerOption.source
    let target = targetRunnerOption.target
    let originFileKeySetResult = readKeyFileSetResult source.path
    let targetFileKeySetResult = readKeyFileSetResult target.path

    let compareStep originFileKeySet comparingFileKeySet=
        let originResult = 
            compare 
                (source,OriginFile,  originFileKeySet)
                (target,CompareeFile,comparingFileKeySet)
        let compareeResult = 
            compare
                (target,CompareeFile,comparingFileKeySet)
                (source,OriginFile,  originFileKeySet)

        let strictStep originFile compareeFile=
            let diffResults = DiffResults_ originFile [compareeFile]
            match originFile,compareeFile with
            | Same _,Same _                                 ->  Ok diffResults
            | Same _, Different(fileArgument,difflineList)
            | Different(fileArgument,difflineList), Same _  -> Error [JamesonFail_ NOT_SAME (Option.Some (Different(fileArgument,difflineList)))]
            | Different(fileArgumentA,difflineListA), Different(fileArgumentB,difflineListB) -> 
                Error[
                    JamesonFail_ NOT_SAME (Option.Some (Different(fileArgumentA,difflineListA)));
                    JamesonFail_ NOT_SAME (Option.Some (Different(fileArgumentB,difflineListB)))
                ]
        HandleResultTuple (originResult,compareeResult) (strictStep)
    HandleResultTuple (originFileKeySetResult,targetFileKeySetResult) compareStep
