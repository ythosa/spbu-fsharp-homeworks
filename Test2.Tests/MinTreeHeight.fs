module Test2.Tests.MinTreeHeight

open NUnit.Framework
open FsUnit

open Test2.MinTreeHeight

[<Test>]
let ``test all heights eqs`` () =
    let tree = Node(Node(Empty, Empty), Node(Empty, Empty))

    should equal 1 <| minTreeHeight tree

[<Test>]
let ``test empty tree`` () = should equal 0 <| minTreeHeight Empty

[<Test>]
let ``test height 0`` () =
    should equal 0 <| minTreeHeight (Node(Empty, Empty))

[<Test>]
let ``test complex height`` () =
    let tree =
        Node(
            Node(
                Node(
                    Empty,
                    Node(Empty, Empty)
                ),
                Node(
                    Empty,
                    Node(Empty, Empty))
                ),
            Node(
                Empty,
                Node(Empty, Empty)))

    should equal 2 <| minTreeHeight tree
