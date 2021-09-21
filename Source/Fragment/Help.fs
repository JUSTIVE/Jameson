module Help
open JamesonResult
open JamesonResults
open PrinterUtil
open System



let printUsage() = 
    $"usage :

    jameson -t <sourceFilePath> <targetFilePath>\tcompare <sourceFile> with <targetFile>
    jameson -g <sourceFilePath> \t\t\tcompare <sourceFile> with all neighbour files

options : 
    
    -o <outputDirectory>\t\tdump diff file to <outputDirectory>
    -v\t\t\t\t\tshow log verbosely
    -h\t\t\t\t\tshow help
        "
    |>printfn "%s"

let help ():JamesonResult = 
    printLogo()
    printUsage()
    GOOD
    