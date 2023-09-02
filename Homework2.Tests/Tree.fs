module Tree.Tests

open NUnit.Framework
open FsUnit
open Homework2

let transformer = (*) 2

[<Test>]
let ``with only leaf`` () =
    Leaf(1) |> Tree.map transformer |> should equal <| Leaf(2)

[<Test>]
let ``with only left child`` () =
    let inputValue = Node(1, Leaf(2), Empty)
    let expectedResult = Node(2, Leaf(4), Empty)

    inputValue |> Tree.map transformer |> should equal <| expectedResult

[<Test>]
let ``with only right child`` () =
    let inputValue = Node(1, Empty, Leaf(2))
    let expectedResult = Node(2, Empty, Leaf(4))

    inputValue |> Tree.map transformer |> should equal <| expectedResult

[<Test>]
let ``with both children`` () =
    let inputValue = Node(1, Leaf(2), Leaf(3))
    let expectedResult = Node(2, Leaf(4), Leaf(6))

    inputValue |> Tree.map transformer |> should equal expectedResult

[<Test>]
let ``with complex tree`` () =
    let inputValue = Node(1, Leaf(2), Node(3, Node(4, Leaf(5), Empty), Leaf(6)))
    let expectedResult = Node(2, Leaf(4), Node(6, Node(8, Leaf(10), Empty), Leaf(12)))

    inputValue |> Tree.map transformer |> should equal expectedResult
