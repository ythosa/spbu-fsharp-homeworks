namespace Homework2

type Tree<'a> =
    | Node of value: 'a * left: Tree<'a> * right: Tree<'a>
    | Leaf of value: 'a
    | Empty

module Tree =
    let rec map transform tree =
        match tree with
        | Node (value, left, right) -> Node(transform value, map transform left, map transform right)
        | Leaf value -> Leaf(transform value)
        | Empty -> Empty
