module Homework6.Workflows

open System

type RoundingBuilder(precision: int) =
    member this.Bind(value: float, f) = f <| Math.Round(value, precision)
    member this.Return(value: float) = Math.Round(value, precision)
    member this.ReturnFrom(value: float) = Math.Round(value, precision)

let rounding precision = RoundingBuilder(precision)



type CalculateBuilder() =
    member this.Bind(value: string, f) =
        match Int32.TryParse value with
        | true, number -> f (number)
        | _ -> None

    member this.Return(x) = Some(x)

let calculate = CalculateBuilder()
