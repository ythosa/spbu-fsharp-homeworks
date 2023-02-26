type Matrix2x2(a11: int, a12: int, a21: int, a22: int) =
    let data = [ [ a11; a12 ]; [ a21; a22 ] ]

    member this.Power(n: int) =
        match n with
        | 0 -> Matrix2x2(1, 0, 0, 1)
        | 1 -> this
        | _ ->
            let powered = this.Power <| n / 2

            if n % 2 = 0 then
                powered * powered
            else
                powered * powered * this

    member _.GetRow(index: int) = data[index][*]
    member _.GetColumn(index: int) = data[*][index]
    member _.GetElement(rowIndex: int, columnIndex: int) = data[rowIndex][columnIndex]

    override _.ToString() =
        $"[%d{data[0][0]}, %d{data[0][1]}]\n[%d{data[1][0]}, %d{data[1][1]}]"

    static member (*)(m1: Matrix2x2, m2: Matrix2x2) =
        Matrix2x2(
            Matrix2x2.ScalarProduct (m1.GetRow 0) (m2.GetColumn 0),
            Matrix2x2.ScalarProduct (m1.GetRow 0) (m2.GetColumn 1),
            Matrix2x2.ScalarProduct (m1.GetRow 1) (m2.GetColumn 0),
            Matrix2x2.ScalarProduct (m1.GetRow 1) (m2.GetColumn 1)
        )

    static member private ScalarProduct first second = List.map2 (*) first second |> List.sum

exception InvalidFibonacciNumberIndex of int

let countFibonacciNumberByIndex n =
    match n with
    | _ when n < 0 -> raise (InvalidFibonacciNumberIndex n)
    | 0 -> 0
    | n -> Matrix2x2(0, 1, 1, 1).Power(n).GetElement(0, 1)

[<EntryPoint>]
let main _ =
    let fromIndex, toIndex = (0, 10)

    let result =
        ("", [ fromIndex..toIndex ])
        ||> Seq.fold (fun acc i -> acc + $"#%d{i}, number: %d{countFibonacciNumberByIndex i}\n")

    printf $"Fibonacci numbers {fromIndex}..{toIndex}: \n{result}"

    0
