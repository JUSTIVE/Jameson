module ArgumentParser
open JamesonOption
open JamesonOptions
open JamesonResult
open JamesonResults
open State

type SourceResolveInfo = {
    runnerType:RunnerType;
    restArgument:list<string>
}

type SourceResolveResult = Result<SourceResolveInfo,JamesonResult>
type SourceResolveState = ReduceState<SourceResolveResult>

let rec sourceResolver (state:ReduceState<SourceResolveResult>)(argument:list<string>):SourceResolveResult = 
    let RETURN_WITH_ARGUMENT_LENGTH_ERROR_WHEN_INIT (state:ReduceState<SourceResolveResult>) = 
        match state with 
        | SourceResolveState.Init -> SourceResolveResult.Fail(ARGUMENT_LENGTH_ERROR)
        | SourceResolveState.NonInit(result)-> result
    match argument with
    | [] -> RETURN_WITH_ARGUMENT_LENGTH_ERROR_WHEN_INIT state
    | h::t -> 
        match h with
        | "-o" -> RETURN_WITH_ARGUMENT_LENGTH_ERROR_WHEN_INIT state
        | __ -> 
            match state with
            | SourceResolveState.Init -> 
                SourceResolveResult.Success({
                    runnerType=GeneralRunner({
                        sourcePath=h;
                        targetCandidate=List.empty<string>
                    });
                    restArgument=t
                })
            | SourceResolveState.NonInit(result)->
                match result with
                | SourceResolveResult.Fail(x) as result -> result
                | SourceResolveResult.Success({runnerType=runnerType;restArgument=restArgument})->
                    match runnerType with
                    | TargetRunner(x)-> SourceResolveResult.Fail(ARGUMENT_LENGTH_ERROR)
                    | GeneralRunner({sourcePath=sourcePath;targetCandidate=targetCandidate})-> 
                        sourceResolver
                        <|  SourceResolveState.NonInit(
                                SourceResolveResult.Success({
                                    runnerType=TargetRunner({
                                        sourcePath = sourcePath;
                                        targetPath = h
                                    });
                                    restArgument = t
                                })
                            )
                        <| t

type ParseResult = Result<JamesonOption,JamesonResult>

let rec argumentResolver (result:ParseResult) (argument:list<string>):ParseResult =
    match result with
    | ParseResult.Success(x) ->
        match argument with
        | h::t -> argumentResolver result t
        | __ -> result
    | ParseResult.Fail(x) -> ParseResult.Fail(x)

let parse (argument:string[]):ParseResult = 
    let sourceResult = 
        sourceResolver 
        <| SourceResolveState.Init 
        <| Array.toList argument 
    match sourceResult with
    | SourceResolveResult.Success({runnerType=runnerType;restArgument=restArgument}) ->
        argumentResolver 
        <| ParseResult.Success(OPTION_DEFAULT runnerType)
        <| restArgument
    | SourceResolveResult.Fail(jamesonResult:JamesonResult) -> 
        ParseResult.Fail(jamesonResult)