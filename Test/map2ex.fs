module Test.Map2ex

let rec map2ex (fst: 'a seq) (snd: 'b seq) (fn: 'a -> 'b -> 'c) =
    seq {
        if not ((Seq.isEmpty fst) && (Seq.isEmpty snd)) then
            if Seq.isEmpty fst || Seq.isEmpty snd then
                failwith "lengths of sequences must be equal"

            yield fn (Seq.head fst) (Seq.head snd)

            yield! map2ex (Seq.skip 1 fst) (Seq.skip 1 snd) fn
    }
