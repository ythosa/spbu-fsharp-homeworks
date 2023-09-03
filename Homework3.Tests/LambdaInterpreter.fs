module Homework3.Tests

open Microsoft.FSharp.Quotations
open NUnit.Framework
open FsUnit

open LambdaInterpreter

let I name = Abstraction(name, Variable(name))

let K name1 name2 =
    Abstraction(name1, Abstraction(name2, Variable(name1)))

let S name1 name2 name3 =
    Abstraction(
        name1,
        Abstraction(
            name2,
            Abstraction(
                name3,
                Application(
                    Application(Variable(name1), Variable(name3)),
                    Application(Variable(name2), Variable(name3))
                )
            )
        )
    )

let omega name =
    Abstraction(name, Application(Variable name, Variable name))

let bigOmega name = Application(omega name, omega name)

let betaReduceTestCases () =
    [ Application(Application(S "x" "y" "z", K "x" "y"), K "x" "y"), (I "z", true)

      I "n", (I "n", true)

      Application(Abstraction("x", Variable("x")), Variable("y")), (Variable("y"), true)

      Application(Abstraction("c", Application(Variable "c", Variable "b")), Abstraction("a", Variable "a")),
      (Variable("b"), true)

      bigOmega "x", (bigOmega "x", false)

      Application(
          Abstraction("f", Abstraction("x", Application(Variable "f", Application(Variable "x", Variable "x")))),
          Variable "+"
      ),
      (Abstraction("x", Application(Variable "+", Application(Variable "x", Variable "x"))), true)
      ]
    |> List.map TestCaseData

[<TestCaseSource(nameof betaReduceTestCases)>]
let ``test term beta reducing`` inputTerm expectedResult =
    betaReduce inputTerm |> should equal expectedResult
