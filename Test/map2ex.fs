module Test.Map2ex

open System

type SequencesMustHaveEqualLengthError() =
    inherit Exception("lengths of sequences must be equal")

let rec map2ex (fst: 'a seq) (snd: 'b seq) (fn: 'a -> 'b -> 'c) =
    seq {
        match (Seq.isEmpty fst, Seq.isEmpty snd) with
        | true, false
        | false, true -> raise <| SequencesMustHaveEqualLengthError()
        | false, false ->
            yield fn (Seq.head fst) (Seq.head snd)
            yield! map2ex (Seq.skip 1 fst) (Seq.skip 1 snd) fn
        | _ -> ()
    }
