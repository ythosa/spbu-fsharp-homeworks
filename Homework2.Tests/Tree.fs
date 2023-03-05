module Tree.Tests

open NUnit.Framework
open FsUnit
open Homework2

let mapper = (*) 2

[<Test>]
let ``with only leaf`` () =
    Leaf(1) |> Tree.map mapper |> should equal <| Leaf(2)

[<Test>]
let ``with only left child`` () =
    Node(1, Leaf(2), Empty) |> Tree.map mapper |> should equal
    <| Node(2, Leaf(4), Empty)

[<Test>]
let ``with only right child`` () =
    Node(1, Empty, Leaf(2)) |> Tree.map mapper |> should equal
    <| Node(2, Empty, Leaf(4))

[<Test>]
let ``with both children`` () =
    let inputValue = Node(1, Leaf(2), Leaf(3))
    let expectedResult = Node(2, Leaf(4), Leaf(6))

    inputValue |> Tree.map mapper |> should equal expectedResult

[<Test>]
let ``with complex tree`` () =
    let inputValue = Node(1, Leaf(2), Node(3, Node(4, Leaf(5), Empty), Leaf(6)))
    let expectedValue = Node(2, Leaf(4), Node(6, Node(8, Leaf(10), Empty), Leaf(12)))

    inputValue |> Tree.map mapper |> should equal expectedValue
