module Help
open JamesonResult
open JamesonResults
open PrinterUtil
open System



let printUsage() = 
    $"usage :

    jameson -t <sourceFilePath> <targetFilePath>\tcompare <sourceFile> with <targetFile>
    jameson -g <sourceFilePath> \t\t\tcompare <sourceFile> with all neighbour files
    jameson -s <sourceFilePath> \t\t\tshow all keys of <sourceFile>

options : 
    
    -o <outputDirectory>\t\tdump diff file to <outputDirectory>
    --s\t\t\t\t\tcompare strictly. will return 0 only when all the files have same key
    --v\t\t\t\t\tshow log verbosely
    --h\t\t\t\t\tshow help
        "
    |>printfn "%s"

let help ():JamesonResult = 
    printLogo()
    printUsage()
    GOOD
    