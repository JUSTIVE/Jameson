module ResultsSummary
open Result

type PartialSuccessType<'success,'failed> = {
    successes: list<Result<'success,'failed>>;
    fails:list<Result<'success,'failed>>
}

type ResultSummary<'success,'failed> = 
    | AllSuccess of list<Result<'success,'failed>>
    | AllFail of list<Result<'success,'failed>>
    | PartialSuccess of PartialSuccessType<'success,'failed>

let Summarize (resultList:list<Result<'success,'failed>>)
    :ResultSummary<'success,'failed> = 
    match resultList|>List.forall(isSuccess) with
    | true -> AllSuccess resultList
    | false ->
        match resultList|>List.forall(isFail) with
        | true -> AllFail resultList
        | false ->
            PartialSuccess({
                successes=resultList|>List.filter(isSuccess);
                fails = resultList|>List.filter(isFail);
            })