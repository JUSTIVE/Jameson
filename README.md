# Jameson
<a href="https://gitmoji.dev">
  <img src="https://img.shields.io/badge/gitmoji-%20😜%20😍-FFDD67.svg?style=flat-square" alt="Gitmoji">
</a>  

## Jameson `\[/ˈdʒeɪməsən/\]`  
are they same JSON files?

Jameson is yet-another JSON key comparing/validate tool.  
Runs on .Net Core which means runs on major OS platforms as well.  
Jameson compares only keys, usually for validating i18n files

## Usage

    jameson -t <sourceFilePath> <targetFilePath> : compare <sourceFile> with <targetFile>
    jameson -g <sourceFilePath>                  : compare <sourceFile> with all neighbour files
    jameson -w <sourceFilePath>                  : show all keys of <sourceFile>
options : 
|options | descriptions|implemented|
|---|---|---|
|-o \<outputDirectory\>|dump diff file to \<outputDirectory\>|🚫|
|-c \<camel,pascal,upper,lower\>|check key naming convention with given convention type|🚫|
|--s|compare strictly. will return 0 only when all the files have same key|✅|
|--v|show log verbosely|✅|
|--h|show help|✅|
|--f|auto generate missing keys|🚫|
