namespace Lazy

open Lazy.Naive
open Lazy.Lock
open Lazy.LockFree

type LazyFactory =
    static member SingleThreaded supplier = Naive.Lazy(supplier) :> ILazy<_>
    static member MultiThreaded supplier = Lock.Lazy(supplier) :> ILazy<_>
    static member LockFree supplier = LockFree.Lazy(supplier) :> ILazy<_>
