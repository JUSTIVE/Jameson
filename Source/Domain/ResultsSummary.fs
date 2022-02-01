module ResultsSummary
open Result

type PartialSuccessType<'success,'failed,'reason> = {
    successes: list<Result<'success,'failed,'reason>>;
    fails:list<Result<'success,'failed,'reason>>
}

type ResultSummary<'success,'failed,'reason> = 
    | AllSuccess of list<Result<'success,'failed,'reason>>
    | AllFail of list<Result<'success,'failed,'reason>>
    | PartialSuccess of PartialSuccessType<'success,'failed,'reason>

let Summarize (resultList:list<Result<'success,'failed,'reason>>)
    :ResultSummary<'success,'failed,'reason> = 
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