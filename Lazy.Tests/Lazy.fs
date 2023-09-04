module Lazy.Tests

open NUnit.Framework
open FsUnit
open System.Threading

let getLazies supplier =
    [ LazyFactory.LockFree; LazyFactory.SingleThreaded; LazyFactory.MultiThreaded ]
    |> List.map (fun f -> f supplier)

[<Test>]
let ``get tests for all lazies`` () =
    let expectedValue = 1
    let lazies = getLazies (fun () -> expectedValue)
    let values = List.map (fun (l: ILazy<int>) -> l.Get()) <| lazies
    should equal true <| List.forall (fun x -> x = expectedValue) values

let replicateRunAndWait (event: ManualResetEvent) count (lz: ILazy<_>) =
    Seq.replicate count lz
    |> Seq.map (fun lz -> async { return lz.Get() })
    |> Async.Parallel
    |> Async.StartAsTask
    |> fun task ->
        event.Set() |> ignore
        task.Result

let createEventTest count (lazyConstructor: (unit -> _) -> ILazy<_>) =
    use event = new ManualResetEvent(false)

    let action =
        fun () ->
            event.WaitOne() |> ignore
            obj ()

    let lz = lazyConstructor action

    replicateRunAndWait event count lz
    |> Seq.pairwise
    |> Seq.map obj.ReferenceEquals
    |> Seq.reduce (&&)
    |> should equal true

[<Test>]
let ``lock test``() =
    for iteration in 1..10000 do
        let createLockLazy = fun action -> Lock.Lazy(action) :> ILazy<_>
        createEventTest 10 createLockLazy

        let createLockFreeLazy = fun action -> Lock.Lazy(action) :> ILazy<_>
        createEventTest 10 createLockFreeLazy
