# Jameson

Jameson`\[/ˈdʒeɪməsən/\]`  
are they same JSON files?

Jameson is yet-another JSON key comparing/validate tool.  
Runs on .Net Core which means runs on major OS platforms as well.  
Jameson compares only keys, usually for validating i18n files

## Usage

    jameson -t <sourceFilePath> : <targetFilePath>\tcompare <sourceFile> with <targetFile>
    jameson -g <sourceFilePath> : compare <sourceFile> with all neighbour files
    jameson -s <sourceFilePath> : show all keys of <sourceFile>
options : 
|options | descriptions|
|---|---|
|-o \<outputDirectory\>|dump diff file to \<outputDirectory\>|
|--s|compare strictly. will return 0 only when all the files have same key|
|--v|show log verbosely|
|--h|show help|
