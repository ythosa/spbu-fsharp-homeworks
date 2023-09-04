module Homework5.LocalNetworkInfection

open System
open Homework5.Computer

type LocalNetworkInfection private (computersByHost: Map<Host, Computer>, conns: Map<Host, List<Host>>) =
    let random = Random()

    let neighbours h =
        let hasValue, value = conns.TryGetValue h
        if hasValue then value else List.Empty

    let infectedNeighbours =
        neighbours
        >> List.filter (fun h -> computersByHost[h].WillInfected random)
        |> Seq.collect

    new(computers: Computer list, conns) =
        LocalNetworkInfection(computers |> List.map (fun c -> c.Host, c) |> Map.ofList, conns)

    member private this.GrepWillInfected() =
        let infected =
            computersByHost.Values
            |> Seq.choose (fun c -> if c.IsInfected then Some(c.Host) else None)

        let neighbours = infectedNeighbours infected

        Set.ofSeq neighbours

    member this.Computers() = computersByHost.Values |> Seq.toList

    member this.Apply() =
        let willInfected = this.GrepWillInfected()

        let computersByHost =
            computersByHost.Keys
            |> Seq.map (fun h ->
                h,
                if willInfected.Contains h then
                    computersByHost[h].Infect()
                else
                    computersByHost[h])
            |> Map.ofSeq

        LocalNetworkInfection(computersByHost, conns)

    member this.IsMutable() =
        Seq.exists (fun c -> (not c.IsInfected) && c.InfectionProbability < 1) computersByHost.Values

    member this.Infected() =
        Seq.filter (fun c -> c.IsInfected) computersByHost.Values
