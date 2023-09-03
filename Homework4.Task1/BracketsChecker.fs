module Homework4.BracketsChecker

let brackets = Map.empty.Add('}', '{').Add(']', '[').Add(')', '(')
let openBrackets = brackets.Values
let closeBrackets = brackets.Keys

let rec checkBracketsSequence s =
    let rec h (s: list<char>) (stack: list<char>) =
        match s with
        | [] -> stack.Length = 0
        | x :: xs when openBrackets.Contains x -> h xs <| x :: stack
        | x :: xs when closeBrackets.Contains x ->
            match stack with
            | sx :: sxs when brackets[x] = sx -> h xs sxs
            | _ -> false
        | _ :: xs -> h xs stack

    h <| Seq.toList s <| []
