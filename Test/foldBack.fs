module Test.foldBack

let foldBack folder source state =
    List.fold (fun s x -> x :: s) [] source
    |> List.fold (fun s x -> folder x s) state
