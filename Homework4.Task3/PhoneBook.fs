module Homework4.PhoneBook

open System.IO
open System
open Homework4.Utils

type Contact(name: string, phoneNumber: string) =
    do
        if not <| Seq.forall Char.IsDigit phoneNumber then
            failwith "incorrect phone number"

    member this.Name = name
    member this.PhoneNumber = phoneNumber

    override this.ToString() = $"name: %s{this.Name}, phone number: %s{this.PhoneNumber}"


type PhoneBook(contacts: list<Contact>) =
    member this.AddContact(contact) = PhoneBook(contact :: contacts)

    member this.SearchByName = this.SearchByField(fun contact -> contact.Name)

    member this.SearchByPhone = this.SearchByField(fun contact -> contact.PhoneNumber)

    member this.SearchByField (fieldGetter: Contact -> string) (pattern: string) =
        contacts
        |> List.filter (fun contact -> fieldGetter(contact).StartsWith(pattern))

    member this.ListContacts() = List.rev contacts

    new(reader: TextReader, parseLine: string -> Contact) =
        PhoneBook(readLines reader |> Seq.map parseLine |> Seq.toList)

    new() = PhoneBook([])
