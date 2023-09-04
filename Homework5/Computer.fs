module Homework5.Computer

open System

open Homework5.OS

type Host = string

type Computer =
    { Host: Host
      OS: OS
      IsInfected: bool }

    member computer.Infect() = { computer with IsInfected = true }

    member computer.WillInfected(random: Random) =
        random.NextDouble() > computer.OS.InfectionProbability

    member computer.InfectionProbability = computer.OS.InfectionProbability

    override computer.ToString() =
        sprintf "%s:%A %s" computer.Host computer.OS
        <| if computer.IsInfected then "🥶" else "🙂"
