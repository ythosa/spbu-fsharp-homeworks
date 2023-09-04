module Test2.Tests.PointFree

open NUnit.Framework
open FsCheck

open Test2.PointFree

let eqOriginal f g l = (func g l) = (f g l)

[<Test>]
let ``test first`` () =
    Check.QuickThrowOnFailure(eqOriginal func')

[<Test>]
let ``test second`` () =
    Check.QuickThrowOnFailure(eqOriginal func'')

[<Test>]
let ``test third`` () =
    Check.QuickThrowOnFailure(eqOriginal func''')
