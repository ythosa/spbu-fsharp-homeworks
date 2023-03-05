let factorial x =
    let rec go x acc =
        match x with
        | 0L -> acc
        | x -> go (x - 1L) (acc * x)

    go x 1L

[<EntryPoint>]
let main _ =
    let fromNumber, toNumber = (0, 15)

    let result =
        ("", [ fromNumber..toNumber ])
        ||> Seq.fold (fun acc i -> acc + $"factorial of {i} equals: %d{factorial i}\n")

    printf $"Result: \n{result}"

    0
