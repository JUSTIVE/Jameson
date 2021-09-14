module FileType

type FileInfo ={
    filename: string;
}

type FileKeySet = Set<string>

type FileType = 
    | OriginFile of FileKeySet 
    | CompareeFile of FileKeySet