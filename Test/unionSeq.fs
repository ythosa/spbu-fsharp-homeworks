module Test.unionSeq

let seqTail (s: 'a seq) =
    if (Seq.isEmpty s) then Seq.empty else Seq.tail s

let rec unionSeq (fst: 'a seq) (snd: 'a seq) =
    seq {
        match (Seq.isEmpty fst, Seq.isEmpty snd) with
        | true, false -> yield! snd
        | false, true -> yield! fst
        | false, false ->
            yield Seq.head fst
            yield Seq.head snd
            yield! unionSeq (Seq.tail fst) (Seq.tail snd)
        | true, true -> ()
    }
