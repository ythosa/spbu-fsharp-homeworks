let find source element =
    let rec go source element currentIndex =
        match source with
        | [] -> None
        | head :: _ when head = element -> Some currentIndex
        | _ :: tail -> go tail element (currentIndex + 1)

    go source element 0

[<EntryPoint>]
let main _ =
    printf $"%A{find [ 0..100 ] 10}\n"
    printf $"%A{find [ 0..100 ] 1001}"

    0
