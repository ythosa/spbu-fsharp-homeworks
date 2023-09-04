module Test2.PointFree

let rec tail l =
    match l with
    | [] -> []
    | _ -> List.tail l

let func g l = List.map g (tail l)

let func' g = List.map g << tail

let func'' g = (<<) (List.map g) tail

type f<'a, 'b> = ('a -> 'b) -> 'a list -> 'b list
let func''': f<int, int> = ((>>) tail) << List.map
