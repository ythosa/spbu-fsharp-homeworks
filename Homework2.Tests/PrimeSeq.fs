module PrimeSeq.Tests

open NUnit.Framework
open FsUnit

let seqData () =
    [ 1, [ 2 ] |> seq
      3, [ 2; 3; 5 ] |> seq
      5, [ 2; 3; 5; 7; 11 ] |> seq
      10, [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29 ] |> seq ]
    |> List.map TestCaseData

[<TestCaseSource(nameof seqData)>]
let ``generate prime seq test`` lenght seq =
    generatePrimeSeq() |> Seq.take lenght |> should equal seq
