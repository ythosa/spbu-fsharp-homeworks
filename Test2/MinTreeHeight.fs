module Test2.MinTreeHeight

type Tree =
    | Node of Tree * Tree
    | Empty

let rec minTreeHeight tree =
    let rec h tree currentHeight =
        match tree with
        | Node(Empty, Empty)
        | Empty -> currentHeight
        | Node(Node _ as left, Empty) -> h left (currentHeight + 1)
        | Node(Empty, (Node _ as right)) -> h right (currentHeight + 1)
        | Node(left, right) -> min (h left currentHeight + 1) (h right currentHeight + 1)

    h tree 0
