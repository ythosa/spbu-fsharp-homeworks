module Test.Tests.foldBack

open NUnit.Framework
open FsCheck

open Test.foldBack

[<Test>]
let ``check fold back`` () =
    Check.QuickThrowOnFailure(fun folder source state ->
        (foldBack folder source state) = (List.foldBack folder source state))
