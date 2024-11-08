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
        let res = database.GetRecords() |> Seq.toList  // creates F# list as copy of the .NET list

        Assert.Equal(1, res.Length) // number of persons in the db should be 1
        Assert.Equal("Max Mustermann", res.[0].Name) // check person's name

    // Test for the FilterPeopleByAgeRange method
    [<Fact>]
    member this.``FilterPeopleByAgeRange_ShouldReturnCorrectResults`` () =
        let database = Database()
        database.AddPerson(Person("Max Mustermann", 30, "Bad Mergentheim"))
        database.AddPerson(Person("Erika Musterfrau", 40, "Bad Mergentheim"))

        database.FilterPeopleByAgeRange(30, 35)
        let filteredPeople = database.GetRecords() |> Seq.toList

        Assert.Equal(2, filteredPeople.Length) // filtered list should contain 2 persons
        Assert.Equal("Max Mustermann", filteredPeople.[0].Name) // first person should be Max Mustermann

    // Test for the SortPeopleByAge method
    [<Fact>]
    member this.``SortPeopleByAge_ShouldSortPeopleByAgeAscending`` () =
        let database = Database()
        database.AddPerson(Person("Max Mustermann", 30, "Bad Mergentheim"))
        database.AddPerson(Person("Erika Musterfrau", 40, "Bad Mergentheim"))
        database.AddPerson(Person("John Doe", 25, "Bad Mergentheim"))

        database.SortPeopleByAge()
        let sortedPeople = database.GetRecords() |> Seq.toList

        Assert.Equal("John Doe", sortedPeople.[0].Name) // youngest person should be first
        Assert.Equal("Max Mustermann", sortedPeople.[1].Name) // middle person should be second
        Assert.Equal("Erika Musterfrau", sortedPeople.[2].Name) // oldest person should be last

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
        Assert.Equal(31.666666666666668, averageAge, 3) // average age should be 31.67

        let youngestAge = res |> List.minBy (fun p -> p.Age)
        Assert.Equal(25, youngestAge.Age) // youngest person should be 25

        let oldestAge = res |> List.maxBy (fun p -> p.Age)
        Assert.Equal(40, oldestAge.Age) // oldest person should be 40
