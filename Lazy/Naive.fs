module Lazy.Naive

type Lazy<'a>(supplier) =
    let mutable result = None

    interface ILazy<'a> with
        member this.Get() =
            Option.defaultWith
                (fun () ->
                    let localResult = supplier ()

                    result <- Some localResult
                    localResult)
                result
