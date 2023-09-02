module EvenCounters

let revMod2 = (-) 1 << (&&&) 1
let countEvenUsingMap = List.sum << List.map revMod2
let countEvenUsingFold = List.fold (fun acc e -> acc + revMod2 e) 0
let countEvenUsingFilter = (=) 0 << (&&&) 1 |> List.filter >> List.length
