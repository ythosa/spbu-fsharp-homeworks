module Evaluator.Tests

open NUnit.Framework
open FsUnit
open OperatorExecutor

[<Test>]
let ``test infix plus`` () =
    let inputExpression =
        InfixExpr
            { Operator = InfixOperator.Plus
              Left = Literal 1
              Right = Literal 2 }

    let expectedResult = 3

    eval inputExpression |> should equal expectedResult

[<Test>]
let ``test infix minus`` () =
    let inputExpression =
        InfixExpr
            { Operator = InfixOperator.Minus
              Left = Literal 1
              Right = Literal 2 }

    let expectedResult = -1

    eval inputExpression |> should equal expectedResult


[<Test>]
let ``test infix multiply`` () =
    let inputExpression =
        InfixExpr
            { Operator = InfixOperator.Multiply
              Left = Literal 3
              Right = Literal 2 }

    let expectedResult = 6

    eval inputExpression |> should equal expectedResult


[<Test>]
let ``test infix divide`` () =
    let inputExpression =
        InfixExpr
            { Operator = InfixOperator.Divide
              Left = Literal 4
              Right = Literal 2 }

    let expectedResult = 2

    eval inputExpression |> should equal expectedResult

[<Test>]
let ``test prefix plus`` () =
    let inputExpression =
        PrefixExpr
            { Operator = PrefixOperator.Plus
              Right = Literal 2 }

    let expectedResult = 2

    eval inputExpression |> should equal expectedResult

[<Test>]
let ``test prefix minus`` () =
    let inputExpression =
        PrefixExpr
            { Operator = PrefixOperator.Minus
              Right = Literal 2 }

    let expectedResult = -2

    eval inputExpression |> should equal expectedResult

[<Test>]
let ``test eval nested tree`` () =
    let inputExpression =
        InfixExpr
            { Operator = InfixOperator.Divide
              Left =
                PrefixExpr
                    { Operator = PrefixOperator.Minus
                      Right = Literal 14 }
              Right =
                InfixExpr
                    { Operator = InfixOperator.Multiply
                      Left = Literal 2
                      Right =
                        InfixExpr
                            { Operator = InfixOperator.Plus
                              Left = Literal 3
                              Right = Literal 4 } } }

    let expectedResult = -1

    eval inputExpression |> should equal expectedResult
