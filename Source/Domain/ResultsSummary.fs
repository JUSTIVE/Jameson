module ResultsSummary
open Result

type PartialSuccessType<'a,'b> = {successes: list<Result<'a,'b>>; fails:list<Result<'a,'b>>}

type ResultSummary<'a,'b> = 
    | AllSuccess of list<Result<'a,'b>>
    | AllFail of list<Result<'a,'b>>
    | PartialSuccess of PartialSuccessType<'a,'b>

let Summarize (resultList:list<Result<'a,'b>>) :ResultSummary<'a,'b> = 
    match resultList|>List.forall(isSuccess) with
    | true -> AllSuccess resultList
    | false -> match resultList|>List.forall(isFail) with
        | true -> AllFail resultList
        | false -> PartialSuccess({
                    successes=resultList|>List.filter(isSuccess);
                    fails = resultList|>List.filter(isFail);
                })
    