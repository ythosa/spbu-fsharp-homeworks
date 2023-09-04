module Homework5.Tests

open Homework5.Computer
open Homework5.LocalNetworkInfection
open Homework5.OS
open NUnit.Framework
open FsUnit

[<Test>]
let ``Test simulation on weak`` () =
    let c1 =
        { Host = "1"
          OS = Weak
          IsInfected = true }

    let c2 =
        { Host = "2"
          OS = Weak
          IsInfected = false }

    let c3 =
        { Host = "3"
          OS = Weak
          IsInfected = false }

    let computers = [ c1; c2; c3 ]
    let connections = [ (c1.Host, [ c2.Host; c3.Host ]) ] |> Map.ofList

    let infectedComputers =
        LocalNetworkInfection(computers, connections).Apply().Infected()

    should equal [ c1; c2.Infect(); c3.Infect() ] infectedComputers

[<Test>]
let ``Test simulation on strong`` () =
    let c1 =
        { Host = "1"
          OS = Strong
          IsInfected = true }

    let c2 =
        { Host = "2"
          OS = Strong
          IsInfected = false }

    let c3 =
        { Host = "3"
          OS = Strong
          IsInfected = false }

    let computers = [ c1; c2; c3 ]
    let connections = [ (c1.Host, [ c2.Host; c3.Host ]) ] |> Map.ofList

    let infectedComputers =
        LocalNetworkInfection(computers, connections).Apply().Infected()

    should equal [ c1 ] infectedComputers
