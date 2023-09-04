module Homework5.OS

type OS =
    | MacOS
    | Linux
    | Windows
    | Weak
    | Strong

    member os.InfectionProbability: float =
        match os with
        | Weak -> 0
        | MacOS -> 0.25
        | Linux -> 0.5
        | Windows -> 0.75
        | Strong -> 1

    override os.ToString() =
        match os with
        | Weak -> "Weak"
        | MacOS -> "MacOS"
        | Linux -> "Linux"
        | Windows -> "Windows"
        | Strong -> "Strong"
