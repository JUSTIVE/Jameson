module PathResolver
open LanguageSupport

let getFilePathList (path:string):list<string> = 
    System.IO.Directory.GetFiles(path,SEARCH_PATTERN)
    |>Array.toList