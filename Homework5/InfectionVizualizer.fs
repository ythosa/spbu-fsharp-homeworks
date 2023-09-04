module Homework5.InfectionVizualizer

open Homework5.Computer
open Homework5.OS
open Homework5.LocalNetworkInfection

type InfectionVizualizer(network: LocalNetworkInfection) =
    member this.Vizualize() =
        let rec vizualize (network: LocalNetworkInfection) step =
            InfectionVizualizer.PrintNetwork(network, step)

            if network.IsMutable() then
                vizualize <| network.Apply() <| (step + 1)

        vizualize network 0

    static member PrintNetwork(network: LocalNetworkInfection, step) =
        printfn $"===Step %d{step}==="

        network.Computers()
        |> List.iter (fun computer -> printfn $"$s{computer.ToString()}")

        printfn ""

let c1 =
    { Host = "1"
      OS = Weak
      IsInfected = true }

let c2 =
    { Host = "2"
      OS = MacOS
      IsInfected = false }

let c3 =
    { Host = "3"
      OS = Windows
      IsInfected = false }

let c4 =
    { Host = "4"
      OS = Linux
      IsInfected = false }

let c5 =
    { Host = "5"
      OS = Weak
      IsInfected = false }

let c6 =
    { Host = "6"
      OS = Strong
      IsInfected = false }

let network =
    Map
        [ (c1.Host, [ c2.Host ])
          (c2.Host, [ c3.Host; c4.Host ])
          (c4.Host, [ c5.Host ])
          (c5.Host, [ c6.Host ]) ]

let comps = [ c1; c2; c3; c4; c5; c6 ]

InfectionVizualizer(LocalNetworkInfection(comps, network)).Vizualize()
