type LambdaTerm =
    | Variable of name: string
    | Application of leftTerm: LambdaTerm * rightTerm: LambdaTerm
    | Abstraction of variable: string * innerTerm: LambdaTerm

let rec freeVariableNames t =
    match t with
    | Variable x -> Set.singleton x
    | Application(t1, t2) -> freeVariableNames t1 + freeVariableNames t2
    | Abstraction(x, t) -> freeVariableNames t |> Set.remove x

let rec toVacantVariableName name (names: Set<string>) =
    match names.Contains name with
    | true -> toVacantVariableName << (+) "'" <| name <| names
    | false -> name

let rec substitute (targetTerm: LambdaTerm) (variable: string) (withTerm: LambdaTerm) =
    match targetTerm with
    | Variable name when name = variable -> withTerm
    | Variable _ -> targetTerm
    | Application(left, right) -> Application(substitute left variable withTerm, substitute right variable withTerm)
    | Abstraction(boundedVariable, _) when boundedVariable = variable -> targetTerm
    | Abstraction(boundedVariable, innerTerm) ->
        let freeNamesInner = freeVariableNames innerTerm
        let freeNamesWith = freeVariableNames withTerm

        let isNameConflict =
            freeNamesInner.Contains variable && freeNamesWith.Contains boundedVariable

        if isNameConflict then
            let newBoundedVariable =
                toVacantVariableName boundedVariable <| freeNamesInner + freeNamesWith

            let newInnerTerm =
                substitute innerTerm boundedVariable (Variable newBoundedVariable)

            Abstraction(newBoundedVariable, substitute newInnerTerm variable withTerm)
        else
            Abstraction(boundedVariable, substitute innerTerm variable withTerm)



let betaReduce term =
    let rec h term (seenTerms: Set<LambdaTerm>) =
        match seenTerms.Contains term with
        | true -> term, false
        | _ ->
            match term with
            | Variable _ -> term, true
            | Application(Abstraction(name, innerTerm), t2) -> h <| substitute innerTerm name t2 <| seenTerms.Add(term)
            | Application(Application _ as firstTerm, secondTerm) ->
                h
                <| Application(fst << h firstTerm <| seenTerms.Add(term), secondTerm)
                <| seenTerms.Add(term)
            | Application(firstTerm, secondTerm) ->
                Application(firstTerm, fst << h secondTerm <| seenTerms.Add(term)), true
            | Abstraction(name, innerTerm) -> Abstraction(name, fst << h innerTerm <| seenTerms.Add(term)), true

    h term Set.empty
