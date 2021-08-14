module PathResolver

let getFilePathList (path:string):list<string> = 
    System.IO.Directory.GetFiles(path,LanguageSupport.JSON_PATTERN)
    |>Array.toList