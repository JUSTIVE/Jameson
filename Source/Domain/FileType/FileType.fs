module FileType

type FileKeySet = Set<string>

type FileType = 
    | OriginFile 
    | CompareeFile

type FileData = string*FileType*FileKeySet

