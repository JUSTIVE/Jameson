# Jameson
<a href="https://gitmoji.dev">
  <img src="https://img.shields.io/badge/gitmoji-%20ð%20ð-FFDD67.svg?style=flat-square" alt="Gitmoji">
</a>  

[![Build](https://github.com/JUSTIVE/Jameson/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/JUSTIVE/Jameson/actions/workflows/dotnet.yml)

## Jameson `\[/ËdÊeÉªmÉsÉn/\]`  
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
|-o \<outputDirectory\>|dump diff file to \<outputDirectory\>|ð«|
|-c \<camel,pascal,upper,lower\>|check key naming convention with given convention type|ð«|
|--s|compare strictly. will return 0 only when all the files have same key|â|
|--v|show log verbosely|â|
|--h|show help|â|
|--f|auto generate missing keys|ð«|
