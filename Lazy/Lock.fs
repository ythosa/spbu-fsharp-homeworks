module Lazy.Lock

type Lazy<'a>(supplier) =
    let locker = obj ()

    let mutable result = None

    let lockCheckAndCompute () =
        let mutator () =
            result <- Some <| supplier ()
            result.Value

        lock locker <| (fun () -> Option.defaultWith mutator result)

    interface ILazy<'a> with
        member this.Get() =
            Option.defaultWith lockCheckAndCompute result
