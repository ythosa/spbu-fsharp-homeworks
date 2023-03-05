let getListOfPowersOfTwo n m =
    ([ pown 2 n ], [ 1..m ])
    ||> Seq.fold (fun acc _ -> (*) 2 acc.Head :: acc)
    |> List.rev


[<EntryPoint>]
let main _ =
    printf $"%A{getListOfPowersOfTwo 4 10}\n"
    printf $"%A{getListOfPowersOfTwo 1 3}\n"
    printf $"%A{getListOfPowersOfTwo 0 1}\n"
    printf $"%A{getListOfPowersOfTwo 4 0}\n"

    0
