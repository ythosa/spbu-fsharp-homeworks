open System

let reverseList list =
    let rec go list acc =
        match list with
        | [] -> acc
        | head :: tail -> go tail (head :: acc)

    go list []

[<EntryPoint>]
let main _ =
    let randomGenerator = Random()

    let generateRandomList (randomGenerator: Random) =
        List.init (randomGenerator.Next() % 10) (fun _ -> randomGenerator.Next() % 100)

    let result =
        List.init 10 (fun _ -> generateRandomList randomGenerator)
        |> List.map (fun i -> $"reversed of %A{i} is %A{reverseList i}")
        |> String.concat "\n"

    printf $"Result: \n{result}"

    0
