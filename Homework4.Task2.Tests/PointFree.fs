module Homework4.Task2.Tests

open NUnit.Framework
open FsCheck

open Homework4.PointFree

let compareWithOrigin f x l = (f x l) = (func x l)

[<Test>]
let ``first step is correct``() =
    Check.QuickThrowOnFailure <| compareWithOrigin func'

[<Test>]
let ``second step is correct``() =
    Check.QuickThrowOnFailure <| compareWithOrigin func''

[<Test>]
let ``third step is correct``() =
    Check.QuickThrowOnFailure <| compareWithOrigin func'''
