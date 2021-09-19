module Help
open JamesonResult
open JamesonResults
open PrinterUtil
open System



let printUsage() = 
    $"usage :
    jameson <sourceFilePath> <targetFilePath>\tcompare <sourceFile> with <targetFile>
    jameson <sourceFilePath> \t\t\tcompare <sourceFile> with all neighbourFiles
    
        "
    |>printfn "%s"

let help ():JamesonResult = 
    printLogo()
    printUsage()
    GOOD
    