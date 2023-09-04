module Test.Tests.map2ex

open System
open NUnit.Framework
open FsUnit

open Test.Map2ex

let infSeqOfX x =
    seq {
        while true do
            yield x
    }

[<Test>]
let ``test infinite seqs`` () =
    let seqOfOnes = infSeqOfX 1
    let seqOfTwos = infSeqOfX 2
    let seqOfThree = infSeqOfX 3

    let mapped = map2ex seqOfOnes seqOfTwos (+)

    should equal <|| (Seq.take 10 seqOfThree, Seq.take 10 mapped)

[<Test>]
let ``test left seq len is less`` () =
    let left = [ 1; 2 ] |> Seq.ofList
    let right = [ 4; 5; 6 ] |> Seq.ofList

    let result = map2ex left right (*)

    (fun () -> ignore <| List.ofSeq result) |> should throw typeof<SequencesMustHaveEqualLengthError>

let ``test rights is infinite`` () =
    let left = seq { 0..10 }
    let right = Seq.initInfinite id

    let result = map2ex left right (*)

    (fun () -> ignore <| List.ofSeq result) |> should throw typeof<SequencesMustHaveEqualLengthError>

let ``test left is infiite`` () =
    let left = Seq.initInfinite id
    let right = seq { 0..10 }

    let result = map2ex left right (*)

    (fun () -> ignore <| List.ofSeq result) |> should throw typeof<SequencesMustHaveEqualLengthError>

[<Test>]
let ``test right seq len is less`` () =
    let left = [ 4; 5; 6 ] |> Seq.ofList
    let right = [ 1; 2 ] |> Seq.ofList

    let result = map2ex left right (*)

    (fun () -> ignore <| List.ofSeq result) |> should throw typeof<SequencesMustHaveEqualLengthError>

[<Test>]
let ``test seqs lengths are equals`` () =
    let left = [ 4; 5; 6 ] |> Seq.ofList
    let right = [ 1; 2; 3 ] |> Seq.ofList

    let result = List.ofSeq <| map2ex left right (*)

    should equal [ 4; 10; 18 ] result

[<Test>]
let ``test empty seqs`` () =
    let result = List.ofSeq <| map2ex [] [] (*)

    should equal Seq.empty result
