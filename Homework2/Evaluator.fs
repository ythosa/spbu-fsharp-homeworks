module OperatorExecutor

type InfixOperator =
    | Plus
    | Minus
    | Multiply
    | Divide

let getInfixOperation operator =
    match operator with
    | Plus -> (+)
    | Minus -> (-)
    | Multiply -> (*)
    | Divide -> (/)

type PrefixOperator =
    | Minus
    | Plus

let getPrefixOperation operator =
    match operator with
    | Minus -> (~-)
    | Plus -> (~+)

type PrefixExpression<'a> =
    { Operator: PrefixOperator
      Right: Expression<'a> }

and InfixExpression<'a> =
    { Operator: InfixOperator
      Left: Expression<'a>
      Right: Expression<'a> }

and Expression<'a> =
    | Literal of 'a
    | InfixExpr of InfixExpression<'a>
    | PrefixExpr of PrefixExpression<'a>

let rec eval expression =
    match expression with
    | Literal v -> v
    | InfixExpr { Operator = op; Left = l; Right = r } -> op |> getInfixOperation <| eval l <| eval r
    | PrefixExpr { Operator = op; Right = r } -> op |> getPrefixOperation <| eval r
