module Homework4.Task1.Tests

open NUnit.Framework
open FsUnit

open Homework4.BracketsChecker

let checkBracketsSequenceTestCases () =
    [ "", true
      "([)]", false
      "(({}))", true
      "{[)]:(", false
      "((hi!!)){[]}{}", true
      "([{}](){[]})", true ]
    |> List.map TestCaseData

[<TestCaseSource(nameof checkBracketsSequenceTestCases)>]
let ``Bracket sequence test`` string result =
    checkBracketsSequence string |> should equal result
