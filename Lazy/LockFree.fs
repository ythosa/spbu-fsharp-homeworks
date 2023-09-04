module Lazy.LockFree

open System.Threading

type Lazy<'a>(supplier) =
    let mutable result = None

    interface ILazy<'a> with
        member this.Get() =
            let mutator () =
                Interlocked.CompareExchange(&result, Some <| supplier (), None) |> ignore
                result.Value

            Option.defaultWith mutator result
