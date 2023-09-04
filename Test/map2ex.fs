module Test.Map2ex

let inline xor a b = (a || b) && not (a && b)


let rec map2ex (fst: 'a seq) (snd: 'b seq) (fn: 'a -> 'b -> 'c) =
    seq {
        if not ((Seq.isEmpty fst) && (Seq.isEmpty snd)) then
            if Seq.isEmpty fst || Seq.isEmpty snd then
                failwith "lengths of sequences must be equal"

            yield fn (Seq.head fst) (Seq.head snd)

            yield! map2ex (Seq.skip 1 fst) (Seq.skip 1 snd) fn
    }

let seqOfOnes =
    seq {
        while true do
            yield 1
    }

let seqOfTwos =
    seq {
        while true do
            yield 2
    }

// ТЕБЕ НЕ ПОКАЖУ!!!! ТЫ ПИСЬКА !!!! КУДА СМОТРИШЬ!!!

let s1 = [ 1; 2; 3 ] |> Seq.ofList
let s2 = [ 4; 5; 6; 7 ] |> Seq.ofList
// let kek = map2ex s1 s2 <| (*)
let kek = map2ex seqOfOnes seqOfTwos <| (+)

printf "%A" <| Seq.take 3 kek
