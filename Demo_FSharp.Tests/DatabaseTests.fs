namespace Demo_FSharp.Tests

open System
open System.Linq
open Xunit
open Demo_FSharp.Database
open Demo_FSharp.Person

type DatabaseTests() =

    // Test for the AddPerson method
    [<Fact>]
    member this.``AddPerson_ShouldLogPersonAdded`` () =
        let database = Database()
        let person = Person("Max Mustermann", 30, "Bad Mergentheim")
        
        database.AddPerson(person)
        let res = database.GetRecords() |> Seq.toList  // Konvertiert die .NET-Liste in eine F#-Liste

        Assert.Equal(1, res.Length) // Anzahl der Personen in der Datenbank
        Assert.Equal("Max Mustermann", res.[0].Name) // Überprüfen des Namens der Person

    // Test for the FilterPeopleByAgeRange method
    [<Fact>]
    member this.``FilterPeopleByAgeRange_ShouldReturnCorrectResults`` () =
        let database = Database()
        database.AddPerson(Person("Max Mustermann", 30, "Bad Mergentheim"))
        database.AddPerson(Person("Erika Musterfrau", 40, "Bad Mergentheim"))

        database.FilterPeopleByAgeRange(30, 35)
        let filteredPeople = database.GetRecords() |> Seq.toList

        Assert.Equal(2, filteredPeople.Length) // Erwartet zwei Personen in der gefilterten Liste
        Assert.Equal("Max Mustermann", filteredPeople.[0].Name)

    // Test for the SortPeopleByAge method
    [<Fact>]
    member this.``SortPeopleByAge_ShouldSortPeopleByAgeAscending`` () =
        let database = Database()
        database.AddPerson(Person("Max Mustermann", 30, "Bad Mergentheim"))
        database.AddPerson(Person("Erika Musterfrau", 40, "Bad Mergentheim"))
        database.AddPerson(Person("John Doe", 25, "Bad Mergentheim"))

        database.SortPeopleByAge()
        let sortedPeople = database.GetRecords() |> Seq.toList

        Assert.Equal("John Doe", sortedPeople.[0].Name) // Jüngste Person sollte an erster Stelle stehen
        Assert.Equal("Max Mustermann", sortedPeople.[1].Name)
        Assert.Equal("Erika Musterfrau", sortedPeople.[2].Name) // Älteste Person sollte an letzter Stelle stehen

    // Test for the CalculateStatistics method
    [<Fact>]
    member this.``CalculateStatistics_ShouldReturnCorrectStatistics`` () =
        let database = Database()
        database.AddPerson(Person("Max Mustermann", 30, "Bad Mergentheim"))
        database.AddPerson(Person("Erika Musterfrau", 40, "Bad Mergentheim"))
        database.AddPerson(Person("John Doe", 25, "Bad Mergentheim"))

        database.CalculateStatistics()
        let res = database.GetRecords() |> Seq.toList

        let averageAge = res |> List.averageBy (fun p -> float p.Age)
        Assert.Equal(31.666666666666668, averageAge, 3)

        let youngestAge = res |> List.minBy (fun p -> p.Age)
        Assert.Equal(25, youngestAge.Age)

        let oldestAge = res |> List.maxBy (fun p -> p.Age)
        Assert.Equal(40, oldestAge.Age)
