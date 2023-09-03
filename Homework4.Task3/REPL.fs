module Homework4.REPL

open System.IO
open System
open Homework4.PhoneBook
open Microsoft.FSharp.Core

let printContacts (dst: TextWriter) (contacts: list<Contact>) = contacts |> Seq.iter dst.WriteLine

type Command =
    { Name: string
      Description: string
      ArgsCount: int
      Run: PhoneBook -> list<string> -> PhoneBook }

let quitCmd =
    { Name = "q"
      Description = "quit from repl"
      ArgsCount = 0
      Run = fun (_: PhoneBook) (_: list<string>) -> exit 0 }

let addCmd =
    { Name = "add"
      Description = "accepts person name, phone and adds contact to phone book"
      ArgsCount = 2
      Run = fun (phoneBook: PhoneBook) (args: list<string>) -> phoneBook.AddContact <| Contact(args[0], args[1]) }

let searchByNameCmd =
    { Name = "bn"
      Description = "prints all contacts which names starts with passed string"
      ArgsCount = 1
      Run =
        fun (phoneBook: PhoneBook) (args: list<string>) ->
            let found = phoneBook.SearchByName args[0]
            printfn $"Found %d{found.Length} contacts"
            printContacts stdout found

            phoneBook }

let searchByPhoneCmd =
    { Name = "bp"
      Description = "prints all contacts which phones starts with passed string"
      ArgsCount = 1
      Run =
        fun (phoneBook: PhoneBook) (args: list<string>) ->
            let found = phoneBook.SearchByPhone args[0]
            printfn $"Found %d{found.Length} contacts"
            printContacts stdout found

            phoneBook }

let listAllCmd =
    { Name = "all"
      Description = "prints all contacts"
      ArgsCount = 0
      Run =
        fun (phoneBook: PhoneBook) (_: list<string>) ->
            printContacts stdout <| phoneBook.ListContacts()

            phoneBook }

let saveCmd =
    { Name = "s"
      Description = "receives file path and saves all contacts"
      ArgsCount = 1
      Run =
        fun (phoneBook: PhoneBook) (args: list<string>) ->
            use streamWriter = new StreamWriter(args[0])
            printContacts streamWriter <| phoneBook.ListContacts()

            phoneBook }

let loadCmd =
    { Name = "l"
      Description = "receives file path and loads phone book"
      ArgsCount = 1
      Run =
        fun (_: PhoneBook) (args: list<string>) ->
            use reader = new StreamReader(args[0])

            PhoneBook(
                reader,
                (fun str ->
                    let parts = str.Split(" ")

                    if parts.Length <> 2 then
                        failwith $"invalid input = %s{str}, each line in file must have 2 parts separated by space"

                    Contact(parts[0], parts[1]))
            ) }

let commands =
    [ quitCmd
      addCmd
      searchByNameCmd
      searchByPhoneCmd
      listAllCmd
      saveCmd
      loadCmd ]

let helpCmd =
    printfn "Welcome to Phone Book!"

    commands
    |> List.iter (fun cmd -> printfn $"\t%s{cmd.Name} — %s{cmd.Description}")

let parseCmd (input: string) =
    match input.Split(" ") |> Array.toList with
    | h :: tail -> h, tail
    | [] -> failwith "you are missed command"

let rec repl phoneBook =
    try
        Console.Write "> "
        let cmdName, args = parseCmd <| Console.ReadLine()

        match List.tryFind (fun cmd -> cmd.Name = cmdName) commands with
        | None -> printfn "unknown command"
        | Some cmd when args.Length = cmd.ArgsCount -> repl <| cmd.Run phoneBook args
        | Some cmd -> printfn $"invalid args count: expected=%d{cmd.ArgsCount}, actual=%d{args.Length}"
    with Failure msg ->
        printfn $"Oops! %s{msg}"

    repl phoneBook

[<EntryPoint>]
let main _ =
    repl <| PhoneBook()

    0
