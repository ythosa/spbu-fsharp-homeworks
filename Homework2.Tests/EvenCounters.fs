module EvenCounters.Tests

open NUnit.Framework
open EvenCounters
open FsCheck

[<Test>]
let CompareEqualityOfEvenCounters () =
    let areAllTheSame lst =
        match lst with
        | [] -> true
        | x :: xs -> List.forall <| (=) x <| xs

    let compareEvenCounters xs =
        [ countEvenUsingFilter; countEvenUsingFold; countEvenUsingMap ]
        |> List.map (fun f -> f xs)
        |> areAllTheSame

    Check.QuickThrowOnFailure compareEvenCounters
