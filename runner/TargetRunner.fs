module TargetRunner
open JamesonResult
open JamesonResults

let run (originFilePath:string) (comparingFilePath:string) :JamesonResult= 
    CLEAR