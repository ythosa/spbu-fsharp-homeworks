module Homework4.Task3.Tests

open System
open Homework4
open NUnit.Framework
open FsUnit
open PhoneBook

let testContact1 = Contact("kek", "123")
let testContact2 = Contact("lol", "456")

[<Test>]
let ``test new valid contact`` () =
    let name = "bebra"
    let phoneNumber = "1"

    let contact = Contact(name, phoneNumber)

    should equal name contact.Name
    should equal phoneNumber contact.PhoneNumber

[<Test>]
let ``test new invalid contact`` () =
    (fun () -> ignore <| Contact("kek", "lol")) |> should throw typeof<Exception>

[<Test>]
let ``test add contact`` () =
    let contacts = PhoneBook([]).AddContact(testContact1).ListContacts()

    should haveLength 1 contacts
    should equal testContact1 contacts.Head

[<Test>]
let ``test search by name`` () =
    let phoneBook = PhoneBook().AddContact(testContact1).AddContact(testContact2)
    let foundContacts = phoneBook.SearchByName("k")

    should haveLength 1 foundContacts
    should equal foundContacts[0] testContact1

[<Test>]
let ``test search by phone number`` () =
    let phoneBook = PhoneBook().AddContact(testContact1).AddContact(testContact2)
    let foundContacts = phoneBook.SearchByPhone("4")

    should haveLength 1 foundContacts
    should equal foundContacts[0] testContact2

let ``test list`` () =
    let contacts = PhoneBook().AddContact(testContact1).AddContact(testContact2).ListContacts()

    should haveLength 2 contacts
    should equal contacts[0] testContact1
    should equal contacts[1] testContact2
