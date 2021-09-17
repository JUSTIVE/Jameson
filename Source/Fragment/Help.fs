module Help
open JamesonResult
open JamesonResults
open PrinterUtil
open System




let help ():JamesonResult = 
    printLogo()
    $"
jameson <sourceFilePath> <targetFilePath>\tcompare <sourceFile> with <targetFile>
    "
    |>printfn "%s"
    GOOD
    