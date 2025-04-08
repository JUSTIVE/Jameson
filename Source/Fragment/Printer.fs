module Printer
open FileType
open JamesonResult
open JamesonOption
open PrinterType
open PrinterUtil
open Diff
open System

let printDiffLines show indent difflines =
    let printDiffLine indent diffline =
        match diffline with 
        | Changed changed->
            match changed with
            | Added line ->
                print show indent ConsoleColor.Green true $"Added :\t\t{line.path}"
            | Removed line -> 
                print show indent ConsoleColor.Red true $"Removed :\t{line.path}"
        | __-> ()
        
    let innerPrint() =
        difflines
        |>List.map (printDiffLine indent)
        |>ignore
    innerPrint
    |> showPrint show

let printDiffFile show indent (diffFile:DiffFile) = 
    let innerPrint() =
        match diffFile with 
        | Same filename-> 
            printType show indent $"DiffFile : Same"
            print show indent ConsoleColor.White true $"Filename  : {filename.filename}"
            print show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.DarkGray true $"Path  : {filename.path}"
        | Different (filename,difflines)-> 
            printType show indent $"DiffFile : Different"
            print show indent ConsoleColor.White true $"Filename  : {filename.filename}"
            print show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.DarkGray true $"Path  : {filename.path}"
            printDiffLines show (EmptyChild::indent) difflines
    innerPrint
    |>showPrint show

let printJamesonResult show indent (jamesonResult:JamesonResult) :int = 
    let innerPrint () =
        printEmptyLine()
        printType show indent "JamseonResult"
        let messageColor = 
            match jamesonResult.errorCode = 0 with
            | true -> ConsoleColor.DarkGreen
            | false -> ConsoleColor.Red
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) messageColor "message" print jamesonResult.message 
        printWithOptionName show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.Gray "errorCode" print jamesonResult.errorCode 
    innerPrint
    |> showPrint show
    jamesonResult.errorCode

let printJamesonFail show indent (jamesonFail:JamesonFail.t) :int = 
     let innerPrint () =
         printEmptyLine()
         printType show indent "JamseonFail"
         let messageColor = 
             match jamesonFail.result.errorCode = 0 with
             | true -> ConsoleColor.DarkGreen
             | false -> ConsoleColor.Red
         printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) messageColor "message" print jamesonFail.result.message 
         printWithOptionName show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.Gray "errorCode" print jamesonFail.result.errorCode
         match jamesonFail.reason with
         | Some diffFile->
            printDiffFile show (EmptyChild::indent) diffFile
         | Option.None -> ignore()
          
     innerPrint
     |> showPrint show
     jamesonFail.result.errorCode

let printJamesonFails show indent (jamesonResults:list<JamesonFail.t>) :int =
    jamesonResults
    |>List.map(printJamesonFail show indent)
    |>List.fold (fun x y->if x > y then x else y) 0

let printFileArgument indent (fileArgument:FileArgument) = 
    print true (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.Gray true fileArgument.filename
    print true (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.Gray true fileArgument.path
    
let printTargetRunnerOption show indent (targetRunnerOption:TargetRunnerOption)= 
    let innerPrint () =
        print show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.Gray true "source" 
        printFileArgument (ColorableIndent(MidChild,ConsoleColor.White)::indent) targetRunnerOption.source
        print show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.Gray true "target" 
        printFileArgument (EmptyChild::indent) targetRunnerOption.target
    innerPrint
    |> showPrint show

let printShowRunnerOption show indent showRunnerOption = 
    let innerPrint () =
        print show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.Gray true "source" 
        printFileArgument (ColorableIndent(MidChild,ConsoleColor.White)::indent) showRunnerOption.source
    innerPrint
    |> showPrint show

let printRunnerType show indent consoleColor newline runnerType = 
    let innerPrint () =
        match runnerType with
        | TargetRunnerOption t ->
            printType show indent $"RunnerType : TargetRunnerOption"
            printTargetRunnerOption  show (ColorableIndent(MidChild,ConsoleColor.White)::indent) t
        | GeneralRunnerOption g ->
            printType show indent $"RunnerType : GeneralRunnerOption"
        | ShowRunnerOption s ->
            printType show indent $"RunnerType : ShowRunnerOption"
            printShowRunnerOption  show (ColorableIndent(MidChild,ConsoleColor.White)::indent) s
        | None -> 
            print show indent ConsoleColor.White true "none"
    innerPrint
    |> showPrint show

let printOption show indent consoleColor newline option =
    match option with
    | Option.Some x -> print show indent consoleColor newline x
    | Option.None   -> print show indent ConsoleColor.DarkRed newline "None" 

let printCheckConvention show indent consoleColor newline (checkConvention:CheckConventionType) =
    match checkConvention with
    | NoConvention -> "No Convention"
    | SimpleConventionType convention ->
        match convention with
        | LowerCase -> "Lower Case"
        | UpperCase -> "Upper Case"
    | HeadCharConventionType convention ->
        match convention with
        | CamelCase -> "Camel Case"
        | PascalCase -> "Pascal Case"
    | ComplexConventionType convention ->
        match convention with
        | SnakeCase -> "Snake Case"
    |>sprintf "checkConventionType : %s" 
    |>printType show indent

let printJamesonOption show indent (jamesonOption:JamesonOption.t) =
    let innerPrint () =
        printType show indent "JamseonOption"
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.White "runnerType" printRunnerType jamesonOption.runnerType
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.White "write to file" printOption jamesonOption.writeToFile
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.White "strict" printBool jamesonOption.strict
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.White "verbose" printBool jamesonOption.verbose
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.White "help" printBool jamesonOption.help
        printWithOptionName show (ColorableIndent(MidChild,ConsoleColor.White)::indent) ConsoleColor.White "autoFill" printBool jamesonOption.autoFill
        printWithOptionName show (ColorableIndent(LastChild,ConsoleColor.White)::indent) ConsoleColor.White "checkConvention" printCheckConvention jamesonOption.checkConvention
    innerPrint
    |> showPrint show


