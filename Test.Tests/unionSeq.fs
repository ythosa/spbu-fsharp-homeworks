module Test.Tests.unionSeq

open NUnit.Framework
open FsUnit

open Test
open Test.unionSeq

[<Test>]
let ``union seq diff len`` () =
    let left = [ 1; 3; 5 ]
    let right = [ 2; 4 ]
    let expected = List.concat [ left; right ] |> List.sort
    let actual = unionSeq left right |> Seq.toList |> List.sort

    should equal expected actual

[<Test>]
let ``union seq eq len`` () =
    let left = [ 1; 3; 5 ]
    let right = [ 2; 4; 6 ]
    let expected = List.concat [ left; right ] |> List.sort
    let actual = unionSeq left right |> Seq.toList |> List.sort

    should equal expected actual

let ``union seq empty`` () =
    let left = [ 1 ]
    let right = []

    should equal [ 1 ] <| unionSeq left right
