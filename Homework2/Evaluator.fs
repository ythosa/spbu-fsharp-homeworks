module OperatorExecutor

type BinaryOperator =
    | Plus
    | Minus
    | Multiply
    | Divide

type UnaryOperator =
    | UnaryMinus
    | UnaryPlus

type Expression<'a> =
    | Value of 'a
    | BinaryExpr of BinaryOperator * Expression<'a> * Expression<'a>
    | UnaryExpr of UnaryOperator * Expression<'a>

let rec eval expression =
    let getBinaryOperationByOperator op =
        match op with
        | Plus -> (+)
        | Minus -> (-)
        | Multiply -> (*)
        | Divide -> (/)

    let getUnaryOperationByOperator op =
        match op with
        | UnaryMinus -> (~-)
        | UnaryPlus -> (~+)

    match expression with
    | Value v -> v
    | BinaryExpr (op, l, r) -> op |> getBinaryOperationByOperator <| eval l <| eval r
    | UnaryExpr (op, e) -> op |> getUnaryOperationByOperator <| eval e

let exp =
    BinaryExpr(Multiply, BinaryExpr(Plus, Value(1), UnaryExpr(UnaryMinus, Value(2))), UnaryExpr(UnaryPlus, Value(3)))

printf "%d" <| (eval <| exp)
