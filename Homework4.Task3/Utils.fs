module Homework4.Utils

open System.IO

let readLines =
    Seq.unfold (fun (reader: TextReader) ->
        match reader.ReadLine() with
        | null -> None
        | str -> Some(str, reader))
